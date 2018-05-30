using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class add_atk : MonoBehaviour {

    void OnTriggerEnter(Collider Col)
    {
        if (Col.CompareTag("Player"))
        {
            PlayerController.addAttr(5, PlayerController.attrType.atk);
            Debug.Log("恭喜你增加了5点攻击力");
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
