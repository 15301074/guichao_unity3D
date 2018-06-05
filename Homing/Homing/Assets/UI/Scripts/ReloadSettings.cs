using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Xml;
using System.IO;
using UnityEngine.EventSystems;

public class ReloadSettings : MonoBehaviour, IPointerClickHandler {

	string filePath;
	public Dropdown fblDropdown;
	public Slider bgmSlider, sfxSlider;
	public Toggle fullScreenToggle, bgmToggle, sfxToggle;

	void Start () {
		loadSettingsXml();
	}

	public void OnPointerClick (PointerEventData eventData)
	{	
		// 保存游戏设置
		filePath = Application.dataPath + "/Resources/Config/settings.xml";
		loadSettingsXml ();
	}

	private void loadSettingsXml(){
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
					}
				}
			} else if(ele.Name == "setting2"){
				// 游戏音乐
				foreach(XmlElement i in ele.ChildNodes){
					if(i.Name == "value"){
						bgmSlider.value = float.Parse(i.InnerText);
					}
				}
			} else if(ele.Name == "setting3"){
				// 游戏音效
				foreach(XmlElement i in ele.ChildNodes){
					if(i.Name == "value"){
						sfxSlider.value = float.Parse(i.InnerText);
					}
				}
			} else if(ele.Name == "setting4"){
				// 全屏
				foreach(XmlElement i in ele.ChildNodes){
					if(i.Name == "value"){
						fullScreenToggle.isOn = bool.Parse(i.InnerText);
					}
				}
			} else if(ele.Name == "setting5"){
				// 音乐静音
				foreach(XmlElement i in ele.ChildNodes){
					if(i.Name == "value"){
						bgmToggle.isOn = bool.Parse(i.InnerText);
					}
				}
			} else if(ele.Name == "setting6"){
				// 音效静音
				foreach(XmlElement i in ele.ChildNodes){
					if(i.Name == "value"){
						sfxToggle.isOn = bool.Parse(i.InnerText);
					}
				}
			} 
		}
	}
}
