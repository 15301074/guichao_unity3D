using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Roadblock : MonoBehaviour {

	public GameObject dia;
	private Dialog dialog;
	private bool finished;

	void Start () {
		dialog = new Dialog (dia);
	}

	void OnTriggerEnter(Collider col){
		if (col.tag == "Player" && !finished) {
			pauseGame ();
			dialog.open ();
			dialog.setSpeaker ("Player");
			dialog.setContent ("这块巨石仿佛有种力量禁止我飞越过去，不知它很左侧的巨石阵有何联系，也不知它上面神秘的图案有何深意");
			dialog.setAnswer (null, "就此观察图案");
			dialog.getAnswer2Btn().GetComponent<Button>().onClick.AddListener (delegate() {
				dialog.setSpeaker ("神秘图案");
				dialog.setContent ("（仿佛数字和石块之间存在某种联系）");
				dialog.setAnswer (null, "前去观察巨石阵");
				dialog.getAnswer2Btn().GetComponent<Button>().onClick.AddListener (delegate() {
					continueGame();
				});
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
		finished = true;
		dialog.close ();
		Time.timeScale = 1;
	}
}
