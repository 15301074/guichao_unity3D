using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Init : MonoBehaviour {
   
	bool ifShowTip;
    //拼图camera
	public Camera jigsawCamera;

	//资源路径
	public string Cube_Path = "Resources/Cubes";

	//所有的木块
	public GameObject[] cube_S;
    //public GameObject[] cube1_S;

	//背景
	public Transform bg_T;

	//保存空拼图
	public GameObject empty_Cube;
	//public int random_Null;

	//存储拼图位置
	//public Vector3[] cubes_Pos={Vector3.one,Vector3.one,Vector3.one,Vector3.one,Vector3.one,Vector3.one,Vector3.one,Vector3.one,Vector3.one};


	public string all_CubeName;//正确的图片名字相加。
	public string all_NowCubeName;//用来累加当前所有图片的序列。  对比正确序列是否达到了

    int[] direction = new int[300];

    int em_Index;

    // Use this for initialization
    void Start () {
        ifShowTip = false;
		cube_S = Resources.LoadAll<GameObject> (Cube_Path);
        //cube1_S = Resources.LoadAll<GameObject>(Cube_Path);

        //random_Null = Random.Range (0, cube_S.Length);//随机一个空地位置
        //Debug.Log(random_Null);
        //cube_S [random_Null].SetActive (false);

        //创建一个用于存储所有Cube的list test_L
        List<GameObject> test_L = new List<GameObject> ();
		print (cube_S.Length);
		for (int i = 0; i < cube_S.Length; i++) {
			test_L.Add (cube_S [i]);

			//all_SpritName;//正确的图片名字相加
			all_CubeName += cube_S [i].name;
		}
		print ("正确的方块名称构成的顺序" + all_CubeName);
			
        //将test_L按照白块运动算法排序
        //生成300个1-4之间的随机数用于代表空块每次运动的方向

        int i1;
        for (int i = 0; i < 300; i++)
        {
            i1 = Random.Range(1, 5);
            //Debug.Log(i1);
            direction[i] = i1;
        }

        

        //根据direction[] 将 test_L[8]进行移动
        empty_Cube = test_L[test_L.Count - 1];
        em_Index = test_L.Count-1;
        GameObject huancun; 

        //让白块运动了10步
        for (int i=0;i<10;i++) {
            if (direction[i] == 1)
            {
                //向上移动
                //确定是否可以移动并获取移动后的index
                if (IfCanMove(direction[i]) == -1)
                {
                    //无法移动
                    Debug.Log("无法移动,此时空块的em_Index为"+em_Index+"返回的移动后index为"+ IfCanMove(direction[i]));
                }
                else
                {
                    //向上移动
                    //交换
                    huancun = test_L[IfCanMove(direction[i])];
                    test_L[IfCanMove(direction[i])] = test_L[em_Index];
                    test_L[em_Index] = huancun;
                    //改变em_Index的值
                    em_Index = IfCanMove(direction[i]);
                }



                
            }

            if (direction[i] == 2)
            {
                //向左移动
                //确定是否可以移动并获取移动后的index
                if (IfCanMove(direction[i]) == -1)
                {
                    //无法移动
                    Debug.Log("无法移动");
                }
                else
                {
                    //向上移动
                    //交换
                    huancun = test_L[IfCanMove(direction[i])];
                    test_L[IfCanMove(direction[i])] = test_L[em_Index];
                    test_L[em_Index] = huancun;
                    //改变em_Index的值
                    em_Index = IfCanMove(direction[i]);
                }



                
            }

            if (direction[i] == 3)
            {
                ///向下移动
                //确定是否可以移动并获取移动后的index
                if (IfCanMove(direction[i]) == -1)
                {
                    //无法移动
				}
                else
                {
                    //向上移动
                    //交换
                    huancun = test_L[IfCanMove(direction[i])];
                    test_L[IfCanMove(direction[i])] = test_L[em_Index];
                    test_L[em_Index] = huancun;
                    //改变em_Index的值
                    em_Index = IfCanMove(direction[i]);
                }					
                
            }
            if (direction[i] == 4)
            {
                //向右移动
                //确定是否可以移动并获取移动后的index
                if (IfCanMove(direction[i]) == -1)
                {
                    //无法移动
                }
                else
                {
                    //向上移动
                    //交换
                    huancun = test_L[IfCanMove(direction[i])];
                    test_L[IfCanMove(direction[i])] = test_L[em_Index];
                    test_L[em_Index] = huancun;
                    //改变em_Index的值
                    em_Index = IfCanMove(direction[i]);
                }    
            }
        }

		//int m = 0;
		for (int i =0; i <test_L.Count; i++) {

            //根据 test_L中 cube的顺序 初始化当前拼图方块
			GameObject instan_Cube = Instantiate (test_L [i]) as GameObject;
			//设置物体的位置
			instan_Cube.transform.position = cube_S [i].transform.position;
			//m++;

			//Button btn_ = instan_Btn.GetComponent<Button>();
			//btn_.onClick.AddListener(delegate() { this.Btn_OnClick(instan_Btn.GetComponent<RectTransform>()); });//添加按钮事件\

			//test_L.Remove (test_L [i1]);
			instan_Cube.transform.SetParent (bg_T);
		}
	}

	
	
	// Update is called once per frame
	void Update () {
        if (ifShowTip)
        {
            if (Input.GetKeyDown(KeyCode.F)) {
				jigsawCamera.GetComponent<Camera>().depth = 0;
                ifShowTip = false;
                bg_T.GetComponent<BoxCollider>().enabled = false;
            }
           
        }

		if (jigsawCamera.GetComponent<Camera>().depth == 0&&Input.GetKeyDown(KeyCode.Escape))
        {
            //fpsCon.SetActive(true);
			jigsawCamera.GetComponent<Camera>().depth = -2;
            //ifShowTip = true;
            bg_T.GetComponent<BoxCollider>().enabled = true;
        }

        if (Input.GetMouseButtonDown(0))
        {
            //从摄像机发出到点击坐标的射线
			Ray ray = jigsawCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                //划出射线，只有在scene视图中才能看到
                GameObject gameObj = hitInfo.collider.gameObject;
               
            }
        }

    }

	void OnDestroy(){

		//for (int i=0;i<cube_S.Length;i++)
		//{
			//cube_S [i].SetActive (true);
		//}
	}

    private void OnTriggerEnter(Collider other)
    {
		if (other.tag == "Player")
			ifShowTip = true;
		
    }

    private void OnTriggerExit(Collider other)
    {
		if (other.tag == "Player")
        ifShowTip = false;
    }

    private void OnGUI()
    {
        if (ifShowTip) {
            GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 500, 40), "按F键切换视角");
        }
       
    }

    int IfCanMove(int dr)
    {
        if (em_Index == 0) {
            if (dr == 1)
            {
                return -1;
            }
            if (dr == 2)
            {
                return -1;
            }
            if (dr == 3)
            {
                return 3;
            }
            if (dr == 4)
            {
                return 1;
            }

        }
        if (em_Index == 1) {
            if (dr == 1)
            {
                return -1;
            }
            if (dr == 2)
            {
                return 0;
            }
            if (dr == 3)
            {
                return 4;
            }
            if (dr == 4)
            {
                return 2;
            }
        }
        if (em_Index == 2) {
            if (dr == 1)
            {
                return -1;
            }
            if (dr == 2)
            {
                return 1;
            }
            if (dr == 3)
            {
                return 5;
            }
            if (dr == 4)
            {
                return -1;
            }
        }
        if (em_Index == 3) {
            if (dr == 1)
            {
                return 0;
            }
            if (dr == 2)
            {
                return -1;
            }
            if (dr == 3)
            {
                return 6;
            }
            if (dr == 4)
            {
                return 4;
            }
        }
        if (em_Index == 4) {
            if (dr == 1)
            {
                return 1;
            }
            if (dr == 2)
            {
                return 3;
            }
            if (dr == 3)
            {
                return 7;
            }
            if (dr == 4)
            {
                return 5;
            }
        }
        if (em_Index == 5) {
            if (dr == 1)
            {
                return 2;
            }
            if (dr == 2)
            {
                return 4;
            }
            if (dr == 3)
            {
                return 8;
            }
            if (dr == 4)
            {
                return -1;
            }
        }
        if (em_Index == 6) {
            if (dr == 1)
            {
                return 3;
            }
            if (dr == 2)
            {
                return -1;
            }
            if (dr == 3)
            {
                return -1;
            }
            if (dr == 4)
            {
                return 7;
            }
        }
        if (em_Index == 7) {
            if (dr == 1)
            {
                return 4;
            }
            if (dr == 2)
            {
                return 6;
            }
            if (dr == 3)
            {
                return -1;
            }
            if (dr == 4)
            {
                return 8;
            }
        }
        if (em_Index == 8) {
            if (dr == 1)
            {
                return 5;
            }
            if (dr == 2)
            {
                return 7;
            }
            if (dr == 3)
            {
                return -1;
            }
            if (dr == 4)
            {
                return -1;
            }
        }
        return -1;
    }
}
