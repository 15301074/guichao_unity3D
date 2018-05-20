using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkWithFuri : MonoBehaviour {

	public GameObject dia;
	public GameObject NPC;
	public BoxCollider nextCollider;
	private Dialog dialog;

	void Start() {
		dialog = new Dialog (dia);
	}
		
	void OnTriggerEnter(Collider col) {
		if (col.tag == "Player") {
			pauseGame ();
			dialog.open ();
			dialog.setSpeaker ("先知");
			dialog.setContent ("进入对面的木门接受最后的考验，你会得到你想知道的。");
			dialog.setAnswer (null, "多谢指点");
			dialog.getAnswer2Btn().GetComponent<Button>().onClick.AddListener (delegate() {
				continueGame();
			});
		}
	}

	/* 暂停游戏 */
	private void pauseGame() {
		PlayerController.pause = true;
		Time.timeScale = 0;		
	}

	/* 继续游戏 */
	private void continueGame() {
		PlayerController.pause = false;
		this.gameObject.SetActive (false);
		NPC.SetActive(false);
		nextCollider.gameObject.SetActive(true);
		dialog.close();
		Time.timeScale = 1;
	}
}
