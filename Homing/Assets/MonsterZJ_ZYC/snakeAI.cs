using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snakeAI : MonoBehaviour {
    public GameObject game;
    private skyWrathMageControl gameComp1;
    public float alertSnakeRange;//视野范围
    public float defendSnakeRange;//自卫范围
    public float attackSnakeRange;//攻击范围
    public float skill1SnakeRange;//喷火求
    public float skill1SnakeRadius;//雷电
    public float skill2SnakeRange;//风
    public float skill3SnakeNUM;//强化攻击力，移动速度
    public float skill4SnakeNUM;//尾巴
    public float skill5SnakeNUM;//治疗技能
    public float snakeMoveSpeed;
    private Animation snakeAnimation;
    private MonsterState currentSnakeState;
    private string state = "";
    private Quaternion targetRotation;         //怪物的目标朝向  
    private float snakeDistanceToPlayer;         //怪物与玩家的距离   
    private int flag;
    private bool isInSkill3;
    public float snakeActRestTme;            //更换待机指令的间隔时间  7秒/140
    private float snakeLastActTime;
    private float snakeLastActTime1;
    private int jj;


    public Rigidbody Skill_1;
    public Transform Skill_1_Position;
    public Rigidbody Skill_2;
    public Transform Skill_2_Position;
    Rigidbody Skill_2_ins;
    private enum MonsterState
    {
        snakeIDLE,
        snakeSTAND1,      //站立 
        snakeSTAND2,      //站立 
        snakeMOVE,       //走路，往任一方向移动 
        snakeWARN,       //警戒，往玩家方向移动  
        snakeATTACK1,    //攻击
        snakeATTACK2,
        snakeSkill1,
        snakeSkill2,
        snakeSkill3,
        snakeSkill4,//咬
        snakeSkill5,//尾巴
        snakeDIE1,
        snakeDIE2,
        snakeHit1,
        snakeHite2,
        snakeDie
    }

	void Start () {
        flag = 0;
        isInSkill3 = false;
        state = "Idle";
        jj = 0;
        gameComp1 = game.GetComponent<skyWrathMageControl>();
        snakeAnimation = this.GetComponent<Animation>();
	}

    void snakeStand()
    {
        print("Stand");
        
        playAnimationByState(state);
    }

    void snakeMove()
    {
        print("Move");
        //朝向怪物
        targetRotation = Quaternion.LookRotation(-game.transform.position + transform.position, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 0.1f);
        transform.Translate(-Vector3.forward * Time.deltaTime * snakeMoveSpeed);
        playAnimationByState(state);
    }

    void snakeQuickMove()
    {
        print("QuickMove");
        //朝向怪物
        targetRotation = Quaternion.LookRotation(-game.transform.position + transform.position, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 0.1f);
        transform.Translate(-Vector3.forward * Time.deltaTime * snakeMoveSpeed*1.7f);
        playAnimationByState(state);
    }

    int getFlag()
    {
        int k = 0;
        this.snakeDistanceToPlayer = Vector3.Distance(game.transform.position, this.transform.position);
        if (this.GetComponent<MonsterAI>().hp <= 0)
            return 5;

        if (snakeDistanceToPlayer >= this.alertSnakeRange + 25f)
            k = 0;
        else if (snakeDistanceToPlayer >= this.alertSnakeRange)
            k = 1;
        else if (snakeDistanceToPlayer >= this.defendSnakeRange)
            k = 2;
        else if (snakeDistanceToPlayer >= this.attackSnakeRange)
            k = 3;
        else
            k = 4;
        return k;
    }

    void changeStateIn1()
    {
        if (game.transform.position.y < this.transform.position.y)
        {
            snakeLastActTime = Time.time;
            currentSnakeState = MonsterState.snakeSTAND1;
            state = "Stand";
        }
        else if (game.transform.position.y >= this.transform.position.y)
        {
            snakeLastActTime = Time.time;
            currentSnakeState = MonsterState.snakeSTAND1;
            state = "Stand2"; 
        }
    }

    void changeStateIn2()
    {
        float number = Random.Range(0, 1000);          
        //if (number <= actionWeight[0])
        if(number<750)
        {      
            snakeLastActTime = Time.time;
            currentSnakeState = MonsterState.snakeMOVE;
            state = "Move";
        }
        else if (750<number&&number<1000)
        {
            snakeLastActTime = Time.time;
            currentSnakeState = MonsterState.snakeSkill3;
            state = "Move";
        }
    }

    void changeStateIn3()
    {
        float number = Random.Range(0, 1000);
        //if (number <= actionWeight[0])
        if (number < 300)
        {
            snakeLastActTime = Time.time;
            currentSnakeState = MonsterState.snakeMOVE;
            state = "Move";
        }
        else if (300 < number && number < 600)
        {
            snakeLastActTime = Time.time;
            currentSnakeState = MonsterState.snakeSkill1;
            state = "Roar";
        }
        else if (600 < number && number < 1000)
        {
            snakeLastActTime = Time.time;
            currentSnakeState = MonsterState.snakeSkill2;
            state = "Magic";
        }
    }

    void changeStateIn4()
    {
        float number = Random.Range(0, 1200);
        //if (number <= actionWeight[0])
        if (number < 350)
        {
            snakeLastActTime = Time.time;
            currentSnakeState = MonsterState.snakeATTACK1;
            state = "Attack1";
        }
        else if (350 < number && number < 700)
        {
            snakeLastActTime = Time.time;
            currentSnakeState = MonsterState.snakeATTACK2;
            state = "Attack2";
        }
        else if (700 < number && number < 850)
        {
            snakeLastActTime = Time.time;
            currentSnakeState = MonsterState.snakeSkill4;
            state = "Bite";
        }
        else if (850 < number && number < 1000)
        {
            snakeLastActTime = Time.time;
            currentSnakeState = MonsterState.snakeSkill5;
            state = "Whip tail";
        }
        else if (1000 < number && number < 1100)
        {
            snakeLastActTime = Time.time;
            currentSnakeState = MonsterState.snakeSkill1;
            state = "Roar";
        }
        else if (1100 < number && number < 1200)
        {
            snakeLastActTime = Time.time;
            currentSnakeState = MonsterState.snakeSkill2;
            state = "Magic";
        }
    }

    void Update () {
        flag = this.getFlag();
        if (flag == 5)
        {
                    currentSnakeState = MonsterState.snakeDie;
                    snakeLastActTime = Time.time;
                    state = "Death1";
        }
        else if (Time.time - snakeLastActTime >= snakeActRestTme)
        {
            if (flag == 1)
                changeStateIn1();
            if (flag == 2)
                changeStateIn2();
            if (flag == 3)
                changeStateIn3();
            if (flag == 4)
                changeStateIn4();
        }
        //增益技能
        if (Time.time - snakeLastActTime1 > 100)
        {
            float number = Random.Range(0, 1000);
            if (number < 200)
            {
                snakeLastActTime1 = Time.time;
                //播放特效，增加攻击力，移动速度
            }
        }
        targetRotation = Quaternion.LookRotation(game.transform.position - transform.position, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 0.1f);
        switch (currentSnakeState)
        {
            case MonsterState.snakeIDLE:
                this.playAnimationByState(state);
                break;

            case MonsterState.snakeSTAND1 :
                this.playAnimationByState(state);
                break;
            case MonsterState.snakeSTAND2:
                this.playAnimationByState(state);
                break;
            case MonsterState.snakeMOVE:
                this.playAnimationByState(state); 
                transform.Translate(Vector3.forward * Time.deltaTime * snakeMoveSpeed);
                print("skill3");
                break;

            case MonsterState.snakeSkill3:
                if (flag == 1)
                {
                    this.playAnimationByState(state); 
                    transform.Translate(Vector3.forward * Time.deltaTime * snakeMoveSpeed*2.0f);
                    print("skill3");
                }
                break;
            case MonsterState.snakeATTACK1:
                this.playAnimationByState(state);
                break;
            case MonsterState.snakeATTACK2:
                this.playAnimationByState(state);
                break;
            case MonsterState.snakeSkill1:
                this.playAnimationByState(state);
                break;

            case MonsterState.snakeSkill2:
                this.playAnimationByState(state);
                break;

            case MonsterState.snakeSkill4:
                this.playAnimationByState(state);
                break;

            case MonsterState.snakeSkill5:
                this.playAnimationByState(state);
                break;
            case MonsterState.snakeDie:
                this.playAnimationByState(state);
                break;
            default:
                break;
        }  
	}
    void playAnimationByState(string state)
    {
        snakeAnimation.Play(state.Trim());
    }

    void death()
    {
        Destroy(gameObject, 1.0f);
    }

    public virtual void skill1()
    {
        Rigidbody clone;
        clone = (Rigidbody)Instantiate(Skill_1, Skill_1_Position.position, Skill_1_Position.rotation);
        clone.velocity = transform.TransformDirection(Vector3.forward * 10f);
        print("skill1");
        //3秒后销毁克隆体
        Destroy(GameObject.Find("SnakeSkill1(Clone)"), 2);
    }


    public virtual void skill2p()
    {
        Skill_2_ins = (Rigidbody)Instantiate(Skill_2, Skill_2_Position.position, Skill_2_Position.rotation);
        print("skill2p");
        //3秒后销毁克隆体
        Destroy(GameObject.Find("SnakeSkill2(Clone)"), 2);
    }


    public virtual void skill2s()
    {
        Skill_2_ins.velocity = transform.TransformDirection(Vector3.forward *10f);
        print("skill2s");
    }
}
