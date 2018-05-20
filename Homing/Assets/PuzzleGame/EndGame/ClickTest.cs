using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickTest : MonoBehaviour {
	//所有的木块
	public GameObject[] cube_S;
	//资源路径
	public string Cube_Path = "Cubes";

	//空地木块
	//public int random_Null;
	public GameObject empty_Gm;
	public GameObject cube1;
	public GameObject cube2;
	public GameObject cube3;
	public GameObject cube4;
	public GameObject cube5;
	public GameObject cube6;
	public GameObject cube7;
	public GameObject cube8;
	public GameObject cube9;

	public GameObject bg_T;

	public Transform[] all_NowCubes;
	public Init init;

	//当前木块的位置
	public Vector3 m;

	//空地的位置
	public Vector3 n;

	public string all_CubeName;//正确的图片名字相加。
	public string all_NowCubeName;//用来累加当前所有图片的序列。  对比正确序列是否达到了

	bool ifShowTip;
	bool ifNotIn;
	//public GameObject fpsCon;
	Vector3 fpsPos;

    public GameObject PinTUCamera;

    public GameObject EndT;


	// Use this for initialization
	void Start () {
        EndT = GameObject.Find("EndTerrain");
        PinTUCamera = GameObject.Find ("EndTerrain/JigsawCamera");
        //fpsCon.SetActive(false);
        //fpsCon.GetComponent<Camera>().depth = 0;
		ifShowTip = false;
		cube_S = Resources.LoadAll<GameObject> (Cube_Path);

		bg_T = GameObject.Find ("Cubes");
		init = (Init)bg_T.GetComponent(typeof(Init));

		empty_Gm = this.transform.parent.transform.Find("Cube9(Clone)").gameObject;
	
		all_CubeName = init.all_CubeName;
	}
	
	// Update is called once per frame
	void Update () {
		//if (fpsCon.activeInHierarchy == false) {
		//	ifNotIn = false;
		//}
		//if (fpsCon.activeInHierarchy == true) {
		//	ifNotIn = true;
		//}
		//变化视角
		if(Input.GetKeyDown(KeyCode.Escape)){
			//fpsCon.SetActive(true);
		}
		
	}

	void OnMouseOver(){
		//提示按键进行拼图游戏
		ifShowTip=true;
		//变化视角
		if(Input.GetKeyDown(KeyCode.F)){
			//fpsCon.SetActive(false);
			//Debug.Log (ifNotIn+"aaaaaa");
			Cursor.visible = true;
			Cursor.lockState = 0;

		}

	}

	void OnMouseExit(){
		ifShowTip = false;
	}

	void OnGUI(){
		if (ifShowTip) {
			if (ifNotIn==true) {
				GUI.Label (new Rect(Input.mousePosition.x,Screen.height-Input.mousePosition.y,500,40),"按F键切换视角");
			}

		}
	}

	void OnMouseDown(){
		//获取当前木块与空地的距离
		m=gameObject.transform.position;

        n = empty_Gm.gameObject.transform.position;

        //判断距离是否等于木块间距
        if ((Vector3.Distance(m,n)-0.26)<=0.1&&(Vector3.Distance(m,n)-0.26)>=-0.1){

			//移动木块
			//将当前木块移动到空地上
			//float step = speed*Time.deltaTime;
			gameObject.transform.position = Vector3.MoveTowards (gameObject.transform.position, n, 14.4f);

			//将空地处木块移动到当前木块位置上
			empty_Gm.gameObject.transform.position = Vector3.MoveTowards (empty_Gm.gameObject.transform.position, m, 14.4f);

            //换名字
   //         string huancun;
   //         huancun = gameObject.name;
			//gameObject.name = empty_Gm.name;
   //         empty_Gm.name = huancun;

			


		}
        //检查是否拼图成功
        BtnName_All();

    }

	void BtnName_All()
	{
        //all_NowCubeName = "";
        //all_NowCubes =this.transform.parent.GetComponentsInChildren<Transform>();

        //foreach (Transform child in all_NowCubes) {
        //	all_NowCubeName += child.name;
        //}
        //      Debug.Log(all_NowCubeName);
        //if (all_NowCubeName == "CubesCube1(Clone)Cube2(Clone)Cube3(Clone)Cube4(Clone)Cube5(Clone)Cube6(Clone)Cube7(Clone)Cube8(Clone)")
        //{
        //	Debug.Log("拼图完成！！！！！");
        //          
        //      }

        cube1 = this.transform.parent.transform.Find("Cube1(Clone)").gameObject;
        cube2 = this.transform.parent.transform.Find("Cube2(Clone)").gameObject;
        cube3 = this.transform.parent.transform.Find("Cube3(Clone)").gameObject;
        cube4 = this.transform.parent.transform.Find("Cube4(Clone)").gameObject;
        cube5 = this.transform.parent.transform.Find("Cube5(Clone)").gameObject;
        cube6 = this.transform.parent.transform.Find("Cube6(Clone)").gameObject;
        cube7 = this.transform.parent.transform.Find("Cube7(Clone)").gameObject;
        cube8 = this.transform.parent.transform.Find("Cube8(Clone)").gameObject;
        cube9 = this.transform.parent.transform.Find("Cube9(Clone)").gameObject;

        float d1 = Vector3.Distance(cube1.transform.position, cube_S[0].transform.position);
        float d2 = Vector3.Distance(cube2.transform.position, cube_S[1].transform.position);
        float d3 = Vector3.Distance(cube3.transform.position, cube_S[2].transform.position);
        float d4 = Vector3.Distance(cube4.transform.position, cube_S[3].transform.position);
        float d5 = Vector3.Distance(cube5.transform.position, cube_S[4].transform.position);
        float d6 = Vector3.Distance(cube6.transform.position, cube_S[5].transform.position);
        float d7 = Vector3.Distance(cube7.transform.position, cube_S[6].transform.position);
        float d8 = Vector3.Distance(cube8.transform.position, cube_S[7].transform.position);
        float d9 = Vector3.Distance(cube9.transform.position, cube_S[8].transform.position);

        if (d1<0.1&& d2 < 0.1 && d3 < 0.1 && d4 < 0.1 && d5 < 0.1 && d6 < 0.1 && d7 < 0.1 && d8 < 0.1 && d9 < 0.1 )
        {
            //变化视角
            //fpsCon.SetActive(true);
            PinTUCamera.GetComponent<Camera>().depth = -2;
            bg_T.GetComponent<BoxCollider>().enabled = true;
            //EndT.transform.Find("Portal2").gameObject.SetActive(true);
        }

    }
}
