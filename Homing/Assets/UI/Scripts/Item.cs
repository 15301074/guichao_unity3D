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
	public void Additem(string name, Item.type type, int num) {
		hotbar = GameObject.FindGameObjectWithTag ("Hotbar");
		package = GameObject.FindGameObjectWithTag ("Equipment");

		/* 初始化属性 */
		this.itemName = name;
		this.itemType = type;
		this.itemNum = num;

		/* 实例化物品按钮 */
		itemObj = Instantiate(GameObject.FindGameObjectWithTag("ItemDemo").GetComponent<Button>());

		/* 添加到背包或快捷栏 */
		if (itemType.Equals (type.consumable) || itemType.Equals (type.activeProp)) {
			if (hotbar.transform.childCount >= 9) {
				print ("快捷栏已满");
				return;
			}
			if (itemType.Equals (type.consumable)) {	// 消耗品设置数量可见
				itemObj.transform.GetChild (0).gameObject.SetActive (true);
			} else if (itemType.Equals (type.activeProp)) {
				itemNum = -1;
			}
			itemObj.transform.SetParent (hotbar.transform, false);
		} else if (itemType.Equals (type.pasEquip)) {
			if (package.transform.childCount >= 6) {
				print ("装备已满");
				return;
			}
			itemNum = -1;
			itemObj.transform.SetParent(package.transform, false);
		}
		/* 修改物品按钮属性 */
		itemObj.GetComponent<Image> ().sprite = Resources.Load ("Items/" + itemName, typeof(Sprite)) as Sprite;
		addItemEvent ();
		itemObj.transform.GetChild (0).GetComponent<Text> ().text = itemNum.ToString ();
		itemObj.transform.GetChild (0).gameObject.SetActive (false);		// 右上角使用次数不可见
		itemObj.transform.GetChild (1).GetComponent<Text> ().text = itemName + ";" + itemDesc + ";" + itemType;
	}

	/* 给物品按钮修改属性并添加主动效果 */
	private void addItemEvent() {
		switch (itemName) {
		case "芒果":			// 点击回魔
			this.itemDesc = "基迪岛上特有的苦甜参半风味让两栖动物无法抗拒。\n使用：吞食芒果\n立刻回复175点魔法。";
			itemObj.onClick.AddListener (delegate() {
				PlayerController.addAttr (100, PlayerController.attrType.MP);
			});
			break;
		case "藏宝图":		// 点击显示藏宝图
			this.itemDesc = "一张古老的藏宝图，机遇与危险并存，考验寻宝者的智慧与勇气。\n使用：打开藏宝图\n显示藏宝图详情。";
			GameObject treasureMap = new GameObject ();
			treasureMap.transform.SetParent (GameObject.FindGameObjectWithTag ("GameCanvas").transform, false);
			treasureMap.name = "TreasureMap";
			/* 设置图片样式 */
			treasureMap.AddComponent<Image> ();
			treasureMap.GetComponent<Image> ().sprite = Resources.Load ("Others/TreasureMap", typeof(Sprite)) as Sprite;
			treasureMap.transform.localPosition = new Vector3 (0, 0, 0);
			treasureMap.GetComponent<Image> ().SetNativeSize ();
			/* 设置点击事件 */
			treasureMap.AddComponent<Button> ();
			treasureMap.GetComponent<Button> ().onClick.AddListener (delegate() {
				treasureMap.SetActive (false);
			});
			treasureMap.SetActive (false);
			itemObj.onClick.AddListener (delegate() {
				treasureMap.SetActive (true);
			});
			break;
		case "秘法鞋":		// 增加最大魔法和移速属性
			this.itemDesc = "装备这种鞋子的法师在战斗中值得重视。\n属性：\n最大魔法值\t+50\n移动速度\t+2";
			PlayerController.addAttr (50, PlayerController.attrType.maxMP);
			PlayerController.addAttr (2, PlayerController.attrType.mSpeed);
			break;
		case "狗头人的小铁锹":
			this.itemDesc = "狗头人米波所钟爱的小铁锹，敲人挖土功能丰富。不过现在已经破烂不堪，仿佛再使用几次就会损坏。\n使用：挖土\n挖挖看说不定有什么好东西。";
			Transform pTransform = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
			itemObj.onClick.AddListener (delegate() {
				float x = pTransform.position.x;
				float z = pTransform.position.z;
				if(Mathf.Abs(x - 188) < 5 && Mathf.Abs(z - 210) < 5) {	// 第一个挖宝点
					
				} else if(Mathf.Abs(x - 110) < 5 && Mathf.Abs(z - 130) < 5) {	// 第二个挖宝点
					
				} else if(Mathf.Abs(x - 146) < 5 && Mathf.Abs(z - 168) < 5) {	// 第三个挖宝点
					
				} else {	// 非挖宝点
					
				}
			});
			break;
		case "仙灵之火":
			this.itemDesc = "虚无的火焰，来自天木熊熊燃烧的遗迹，可以跨越现实燃烧。\n使用：吞服\n立刻回复85点生命值。";
			itemObj.onClick.AddListener (delegate() {
				PlayerController.addAttr (85, PlayerController.attrType.HP);
			});
			break;
		case "古典密码学":
			this.itemDesc = "总结古典密码加密解密技术。是学习密码学不可多得资料。\n属性：\n最大生命值\t+50\n最大魔法值\t+50";
			PlayerController.addAttr (50, PlayerController.attrType.maxHP);
			PlayerController.addAttr (50, PlayerController.attrType.maxMP);
			break;
		case "玲珑心":
			this.itemDesc = "位于法术技艺核心的，是只有身负异能之人才能感知的光带。\n冷却减少：\n所有技能和物品的冷却时间减少25%。";
			// 尚未实现功能
			break;
		}
	}
		
}

