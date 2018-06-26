using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public Text HPTxt, maxHPTxt, MPTxt, maxMPTxt, atkTxt, aSpeedTxt, mSpeedTxt;
	public Image HPBar, MPBar;
	private static int HP, maxHP, MP, maxMP, atk, aSpeed, mSpeed;
	private static AudioSource playerAudio;
	public static bool pause;	// 正在玩小游戏或者对话时pause为true

	public enum attrType  		
	{
		HP,
		maxHP,
		MP,
		maxMP,
		atk,
		aSpeed,
		mSpeed
	};

	void Start () {
		pause = false;

		HP = int.Parse (HPTxt.text);
		maxHP = int.Parse (maxHPTxt.text);
		MP = int.Parse (MPTxt.text);
		maxMP = int.Parse (maxMPTxt.text);
		atk = int.Parse (atkTxt.text);
		aSpeed = int.Parse (aSpeedTxt.text);
		mSpeed = int.Parse (mSpeedTxt.text);

		playerAudio = GameObject.FindGameObjectWithTag ("PlayerAudio").GetComponent<AudioSource>();

		/* 添加道具 */
		Item item = new Item ();
        item.Additem("藏宝图", Item.type.activeProp, 0);		// 主动道具
        item.Additem ("狗头人的小铁锹", Item.type.consumable,3);       // 消耗品
        item.Additem("芒果", Item.type.consumable, 1);

    }

    void FixedUpdate () {

		float h = Input.GetAxisRaw("Horizontal");
		float v = Input.GetAxisRaw("Vertical");
		if (!(h == 0 && v == 0)) {
			Move(h,v);
		}
	}

	void Update() {
		HPBar.fillAmount = (float)HP / maxHP;
		MPBar.fillAmount = (float)MP / maxMP;
		HPTxt.text = HP.ToString ();
		maxHPTxt.text = maxHP.ToString ();
		MPTxt.text = MP.ToString ();
		maxMPTxt.text = maxMP.ToString ();
		atkTxt.text = atk.ToString ();
		aSpeedTxt.text = aSpeed.ToString ();
		mSpeedTxt.text = mSpeed.ToString ();
	}

	/* 人物移动 */
	private void Move(float h,float v) {
		Vector3 e_rot = transform.eulerAngles;
		e_rot.x = 0;
		if (v == 0) {
			if (h==-1)
				e_rot.y = 270;
			else if (h==1)
				e_rot.y = 90;
		} else if (v == 1) {
			if (h==-1)
				e_rot.y = 315;
			else if (h==0)
				e_rot.y = 0;
			else if (h==1)
				e_rot.y = 45;
		} else if (v == -1) {
			if (h==-1)
				e_rot.y = 225;
			else if (h==0)
				e_rot.y = 180;
			else if (h==1)
				e_rot.y = 135;
		}
		e_rot.z = 0;
		transform.eulerAngles = e_rot;
		transform.Translate(Vector3.forward * Time.deltaTime * mSpeed, Space.Self);
	}

	public static void reduceAttr(int num, attrType type) {
		int result;
		switch (type) {
		case attrType.HP:
			result = HP - num;
			if (result <= 0) {	// 生命值降到0以下
				HP = 0;
				death ();		// 触发死亡事件
			} else {
				HP = result;
			}
			break;
		case attrType.maxHP:
			result = maxHP - num;
			if (result <= 0) {	// 最大生命值降到0以下
				maxHP = 0;
				death ();		// 触发死亡事件
			} else {
				maxHP = result;
				if (HP > maxHP) {	// 当前生命值超过最大生命值
					HP = maxHP;
				}
			}
			break;
		case attrType.MP:
			result = MP - num;
			if (result <= 0) {	// 魔法值降到0以下
				MP = 0;
			} else {
				MP = result;
			}
			break;
		case attrType.maxMP:
			result = maxMP - num;
			if (result <= 0) {	// 最大魔法值降到0以下
				maxMP = 0;
			} else {
				maxMP = result;
				if (MP > maxMP) {	// 当前魔法值超过最大魔法值
					MP = maxMP;
				}
			}
			break;
		case attrType.atk:
			result = atk - num;
			if (result <= 30) {	// 攻击力降到阈值以下
				atk = 30;
			} else {
				atk = result;
			}
			break;
		case attrType.aSpeed:
			result = aSpeed - num;
			if (result <= 5) {	// 攻击速度降到阈值以下
				aSpeed = 5;
			} else {
				aSpeed = result;
			}
			break;
		case attrType.mSpeed:
			result = mSpeed - num;
			if (result <= 0) {	// 移动速度降到阈值以下
				mSpeed = 0;
			} else {
				mSpeed = result;
			}
			break;
		}
	}

	public static void addAttr(int num, attrType type) {
		int result;
		switch (type) {
		case attrType.HP:
			result = HP + num;
			if (result > maxHP) {	// 生命值超过最大生命
				HP = maxHP;
			} else {
				HP = result;
			}
			break;
		case attrType.maxHP:
			maxHP = maxHP + num;
			addAttr (num, attrType.HP);
			break;
		case attrType.MP:
			result = MP + num;
			if (result > maxMP) {	// 魔法值超过最大魔法
				MP = maxMP;
			} else {
				MP = result;
			}
			break;
		case attrType.maxMP:
			maxMP = maxMP + num;
			addAttr (num, attrType.MP);
			break;
		case attrType.atk:
			result = atk + num;
			if (result > 300) {	// 攻击力超过阈值
				atk = 300;
			} else {
				atk = result;
			}
			break;
		case attrType.aSpeed:
			result = aSpeed + num;
			if (result > 50) {	// 攻击速度超过阈值
				aSpeed = 50;
			} else {
				aSpeed = result;
			}
			break;
		case attrType.mSpeed:
			result = mSpeed + num;
			if (result > 16) {	// 移动速度超过阈值
				mSpeed = 16;
			} else {
				mSpeed = result;
			}
			break;
		}
	}

	/* 角色死亡事件 */
	public static void death() {
		print ("角色死亡");
	}

	/* 返回角色属性 */
	public static int getAttr(attrType type) {
		switch (type) {
		case attrType.HP:
			return HP;
			break;
		case attrType.maxHP:
			return maxHP;
			break;
		case attrType.MP:
			return MP;
			break;
		case attrType.maxMP:
			return maxMP;
			break;
		case attrType.atk:
			return atk;
			break;
		case attrType.aSpeed:
			return aSpeed;
			break;
		case attrType.mSpeed:
			return mSpeed;
			break;
		default:
			return -1;
			break;
		}
	}

	/* 设置角色属性 */
	public static void setAttr(int value, attrType type) {
		switch (type) {
		case attrType.HP:
			HP = value;
			break;
		case attrType.maxHP:
			maxHP = value;
			break;
		case attrType.MP:
			MP = value;
			break;
		case attrType.maxMP:
			maxMP = value;
			break;
		case attrType.atk:
			atk = value;
			break;
		case attrType.aSpeed:
			aSpeed = value;
			break;
		case attrType.mSpeed:
			mSpeed = value;
			break;
		default:
			break;
		}
	}

	/* 根据文件名播放音频 */
	public static void speakByName(string name) {
		if (playerAudio.isPlaying)
			return;
		playerAudio.clip = (AudioClip)Resources.Load("Sound/" + name, typeof(AudioClip));
		playerAudio.Play();
	}
}
