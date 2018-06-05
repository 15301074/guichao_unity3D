using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JudgeState : MonoBehaviour {

	public Text nowHP, nowMP, maxHP, maxMP;
	public Image HPBar, MPBar;

	void Update () {
		HPBar.fillAmount = float.Parse (nowHP.text) / float.Parse (maxHP.text);
		MPBar.fillAmount = float.Parse (nowMP.text) / float.Parse (maxMP.text);
		if (int.Parse (nowHP.text) == 0) {	// 血量为0，死亡
			Debug.Log("你死啦~");
		}
	}
}
