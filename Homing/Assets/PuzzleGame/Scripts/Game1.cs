using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game1 : MonoBehaviour {

	public Transform rocks;
	public Sprite rock;
	public Sprite slot;
	public GameObject game1;
	public GameObject particles;
	public GameObject roadblock;

	private static bool finished;
	private int[,] matrix;
	private int[,] gameover;

	void Start () {

		if (rocks == null)
			return;
		
		finished = false;

		/* 初始化二维数组 */
		matrix = new int[5,10];
		gameover = new int[5,10];

		for (int i = 0; i < 5; i++)
			for (int j = 0; j < 10; j++)
				matrix [i, j] = 0;

		for (int i = 0; i < 5; i++)
			for (int j = 0; j < 10; j++)
				gameover [i, j] = 0;

		/* 游戏过关条件 
		for (int i = 2; i < 9; i++)
			gameover [0, i] = 1;
		gameover [1, 0] = 1;
		gameover [1, 1] = 1;
		gameover [1, 5] = 1;
		gameover [1, 6] = 1;
		gameover [1, 8] = 1;
		gameover [1, 9] = 1;
		gameover [2, 1] = 1;
		gameover [2, 6] = 1;
		gameover [2, 8] = 1;
		for (int i = 1; i < 9; i++)
			gameover [3, i] = 1;
		gameover [4, 2] = 1;
		gameover [4, 3] = 1;
		gameover [4, 5] = 1;
		gameover [4, 6] = 1; */

		gameover[4,9]=1;	// test

		foreach (Transform child in rocks)  
		{  
			child.GetComponent<Button>().onClick.AddListener(delegate() {
				
				int num = int.Parse(child.name);
				int row = num / 10;
				int col = num - 10 * row;

				if (matrix [row, col] == 0) {
					matrix [row, col] = 1;
					child.GetComponent<Image>().sprite = rock;
				} else {
					matrix [row, col] = 0;
					child.GetComponent<Image>().sprite = slot;
				}

				judgePassed();
			});

		}
	}

	private void judgePassed () {
		if (compareMatrix (matrix, gameover)) {
			finished = true;
			continueGame ();
			rewards ();
			particles.SetActive (false);
			roadblock.SetActive (false);
		}
	}

	private bool compareMatrix(int[,] m1, int[,] m2) {
		for (int i = 0; i < 5; i++)
			for (int j = 0; j < 10; j++)
				if (m1[i,j] != m2[i,j])
					return false;
		return true;
	}

	void OnTriggerEnter(Collider col){
		if (col.tag == "Player" && !finished) {
			pauseGame ();
			game1.SetActive (true);
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
		Time.timeScale = 1;
		game1.SetActive (false);
	}

	/* 游戏奖励 */
	private void rewards() {
		Item item = new Item ();
		item.Additem ("玲珑心", Item.type.pasEquip,-1);
		item.Additem ("仙灵之火", Item.type.consumable,3);

	}
}
