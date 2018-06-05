using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeColiderState : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void beginAttach()
    {
        this.GetComponent<MonsterAI>().ATKStartEnemy();
        print("熊开始攻击");
    }


    void endAttach()
    {
        this.GetComponent<MonsterAI>().ATKEndEnemy();
        print("熊结束攻击");
    }

    void die()
    {

        Destroy(gameObject, 1.0f);
    }

}
