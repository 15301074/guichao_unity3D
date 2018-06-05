using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisToEveryMonster : MonoBehaviour {
    public GameObject player;
    public int[] monsterFlag ={0,0,0,0,0,0,0,0,0,0,0};
    public string [] monsterTag= {"monster1","monster2","monster3","monster4","monster5","monster6","monster7","monster8","monster9","monster10"};
    private GameObject[] monster1;

    //private GameObject[] monster2;
    //private GameObject[] monster3;
    public int monster1State;

	// Use this for initialization
	void Start () {
        
	}
    void Update()
    {
        //UpdateMonster1();
    }

   public  void changeMonsterFlag(string tag, int i)
    {
        int j = 0;
        for (j = 0; j < 10; j++)
        {
            if (monsterTag[j] == tag)
            {
                monsterFlag[j] = i;
                break;
            }
        }
    }

   public  int getMonsterFlag(string tag)
    {
        int j = 0;
        for (j = 0; j < 10; j++)
        {
            if (monsterTag[j] == tag)
            {
               return  monsterFlag[j];
            }
        }
        return 0;
    }
}
