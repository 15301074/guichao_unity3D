using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour {

	private GameObject dialog;
	private GameObject playerPortrait;
	private GameObject NPCPortrait;
	private GameObject contentTxt;
	private GameObject answer1Btn;
	private GameObject answer2Btn;

	/* 构造方法 */
	public Dialog (GameObject d) {
		dialog = d;
		foreach (Transform child in dialog.transform) {
			if (child.name.Equals("PlayerPortraitSlot")) {
				playerPortrait = child.gameObject;
			} else if (child.name.Equals("NPCPortraitSlot")) {
				NPCPortrait = child.gameObject;
			} if (child.name.Equals("Content")) {
				contentTxt = child.gameObject;
			} if (child.name.Equals("Answer1")) {
				answer1Btn = child.gameObject;
			} if (child.name.Equals("Answer2")) {
				answer2Btn = child.gameObject;
			}
		}
	}

	/* 设置讲话者 */
	public void setSpeaker(string speaker) {
		if (speaker.Equals ("Player")) {
			playerPortrait.SetActive (true);
			NPCPortrait.SetActive (false);
		} else {
			playerPortrait.SetActive (false);
			NPCPortrait.SetActive (true);
			foreach (Transform child in NPCPortrait.transform) {
				if (child.name.Equals("Portrait")) {		// 设置头像
					child.GetComponent<Image>().overrideSprite = Resources.Load("Speaker/" + speaker, typeof(Sprite)) as Sprite;
				} else if (child.name.Equals("Name")) {		// 设置姓名
					child.GetComponent<Text>().text = speaker;
				}
			}
		}
	}

	/* 设置对话内容 */
	public void setContent(string content) {
		contentTxt.GetComponent<Text>().text = content;
	}

	/* 设置回答 */
	public void setAnswer(string answer1, string answer2) {
		if (answer1 == null)
			answer1Btn.gameObject.SetActive (false);
		else {
			answer1Btn.gameObject.SetActive (true);
			answer1Btn.GetComponentInChildren<Text> ().text = answer1;
		}

		if (answer2 == null)
			answer2Btn.gameObject.SetActive (false);
		else {
			answer2Btn.gameObject.SetActive (true);
			answer2Btn.GetComponentInChildren<Text> ().text = answer2;
		}
	}
		
	/* 显示对话框 */
	public void open() {
		dialog.SetActive (true);
	}

	/* 关闭对话框 */
	public void close() {
		dialog.SetActive (false);
	}

	/* 获得回答1按钮 */
	public GameObject getAnswer1Btn() {
		answer1Btn.GetComponent<Button> ().onClick.RemoveAllListeners ();
		return answer1Btn;
	}

	/* 获得回答2按钮 */
	public GameObject getAnswer2Btn() {
		answer2Btn.GetComponent<Button> ().onClick.RemoveAllListeners ();
		return answer2Btn;
	}
}
