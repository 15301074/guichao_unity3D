using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skyWrathMageControl : MonoBehaviour {

	public float m_speed = 1000f;
	public float rotateSpeed = 2f;

	private AnimatorStateInfo currentBaseState;
	private Animator anim;
	private Collider col;
	//private Collider attackSphere;
	//private Collider skill_3_collider;

	static int attackState = Animator.StringToHash ("Base Layer.attack");
	static int skill_1_State = Animator.StringToHash ("Base Layer.skill_1");
	static int skill_2_State = Animator.StringToHash ("Base Layer.skill_2");
	static int skill_3_State = Animator.StringToHash ("Base Layer.skill_3");
	static int skill_4_State = Animator.StringToHash ("Base Layer.skill_4");

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		col = GetComponent<CapsuleCollider> ();
	}
	
	// Update is called once per frame
	void Update () {
		float horizontal = Input.GetAxis("Horizontal"); //A D 左右
		float vertical = Input.GetAxis("Vertical"); //W S 上 下
		anim.SetFloat ("speed",vertical);
		currentBaseState = anim.GetCurrentAnimatorStateInfo(0);

		//移动
		Vector3 velocity = new Vector3(0, 0, vertical);
		//Vector3 velocity = new Vector3(0, vertical, 0);
		//Vector3 velocity = new Vector3(vertical, 0, 0);
		velocity = transform.TransformDirection (velocity);
		velocity *= m_speed;

		//普通攻击
		if (Input.GetKey (KeyCode.J)) {
			anim.SetBool ("attack", true);
		} else {
			anim.SetBool ("attack", false);
		}

		//技能1
		if (Input.GetKeyDown (KeyCode.U)) {
			anim.SetBool ("skill_1", true);
		} else {
			anim.SetBool ("skill_1", false);
		}

		//技能2
		if (Input.GetKeyDown (KeyCode.I)) {
			anim.SetBool ("skill_2", true);
		} else {
			anim.SetBool ("skill_2", false);
		}

		//技能3
		if (Input.GetKeyDown (KeyCode.O)) {
			anim.SetBool ("skill_3", true);
		} else {
			anim.SetBool ("skill_3", false);
		}

		//技能4
		if (Input.GetKeyDown (KeyCode.P)) {
			anim.SetBool ("skill_4", true);
		} else {
			anim.SetBool ("skill_4", false);
		}

		//死亡
		if (Input.GetKeyDown (KeyCode.K)) {
			anim.SetBool ("die", true);
		}

		//重生
		if (Input.GetKeyDown (KeyCode.L)) {
			anim.SetBool ("die", false);
		}

		//设置在攻击或放技能状态时速度为0
		if (currentBaseState.fullPathHash == attackState) {
			anim.SetFloat ("speed", 0);
		}
		if (currentBaseState.fullPathHash == skill_1_State || currentBaseState.fullPathHash == skill_2_State
			|| currentBaseState.fullPathHash == skill_3_State || currentBaseState.fullPathHash == skill_4_State) {
			anim.SetFloat ("speed", 0);
		}

		transform.localPosition += velocity * Time.fixedDeltaTime;

		//旋转
		transform.Rotate(0, horizontal * rotateSpeed, 0);
	}
}