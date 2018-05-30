using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shut_down : MonoBehaviour {

    // Use this for initialization
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator CountSeconds()
    {
     

            PlayerController.reduceAttr(100, PlayerController.attrType.mSpeed);
            yield return new WaitForSeconds(3.0f);
            PlayerController.addAttr(5, PlayerController.attrType.mSpeed);
            
            StopCoroutine("CountSeconds");
        
    }

    void OnTriggerEnter(Collider Col)
    {
        if (Col.CompareTag("Player"))
        {

            InvokeRepeating("die7", 0, 1f);
            PlayerController.reduceAttr(5, PlayerController.attrType.mSpeed);
           
            StartCoroutine("CountSeconds");



        }
    }
    void OnTriggerExit(Collider Col)
    {
        if (Col.CompareTag("Player"))
        {
            Debug.Log("heya444");
            CancelInvoke("die7");

            PlayerController.addAttr(5, PlayerController.attrType.mSpeed);

        }
    }

    private void die7()
    {
        PlayerController.reduceAttr(5, PlayerController.attrType.HP);
    }
}
