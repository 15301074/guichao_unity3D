﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skill1 : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider col){
		if (col.tag == "enemy") {
			print ("skill_1撞到了");
			Destroy (GameObject.Find ("Skill1(Clone)"));
		}
	}
}
