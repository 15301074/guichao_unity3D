using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour {

	private string itemName;	// 物品名称
	private string itemDesc;	// 物品描述
	private type itemType;		// 物品类型
	private int itemNum;		// 物品数量
	private Button itemObj;		// 物品所对应的组件
	private GameObject hotbar;	// 快捷栏
	private GameObject package;	// 背包

	public enum type  		
	{  
		consumable,		// 消耗品
		pasEquip,		// 被动装备
		activeProp		// 主动道具
	};

	/* 添加物品 */
	public void Additem(string name, string desc, Item.type type, int num) {
		hotbar = GameObject.FindGameObjectWithTag ("Hotbar");
		package = GameObject.FindGameObjectWithTag ("Equipment");

		/* 初始化属性 */
		this.itemName = name;
		this.itemDesc = desc;
		this.itemType = type;
		this.itemNum = num;

		/* 实例化物品按钮 */
		itemObj = Instantiate(GameObject.FindGameObjectWithTag("ItemDemo").GetComponent<Button>());
		itemObj.transform.GetChild (0).GetComponent<Text> ().text = itemNum.ToString ();
		itemObj.transform.GetChild (0).gameObject.SetActive (false);		// 右上角使用次数不可见
		itemObj.transform.GetChild (1).GetComponent<Text> ().text = itemName + ";" + itemDesc + ";" + itemType;

		/* 添加到背包或快捷栏 */
		if (itemType.Equals (type.consumable) || itemType.Equals (type.activeProp)) {
			if (hotbar.transform.childCount >= 9) {
				print ("快捷栏已满");
				return;
			}
			if (itemType.Equals (type.consumable)) {	// 消耗品设置数量可见
				itemObj.transform.GetChild (0).gameObject.SetActive (true);
			}
			itemObj.transform.parent = hotbar.transform;
		} else if (itemType.Equals (type.pasEquip)) {
			if (package.transform.childCount >= 6) {
				print ("装备已满");
				return;
			}
			itemObj.transform.parent = package.transform;
		}

		addItemEvent ();
	}

	/* 给物品按钮修改属性并添加主动效果 */
	private void addItemEvent() {
		/* 修改物品按钮属性 */
		itemObj.transform.localScale = new Vector3 (1,1,1);
		itemObj.GetComponent<Image> ().sprite = Resources.Load ("Items/" + itemName, typeof(Sprite)) as Sprite;
		switch (itemName) {

		}
	}

	public Button getItem() {
		return itemObj;
	}
}

