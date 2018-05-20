using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisToEveryMonster : MonoBehaviour {
    public GameObject player;
    private GameObject[] monster1;

    //private GameObject[] monster2;
    //private GameObject[] monster3;
    public int monster1State;
    public int monster2State;
    public int monster3State;
	// Use this for initialization
	void Start () {
        monster1State = 0;//
        //monster1=GameObject.FindGameObjectsWithTag("monster1");
        monster2State = 0;// GameObject.FindGameObjectsWithTag("monster2");
        monster3State = 0;// GameObject.FindGameObjectsWithTag("monster3");
	}
    void Update()
    {
        //UpdateMonster1();
    }
	// Update is called once per frame
	/*void UpdateMonster1 () {
        //if (this.isInmonsterAttach("monster1"))
        //    monster1State = 3;
        //else if(this.isInmonsterDefend("monster1"))s
        //    monster1State =2;
        //else if(this.isInmonsterWarn("monster1"))
        //    monster1State=1;
        //else
        //    monster1State=0;
	}
    bool isInmonsterAttach(string tag)
    {
        foreach (GameObject monster in monster1)
        {
            if (Vector3.Distance(player.transform.position, monster.transform.position) < monster.GetComponent<monsterOneAI>().attackRange)
                return true;
        }
        return false;
    }
    bool isInmonsterDefend(string tag)
    {
        foreach (GameObject monster in monster1)
        {
            if (Vector3.Distance(player.transform.position, monster.transform.position) < monster.GetComponent<monsterOneAI>().defendRadius)
                return true;
        }
        return false;
    }
    bool isInmonsterWarn(string tag)
    {
        foreach (GameObject monster in monster1)
        {
            if (Vector3.Distance(player.transform.position, monster.transform.position) < monster.GetComponent<monsterOneAI>().alertRadius)
                return true;
        }
        return false;
    }
    bool isPlayerDie()
    {
        return false;
    }*/
}
