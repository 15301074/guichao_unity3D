using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour {
    private GameObject player;           //获取玩家单位  
    private Animator animator;          //自身动画组件  
    private Vector3 initialPosition;            //初始位置  

    public float wanderRadius;          //游走半径，移动状态下，如果超出游走半径会返回出生位置  
    public float alertRadius;         //警戒半径，玩家进入后怪物会发出警告，并一直面朝玩家  
    public float defendRadius;          //自卫半径，玩家进入后怪物会追击玩家，当距离<攻击距离则会发动攻击（或者触发战斗）  
    public float chaseRadius;            //追击半径，当怪物超出追击半径后会放弃追击，返回追击起始位置  
    public float attackRange;            //攻击距离  
   
    public float walkSpeed;          //移动速度  
    public float runSpeed;          //跑动速度  
    int times = 1;
    private enum MonsterState
    {
        STAND,      //站立 
        WALK,       //走路，往任一方向移动 
        WARN,       //警戒，往玩家方向移动  
        RUN,      //追击玩家 
        ATTACK,    //攻击
        RETURN,      //超出追击范围后返回 
        DIE         //死亡
    }

    private MonsterState currentState ;          //默认状态为原地呼吸  

    public float[] actionWeight = { 3000, 0};         //设置待机时各种动作的权重，顺序依次为呼吸、观察、移动  
    public float actRestTme;            //更换待机指令的间隔时间  
    private float lastActTime;          //最近一次指令时间  


    private float diatanceToPlayer;         //怪物与玩家的距离  
    private float diatanceToInitial;         //怪物与初始位置的距离  
    private Quaternion targetRotation;         //怪物的目标朝向  

    private bool is_Active = false;//不是主动攻击的游戏对象
    private bool is_Group = false;//不是群体攻击的游戏对象
    private bool is_Running = false;


    public GameObject global;
    private DisToEveryMonster glb;
    private int intState;
	// Use this for initialization
    void Start(){
        glb = global.GetComponent<DisToEveryMonster>();
        intState = glb.monster1State;
        player = GameObject.FindGameObjectWithTag("Player"); 
        animator = this.GetComponent<Animator>();
        //保存初始位置信息  
        initialPosition = gameObject.GetComponent<Transform>().position;

        //检查并修正怪物设置  
        //1. 自卫半径不大于警戒半径，否则就无法触发警戒状态，直接开始追击了  
        defendRadius = Mathf.Min(alertRadius, defendRadius);
        //2. 攻击距离不大于自卫半径，否则就无法触发追击状态，直接开始战斗了  
        attackRange = Mathf.Min(defendRadius, attackRange);
        //3. 游走半径不大于追击半径，否则怪物可能刚刚开始追击就返回出生点  
        wanderRadius = Mathf.Min(chaseRadius, wanderRadius);
        //随机一个待机动作 
        RandomAction(); 
	}
    
    void RandomAction()
    {
        lastActTime = Time.time; 
        float number = Random.Range(0, actionWeight[0] + actionWeight[1]);          
        //if (number <= actionWeight[0])
        if(true)
        {
            print("Boss Stand");
            currentState = MonsterState.STAND;
            animator.SetTrigger("STAND");        
        }           
        else if (actionWeight[0] < number && number <= actionWeight[0] + actionWeight[1])           
        {
            currentState = MonsterState.WALK;
            targetRotation = Quaternion.Euler(0, Random.Range(1, 5) * 90, 0);
            animator.SetTrigger("WALK");
        }           
    }

	// Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case MonsterState.STAND:
                print("stand now");
                this.PlayAnimator("STAND");
               
                //RandomAction();

                StandCheck();//改变站立状态
                break;

            //游走，根据状态随机时生成的目标位置修改朝向，并向前移动  
            case MonsterState.WALK:
                print("walk now");
                this.PlayAnimator("WALK");
                this.Walk();

                WalkCheck();//改变走路状态
                break;

            //警戒状态，播放一次警告动画和声音，并持续朝向玩家位置  
            case MonsterState.WARN:
                print("warn now");
                this.PlayAnimator("WARN");
                this.Warning();

                WarnCheck();//改变警戒状态
                break;

            //追击状态，朝着玩家跑去  
            case MonsterState.RUN:
                print("run now");
                this.PlayAnimator("RUN");
                this.Run();

                RunCheck();//改变追击状态
                break;

            case MonsterState.ATTACK:
                print("attach now");
                this.Attack();
                this.PlayAnimator("ATTACH");


                attackCheck();//改变攻击状态
                break;

           case MonsterState.DIE://死亡



                break;
            //返回状态，超出追击范围后返回出生位置  
           case MonsterState.RETURN:
                print("return now");
                animator.SetTrigger("RUN");
                this.Return();

                ReturnCheck();//改变状态
                break;
            default:
                break;
        }  
	}
    
    //警戒操作  向玩家
    void Warning()
    {
        //transform.Translate(Vector3.forward * Time.deltaTime * walkSpeed);
        targetRotation = Quaternion.LookRotation(player.transform.position - transform.position, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 0.1f);  
    }

    void Walk()
    {

        transform.Translate(Vector3.forward * Time.deltaTime * walkSpeed);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 0.1f); 
    }

    void Run()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * runSpeed);
        //朝向玩家位置  
        targetRotation = Quaternion.LookRotation(player.transform.position - transform.position, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 0.1f);  
    }

    void Attack()
    {
        //朝向玩家位置  
        targetRotation = Quaternion.LookRotation(player.transform.position - transform.position, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 0.1f);  

    }

    void Return()
    {
        targetRotation = Quaternion.LookRotation(initialPosition - transform.position, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 0.1f);
        transform.Translate(Vector3.forward * Time.deltaTime * runSpeed);  
    }

    void PlayAnimator(string trigger)
    {
        animator.SetTrigger(trigger);
        //gameObject.GetComponent<AudioSource>().Play(); 
    }

    //BOSS处于站立状态，根据主角与他的距离，（状态，血量）进行思考
   private  void StandCheck()
    {
        diatanceToPlayer = Vector3.Distance(player.transform.position, this.transform.position);
        diatanceToInitial = Vector3.Distance(initialPosition, this.transform.position);
        //玩家在警戒半径外 ，状态为移动或者站立
       if(glb.monster1State==1){

           currentState = MonsterState.RUN;  
       }
        else if (diatanceToPlayer >= alertRadius)
        {

        }
        //玩家在警戒范围内，自卫范围外，状态变为警戒
        else if (defendRadius < diatanceToPlayer && diatanceToPlayer < alertRadius)
        {
            print("Boss Stand to Warn");
            currentState = MonsterState.WARN;  
        }
        //玩家在自卫范围内，状态变为追击
        else if (diatanceToPlayer < defendRadius)// && attackRange < diatanceToPlayer)
       {
           if (glb.monster1State == 0) glb.monster1State = 1;
            print("Boss Stand to Run");
            currentState = MonsterState.RUN;  
        }
        //玩家在自卫范围内，状态变为攻击
        else if (attackRange >diatanceToPlayer)
        {
            //currentState = MonsterState.ATTACK;
        }
    }

    //BOSS处于行走状态，根据主角与他的距离，（状态，血量）进行思考
    void WalkCheck()
    {
        diatanceToPlayer = Vector3.Distance(player.transform.position, this.transform.position);
        diatanceToInitial = Vector3.Distance(initialPosition, this.transform.position);
        if (diatanceToInitial > wanderRadius)
        {
            currentState = MonsterState.RETURN;
            print("hudie");
        }
        //玩家在警戒半径外 ，状态为移动或者站立  
        else if (diatanceToPlayer >= alertRadius)
        {

        }
        //玩家在警戒范围内，自卫范围外，状态变为警戒
        else if (defendRadius < diatanceToPlayer && diatanceToPlayer < alertRadius)
        {
            print("走路进入警戒范围\n");
            currentState = MonsterState.WARN;
        }
        //玩家在自卫范围内，状态变为追击
        else if (diatanceToPlayer < defendRadius && attackRange < diatanceToPlayer)
        {
            print("进入自卫范围\n");
            currentState = MonsterState.RUN;
        }
        //玩家在自卫范围内，状态变为攻击
        else if (attackRange > diatanceToPlayer)
        {
            print("进入攻击范围");
            currentState = MonsterState.ATTACK;
        }
        
    }

    //Boss处于警戒状态，根据主角与他的距离，（状态，血量）进行思考
    void WarnCheck()
    {
        diatanceToPlayer = Vector3.Distance(player.transform.position, this.transform.position);
        diatanceToInitial = Vector3.Distance(initialPosition, this.transform.position);
        //玩家在警戒半径外 ，状态为移动或者站立  

        if (glb.monster1State == 1)
        {

            currentState = MonsterState.RUN;
        }
        else if (diatanceToPlayer >= alertRadius)
        {
            print("Boss Warn To Stand");
            currentState = MonsterState.STAND;
        }
        //玩家在警戒范围内，自卫范围外，但在游走范围外 ，状态变为回出生点
        else if (diatanceToInitial > wanderRadius)
        {
            //currentState = MonsterState.RETURN;
        }
        //玩家在自卫范围内，状态变为追击
        else if (diatanceToPlayer < defendRadius)
        {
            if (glb.monster1State == 0) glb.monster1State = 1;
            print("Boss Warn To Run");
            currentState = MonsterState.RUN;
        }
    }

    //BOSS处于追击状态，根据主角与他的距离，（状态，血量）进行思考
    void RunCheck()
    {
        diatanceToPlayer = Vector3.Distance(player.transform.position, this.transform.position);
        diatanceToInitial = Vector3.Distance(initialPosition, this.transform.position);
        //玩家在警戒半径外 ，状态为移动或者站立  

        if (glb.monster1State == 0)
        {

            currentState = MonsterState.RETURN;
        }
        else if (diatanceToPlayer >= alertRadius)
        {

        }
        //玩家在追击范围外 ，状态变为回出生点
        else if ( diatanceToInitial > chaseRadius)
        {
            if (glb.monster1State == 1) glb.monster1State = 0;
            print("Boss Run to Return ");
            currentState = MonsterState.RETURN;
        }
        //玩家在攻击范围内，状态变为攻击
        else if (diatanceToPlayer < attackRange)
        {
            print("Boss Run to Attach ");
            currentState = MonsterState.ATTACK;
        }
    }

    //BOSS处于攻击状态，根据主角与他的距离，（状态，血量）进行思考
    void attackCheck()
    {
        diatanceToPlayer = Vector3.Distance(player.transform.position, this.transform.position);
        diatanceToInitial = Vector3.Distance(initialPosition, this.transform.position);
        //玩家在攻击范围外 ，状态为追击  

        if (glb.monster1State == 0)
        {

            currentState = MonsterState.RETURN;
        }
        else if (diatanceToInitial >= chaseRadius)
        {
            currentState = MonsterState.RETURN;
        }
        else if (diatanceToPlayer > attackRange)
        {
            currentState = MonsterState.RUN;
        }
        //玩家在追击范围外 ，状态变为回出生点
        //玩家被击败，状态变为行走
       // else if (diatanceToPlayer < alertRadius && diatanceToInitial < chaseRadius)
        //{

       // }
        //Boss被击败，状态变为死亡
       // else if (diatanceToPlayer < alertRadius && diatanceToInitial < chaseRadius)
       // {

      //  }
    }

    //Boss处于返回原点状态，此过程回血量，不受主角攻击打扰，防御力增加使得主角攻击攻击力下降90%
    void ReturnCheck()
    {
        diatanceToInitial = Vector3.Distance(transform.position, initialPosition);
        if (diatanceToInitial < 0.5f)
        {
            //is_Running = false;
            //RandomAction();
            print("Boss Return to Stand");
            currentState = MonsterState.STAND;
        }
    }  

}
