using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemCommnEvents : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler {

	[Header("物品描述")]
	public GameObject description;
	private GameObject iconImg;
	private GameObject typeTxt;
	private GameObject nameTxt;
	private GameObject descTxt;

	[Header("物品属性")]
	private string itemName;	// 物品名称
	private string itemDesc;	// 物品描述
	private string itemType;	// 物品类型

	void Start() {
		foreach (Transform child in description.transform) {
			if (child.name.Equals("ItemIcon")) {
				iconImg = child.gameObject;
			} else if (child.name.Equals("ItemType")) {
				typeTxt = child.gameObject;
			} else if (child.name.Equals("ItemName")) {
				nameTxt = child.gameObject;
			} else if (child.name.Equals("ItemDesc")) {
				descTxt = child.gameObject;
			}
		}

		string[] info = this.transform.GetChild (1).GetComponent<Text> ().text.Split (';');
		itemName = info [0];
		itemDesc = info [1];
		switch (info [2]) {
		case "activeProp":
			itemType = "主动道具";
			break;
		case "consumable":
			itemType = "消耗品";
			break;
		case "pasEquip":
			itemType = "装备";
			break;
		default:
			itemType = "???";
			break;
		}
	}

    /* 点击数量减一，减至0物品耗尽 */
    public void OnPointerClick(PointerEventData eventData){
        /*if(eventData.pointerId == -1){
			Text numText = this.transform.GetChild (0).GetComponent<Text> ();
			numText.text = (int.Parse(numText.text) - 1).ToString();
			if (this.transform.GetChild (0).GetComponent<Text> ().text.Equals ("0")) {
				this.gameObject.SetActive (false);
				GameObject.FindGameObjectWithTag ("Description").SetActive(false);
			}
		}*/
    }

    /* 经过显示描述 */
    public void OnPointerEnter(PointerEventData eventData){

		/* 设置属性 */
		iconImg.GetComponent<Image> ().sprite = Resources.Load ("Items/" + itemName, typeof(Sprite)) as Sprite;
		typeTxt.GetComponent<Text> ().text = itemType;
		nameTxt.GetComponent<Text> ().text = itemName;
		descTxt.GetComponent<Text> ().text = itemDesc;

		/* 设置位置 */
		description.transform.position = new Vector3 (this.transform.position.x,this.transform.position.y + 15);

		/* 设置可见 */
		description.gameObject.SetActive (true);
	}

	/* 离开关闭描述 */
	public void OnPointerExit(PointerEventData eventData){
		description.gameObject.SetActive (false);
	}
}
