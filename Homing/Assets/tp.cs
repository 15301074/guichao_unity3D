using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tp : MonoBehaviour
{
    public GameObject zhujue;
    public GameObject tp_to_where;

    void OnTriggerEnter(Collider Col)
    {
        if (Col.CompareTag("Player"))
        {
            zhujue.transform.position = new Vector3(209f,20f,761f);
        }
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
