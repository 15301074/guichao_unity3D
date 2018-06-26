using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VSBoss : MonoBehaviour {

	public GameObject dia;
    public GameObject bossHP;
	private GameObject player;
	private Dialog dialog;


	void Start() {
		player = GameObject.FindGameObjectWithTag ("Player");
		dialog = new Dialog (dia);
	}
	void OnTriggerEnter(Collider col) {
		if (col.tag == "Player") {
			player.transform.position = new Vector3 (405f, 0.4f, 136f);

			dialog.open ();
			dialog.setSpeaker ("Player");
			dialog.setContent ("没想到一扇木门竟是一个传送法阵。这里看上去十分诡异，我得时刻做好战斗的准备。");
			dialog.setAnswer (null, "继续");
			dialog.getAnswer2Btn().GetComponent<Button>().onClick.AddListener (delegate() {
				continueGame();
                bossHP.SetActive(true);
            });
			pauseGame ();
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
		dialog.close();
		Time.timeScale = 1;
	}
}
