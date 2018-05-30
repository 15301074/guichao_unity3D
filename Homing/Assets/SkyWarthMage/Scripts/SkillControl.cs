using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SkillControl : MonoBehaviour {

	public float Skill_1_Speed = 10f;
	public Rigidbody Skill_1;
	public Transform Skill_1_Position;

	public GameObject Skill_2;

	//public GameObject Skill_3_Circle;
	public GameObject Skill_3_Lighting;
	public CapsuleCollider Skill_3_Collider;
	public Transform Skill_3_Position;

	private Collider attackSphere;

	// Use this for initialization
	void Start () {
		attackSphere = GetComponentInChildren<SphereCollider> ();
		Skill_3_Collider.enabled = false;
		//skill_3_collider = GetComponentInChildren<CapsuleCollider> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void AttackStart(){
		attackSphere.enabled = true;
	}


	void AttackStop(){
		attackSphere.enabled = false;
	}

	void Skill_1_Start(){
		if (GameObject.Find ("Skill1(Clone)")) {
			Destroy (GameObject.Find ("Skill1(Clone)"));
		}

		Rigidbody clone;

		clone = (Rigidbody)Instantiate(Skill_1,Skill_1_Position.position,Skill_1_Position.rotation);
		clone.velocity = transform.TransformDirection(Vector3.forward * Skill_1_Speed);
        clone.tag = "ATKSkill1";
        //1秒后销毁克隆体
        Destroy(GameObject.Find("Skill1(Clone)"),1);

		//Destroy (clone, 3);
	}

	void Skill_2_Start(){
		Skill_2.GetComponentInChildren<ParticleSystem> ().Play ();
	}

	void Skill_3_Start(){
		//魔法阵特效
		/*GameObject clone_circle;
		Quaternion quaternion = new Quaternion ();
		quaternion.eulerAngles = new Vector3 (90f, 0f, 0f);
		clone_circle = (GameObject)Instantiate (Skill_3_Circle, Skill_3_Position.position + new Vector3(0f ,0.3f, 0f), quaternion);
		clone_circle.GetComponent<ParticleSystem> ().Play ();
		Destroy (clone_circle, 1);*/

		//闪电特效
		GameObject clone_lighting;
		clone_lighting = (GameObject)Instantiate (Skill_3_Lighting, Skill_3_Position.position,Skill_3_Position.rotation);
		clone_lighting.GetComponent<ParticleSystem> ().Play ();
		Destroy (clone_lighting, 3);
	}

	void Skill_3_end(){

		Skill_3_Collider.enabled = true;	//技能3碰撞体生效

		Invoke("Skill3_colleder_end", 2);	//2秒后执行一次Skill3_colleder_end函数

	}

	void Skill3_colleder_end(){
		Skill_3_Collider.enabled = false;
	}
}
