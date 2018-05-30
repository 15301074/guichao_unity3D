using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tp_back : MonoBehaviour {
    public GameObject zhujue;
    public GameObject tp_back_where;
    void OnTriggerEnter(Collider Col)
    {
        if (Col.CompareTag("Player"))
        {
            zhujue.transform.position = tp_back_where.transform.position;
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
