using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Game2 : MonoBehaviour {

	public GameObject dia;
	public GameObject game2;
	public GameObject NPC;

	public Text answer;

	private GameObject question1;
	private GameObject question2;
	private GameObject confirmBtn;
	private GameObject cancelBtn;

	private string state;
	private string answer1;
	private string answer2;
	private Dialog dialog;

	void Start () {

		game2.SetActive (false);
		state = "question1";
		answer1 = "SHIFT";
		answer2 = "1";		// swhdayrugqtgrjgs
		dialog = new Dialog (dia);

		/* 初始化GameObject */
		foreach (Transform child in game2.transform) {
			if (child.name.Equals("Question1")) {
				question1 = child.gameObject;
			} else if (child.name.Equals("Question2")) {
				question2 = child.gameObject;
			} else if (child.name.Equals("Confirm")) {
				confirmBtn = child.gameObject;
			} else if (child.name.Equals("Cancel")) {
				cancelBtn = child.gameObject;
			}
		}
		question1.SetActive (true);
		question2.SetActive (false);

		/* 提交答案点击事件 */
		confirmBtn.GetComponent<Button>().onClick.AddListener (delegate() {
			if(state.Equals("question1") && answer.text.Equals(answer1)) {
				answer.text = "";
				question1.SetActive (false);
				question2.SetActive (true);
				state = "question2";
			} else if (state.Equals("question2") && answer.text.Equals(answer2)) {
				game2.SetActive(false);
				dialog.open();
				dialog.setContent("不错不错，看来你学得很扎实，这里有些小玩意儿你拿去玩吧。");
				dialog.setAnswer(null, "拜谢离开");
				dialog.getAnswer2Btn().GetComponent<Button>().onClick.AddListener (delegate() {
					continueGame();
					rewards();	// 游戏奖励
				});
			}
		});

		cancelBtn.GetComponent<Button>().onClick.AddListener (delegate() {
			if(state.Equals("question1")) {
				game2.SetActive(false);
				dialog.open();
				dialog.setContent("如果你发现了图片里的人是恺撒的雕像，" +
					"那么应该能联想到我所讲的古典密码一课中的恺撒密码，由于这些字母无法组成一个有意义的英文单词，" +
					"说明这是加密后的密文，再观察发现字母上有三点，说明要前推三位，最后得到答案为SHIFT。");
				dialog.setAnswer(null, "惭愧离开");
				dialog.getAnswer2Btn().GetComponent<Button>().onClick.AddListener (delegate() {
					continueGame();
				});

			} else if (state.Equals("question2")) {
				game2.SetActive(false);
				dialog.open();
				dialog.setContent("人们在单一恺撒密码的基础上扩展出多表密码，称为“维吉尼亚”密码，而那张图就是维吉尼亚密码表。而且这题我在作业里布置过，难道你当时是抄的？\n看来你学得还不够扎实，无法得到朱树理的赏识。");
				dialog.setAnswer(null, "离开");
				dialog.getAnswer2Btn().GetComponent<Button>().onClick.AddListener (delegate() {
					continueGame();
				});
			}
		});
	}

	void OnTriggerEnter(Collider col){
		if (col.tag == "Player" && !state.Equals("over")) {
			pauseGame ();
			dialog.open ();
			dialog.setSpeaker ("？？？");
			dialog.setContent ("这位同学请留步！");
			dialog.setAnswer ("有何贵干", "好奇怪的怪蜀黍，无视他");
			dialog.getAnswer1Btn().GetComponent<Button>().onClick.AddListener (delegate() {  
				dialog.setSpeaker ("刘铎");
				dialog.setContent("我是北京交通大学的刘铎刘老师，不知道你是否选修过我的信息安全理论与实践一课？");
				dialog.setAnswer("是", "否");
				dialog.getAnswer1Btn().GetComponent<Button>().onClick.AddListener (delegate() {  
					dialog.setContent("甚好，就让为师抽查一下你的学习进度。");
					dialog.setAnswer(null, "放马过来");
					dialog.getAnswer2Btn().GetComponent<Button>().onClick.AddListener (delegate() {
						dialog.close();
						game2.SetActive(true);
					});

				});
				dialog.getAnswer2Btn().GetComponent<Button>().onClick.AddListener (delegate() {
					dialog.setContent("可惜可惜，看来你我无缘，就此别过。");
					dialog.setAnswer(null, "离开");
					dialog.getAnswer2Btn().GetComponent<Button>().onClick.AddListener (delegate() {
						continueGame();
					});
				});

			});
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
		NPC.SetActive (false);
		game2.SetActive(false);
		state = "over";
		dialog.close();
		Time.timeScale = 1;
	}

	/* 游戏奖励 */
	private void rewards() {
		Item item = new Item ();
		item.Additem ("古典密码学", Item.type.pasEquip,0);	
		item.Additem ("芒果", Item.type.consumable,3);
	}
}
