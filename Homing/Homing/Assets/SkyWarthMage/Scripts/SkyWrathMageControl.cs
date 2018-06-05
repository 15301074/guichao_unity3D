using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyWrathMageControl : MonoBehaviour {

	private AnimatorStateInfo currentBaseState;
	private Animator anim;
	private Collider col;

	static int attackState = Animator.StringToHash ("Base Layer.attack");
	static int skill_1_State = Animator.StringToHash ("Base Layer.skill_1");
	static int skill_2_State = Animator.StringToHash ("Base Layer.skill_2");
	static int skill_3_State = Animator.StringToHash ("Base Layer.skill_3");

	void Start () {
		anim = GetComponent<Animator> ();
		col = GetComponent<CapsuleCollider> ();
	}

	void Update () {
		
		currentBaseState = anim.GetCurrentAnimatorStateInfo(0);

		//普通攻击
		if (Input.GetKey (KeyCode.J)) {
			anim.SetBool ("attack", true);
		} else {
			anim.SetBool ("attack", false);
		}

		//技能1
		if (Input.GetKeyDown (KeyCode.U)) {
			int MP = PlayerController.getAttr (PlayerController.attrType.MP);
			// --------------------------------------- 先判断冷却再判断魔量    待添加：冷却判断 --------------------------------------- 
			if (MP > 10) {
				anim.SetBool ("skill_1", true);
				PlayerController.reduceAttr (10, PlayerController.attrType.MP);
				PlayerController.speakByName ("攻击1");
			} else {
				PlayerController.speakByName ("魔法不足1");
			}
		} else {
			anim.SetBool ("skill_1", false);
		}

		//技能2
		if (Input.GetKeyDown (KeyCode.I)) {
			int MP = PlayerController.getAttr (PlayerController.attrType.MP);
			// --------------------------------------- 先判断冷却再判断魔量    待添加：冷却判断 --------------------------------------- 
			if (MP > 15) {
				anim.SetBool ("skill_2", true);
				PlayerController.reduceAttr (15, PlayerController.attrType.MP);
				PlayerController.speakByName ("攻击2");
				/* 技能效果 增加属性10秒 */
				PlayerController.addAttr (10, PlayerController.attrType.atk);
				PlayerController.addAttr (10, PlayerController.attrType.aSpeed);
				PlayerController.addAttr (3, PlayerController.attrType.mSpeed);

			} else {
				PlayerController.speakByName ("魔法不足2");
			}		} else {
			anim.SetBool ("skill_2", false);
		}

		//技能3
		if (Input.GetKeyDown (KeyCode.O)) {
			int MP = PlayerController.getAttr (PlayerController.attrType.MP);
			// --------------------------------------- 先判断冷却再判断魔量    待添加：冷却判断 --------------------------------------- 
			if (MP > 20) {
				anim.SetBool ("skill_3", true);
				PlayerController.reduceAttr (20, PlayerController.attrType.MP);
				PlayerController.speakByName ("攻击3");
			} else {
				PlayerController.speakByName ("魔法不足3");
			}		} else {
			anim.SetBool ("skill_3", false);
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
		if (currentBaseState.fullPathHash == skill_1_State || currentBaseState.fullPathHash == skill_2_State || currentBaseState.fullPathHash == skill_3_State) {
			anim.SetFloat ("speed", 0);
		}
	}
}