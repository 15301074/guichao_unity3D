using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterDoor : MonoBehaviour {
    public GameObject player;
    public GameObject player1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "SkyWrathMage")
        {
            Debug.Log("aaaa");
            player.transform.position = player1.transform.position;
        }
            
    }
}
