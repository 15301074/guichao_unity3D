using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HotbarKeyborad : MonoBehaviour {

    private GameObject hotbar;	// 快捷栏
    private Button[] items;
    private KeyCode[] codes;

    void Start () {
        hotbar = GameObject.FindGameObjectWithTag("Hotbar");
        items = new Button[9];
        
        codes = new KeyCode[9];
        codes[0] = KeyCode.Alpha1;
        codes[1] = KeyCode.Alpha2;
        codes[2] = KeyCode.Alpha3;
        codes[3] = KeyCode.Alpha4;
        codes[4] = KeyCode.Alpha5;
        codes[5] = KeyCode.Alpha6;
        codes[6] = KeyCode.Alpha7;
        codes[7] = KeyCode.Alpha8;
        codes[8] = KeyCode.Alpha9;
    }

    // Update is called once per frame
    void Update () {
        int itemNum = hotbar.transform.childCount;

        int i;
        for (i = 0; i < items.Length; i++)
        {
            items[i] = null;
        }
        i = 0;
        foreach (Transform child in hotbar.transform)
        {
            if(child.gameObject.active == true)
            {
                items[i++] = child.GetComponent<Button>();
            }
        }
        for (i = 0; i < items.Length; i++)
        {
            if (Input.GetKeyUp(codes[i]))
            {
                items[i].onClick.Invoke();
            }
        }
        
    }
}
