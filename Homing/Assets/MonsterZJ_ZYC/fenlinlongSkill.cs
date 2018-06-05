using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class fenlinlongSkill : MonoBehaviour
{
    public Rigidbody Skill_1;
    public Transform Skill_1_Position;

    // Use this for initialization
    public ParticleSystem doct;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void DoctorSkill()
    {
        GameObject[] Monsters;
        print(this.tag);
        Monsters = GameObject.FindGameObjectsWithTag(this.tag);
        foreach (GameObject monster in Monsters)
        {
            MonsterAI k = monster.GetComponent<MonsterAI>();
            k.Doctor();
        }
    }

    void die()
    {
        Destroy(gameObject, 1.0f);
    }
    public virtual void doctorskill()
    {
        print("doctor");
        Rigidbody clone;
        clone = (Rigidbody)Instantiate(Skill_1, Skill_1_Position.position, Skill_1_Position.rotation);
        //3秒后销毁克隆体
        Destroy(GameObject.Find("DoctorSkill(Clone)"), 3);
    }
}
