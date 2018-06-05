using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skillColider : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter(Collider col)
    {
        print("destroy skill");
        Destroy(GameObject.Find("SnakeSkill2(Clone)"));
    }

}
