using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Xml;
using System.IO;

public class LoadConfigs : MonoBehaviour {

	string filePath;
	public Dropdown fblDropdown, fblDropdown2;
	public Slider bgmSlider, sfxSlider, bgmSlider2, sfxSlider2;
	public Toggle fullScreenToggle, bgmToggle, sfxToggle, fullScreenToggle2, bgmToggle2, sfxToggle2;
	public Text nowHP, maxHP, nowMP, maxMP, atk, moveSpeed, atkSpeed;

	//[RuntimeInitializeOnLoadMethod]
	private void Load () {
		XmlDocument xmlDoc = new XmlDocument();			// 创建xml文档

		filePath = Application.dataPath+"/Resources/Config/settings.xml";
		if (!File.Exists (filePath))
			return;
		xmlDoc.Load(filePath);

		/* 游戏设置 */
		XmlNodeList node = xmlDoc.SelectSingleNode("settings").ChildNodes;
		foreach(XmlElement ele in node){	// 遍历节点
			if(ele.Name == "setting1"){
				// 分辨率
				foreach(XmlElement i in ele.ChildNodes){
					if(i.Name == "value"){
						fblDropdown.value = int.Parse(i.InnerText);
						fblDropdown2.value = int.Parse(i.InnerText);
					}
				}
			} else if(ele.Name == "setting2"){
				// 游戏音乐
				foreach(XmlElement i in ele.ChildNodes){
					if(i.Name == "value"){
						bgmSlider.value = float.Parse(i.InnerText);
						bgmSlider2.value = float.Parse(i.InnerText);
					}
				}
			} else if(ele.Name == "setting3"){
				// 游戏音效
				foreach(XmlElement i in ele.ChildNodes){
					if(i.Name == "value"){
						sfxSlider.value = float.Parse(i.InnerText);
						sfxSlider2.value = float.Parse(i.InnerText);
					}
				}
			} else if(ele.Name == "setting4"){
				// 全屏
				foreach(XmlElement i in ele.ChildNodes){
					if(i.Name == "value"){
						fullScreenToggle.isOn = bool.Parse(i.InnerText);
						fullScreenToggle2.isOn = bool.Parse(i.InnerText);
					}
				}
			} else if(ele.Name == "setting5"){
				// 音乐静音
				foreach(XmlElement i in ele.ChildNodes){
					if(i.Name == "value"){
						bgmToggle.isOn = bool.Parse(i.InnerText);
						bgmToggle2.isOn = bool.Parse(i.InnerText);
					}
				}
			} else if(ele.Name == "setting6"){
				// 音效静音
				foreach(XmlElement i in ele.ChildNodes){
					if(i.Name == "value"){
						sfxToggle.isOn = bool.Parse(i.InnerText);
						sfxToggle2.isOn = bool.Parse(i.InnerText);
					}
				}
			} 
		}

		filePath = Application.dataPath+"/Resources/Config/attributes.xml";
		if (!File.Exists (filePath))
			return;
		xmlDoc.Load(filePath);

		/* 角色属性 */
		XmlNodeList node2 = xmlDoc.SelectSingleNode("attributes").ChildNodes;
		foreach(XmlElement ele in node2){	// 遍历节点
			if(ele.Name == "hp"){
				// 生命值
				foreach(XmlElement i in ele.ChildNodes){
					if(i.Name == "value"){
						nowHP.text = i.InnerText;
						maxHP.text = i.InnerText;
					}
				}
			} else if(ele.Name == "mp"){
				// 魔法值
				foreach(XmlElement i in ele.ChildNodes){
					if(i.Name == "value"){
						nowMP.text = i.InnerText;
						maxMP.text = i.InnerText;
					}
				}
			} else if(ele.Name == "atk"){
				// 攻击力
				foreach(XmlElement i in ele.ChildNodes){
					if(i.Name == "value"){
						atk.text = i.InnerText;
					}
				}
			} else if(ele.Name == "ms"){
				// 移速
				foreach(XmlElement i in ele.ChildNodes){
					if(i.Name == "value"){
						moveSpeed.text = i.InnerText;
					}
				}
			} else if(ele.Name == "as"){
				// 攻速
				foreach(XmlElement i in ele.ChildNodes){
					if(i.Name == "value"){
						atkSpeed.text = i.InnerText;
					}
				}
			}  
		}

		/* 得到设置的分辨率 */
		string option = fblDropdown.options [fblDropdown.value].text;	// option格式"1920*1080"
		string[] xy = option.Split('*');
		int x = int.Parse (xy [0]);
		int y = int.Parse (xy [1]);

		/* 修改分辨率 */
		Screen.SetResolution (x, y, fullScreenToggle.isOn);
	}
}
