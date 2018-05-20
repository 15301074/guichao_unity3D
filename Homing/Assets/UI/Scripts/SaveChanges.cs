using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Xml;
using System.IO;
using System.Text.RegularExpressions;

public class SaveChanges : MonoBehaviour, IPointerClickHandler {

	string filePath;
	public Dropdown fblDropdown;
	public Slider bgmSlider, sfxSlider;
	public Toggle fullScreenToggle, bgmToggle, sfxToggle;

	public void OnPointerClick (PointerEventData eventData)
	{	
		// 保存游戏设置
		filePath = Application.dataPath + "/Resources/Config/settings.xml";
		updateXml ();
		changeScreen ();
	}

	/* 更新xml文件 */
	private void updateXml ()
	{
		if (File.Exists (filePath)) {
			// 创建xml文档
			XmlDocument xmlDoc = new XmlDocument ();
			xmlDoc.Load (filePath);
			XmlNodeList node = xmlDoc.SelectSingleNode ("settings").ChildNodes;
			// 遍历节点
			foreach (XmlElement ele in node) {
				// config下面的节点
				if (ele.Name == "setting1") {
					// 分辨率
					foreach (XmlElement i in ele.ChildNodes) {
						if (i.Name == "value") {
							i.InnerText = fblDropdown.value.ToString ();
						}
					}
				} else if (ele.Name == "setting2") {
					// 游戏音量
					foreach (XmlElement i in ele.ChildNodes) {
						if (i.Name == "value") {
							i.InnerText = bgmSlider.value.ToString ();
						}
					}
				} else if (ele.Name == "setting3") {
					// 游戏音效
					foreach (XmlElement i in ele.ChildNodes) {
						if (i.Name == "value") {
							i.InnerText = sfxSlider.value.ToString ();
						}
					}
				} else if (ele.Name == "setting4") {
					// 全屏
					foreach (XmlElement i in ele.ChildNodes) {
						if (i.Name == "value") {
							i.InnerText = fullScreenToggle.isOn.ToString ();
						}
					}
				} else if (ele.Name == "setting5") {
					// 音量静音
					foreach (XmlElement i in ele.ChildNodes) {
						if (i.Name == "value") {
							i.InnerText = bgmToggle.isOn.ToString ();
						}
					}
				} else if (ele.Name == "setting6") {
					// 音效静音
					foreach (XmlElement i in ele.ChildNodes) {
						if (i.Name == "value") {
							i.InnerText = sfxToggle.isOn.ToString ();
						}
					}
				} 
			}

			// 保存
			xmlDoc.Save (filePath);
		}
	}

	/* 改变屏幕分辨率 */
	private void changeScreen () {
		/* 得到设置的分辨率 */
		string option = fblDropdown.options [fblDropdown.value].text;	// option格式"1920*1080"
		string[] xy = option.Split('*');
		int x = int.Parse (xy [0]);
		int y = int.Parse (xy [1]);

		/* 修改分辨率 */
		Screen.SetResolution (x, y, fullScreenToggle.isOn);
	}
}
