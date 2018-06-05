using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skill3 : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider col){
		if (col.tag == "enemy") {
			print ("skill_3 打到了");
		}
	}
}
