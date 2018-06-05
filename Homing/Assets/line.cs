using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class line : MonoBehaviour {
    GameObject gameobject ;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter(Collider Col)
    {

        if (Col.CompareTag("Player"))
        {
        Light light = this.transform.Find("Point light").gameObject.GetComponent<Light>();
        light.intensity = 100;
        Debug.Log("Is here?");
        }
    }
    void OnTriggerExit(Collider Col)
    {

        if (Col.CompareTag("Player"))
        {
            Light light = this.transform.Find("Point light").gameObject.GetComponent<Light>();
            light.intensity = 0;
            Debug.Log("Is here?");
        }
    }
}
