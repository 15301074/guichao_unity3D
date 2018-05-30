using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class red_mp : MonoBehaviour {


    void OnTriggerEnter(Collider Col)
    {
        if (Col.CompareTag("Player"))
        {
            PlayerController.reduceAttr(10, PlayerController.attrType.MP);
            Debug.Log("你碰到了毒酒，减少10点MP");
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
