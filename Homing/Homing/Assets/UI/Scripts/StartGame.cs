using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour {

	public GameObject desc;
	public GameObject package;

	void Start () {
		desc.SetActive (false);
		package.SetActive (false);
	}
	
	void Update () {
		
	}
}
