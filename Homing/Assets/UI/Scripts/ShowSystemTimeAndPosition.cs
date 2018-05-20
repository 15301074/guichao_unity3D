using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ShowSystemTimeAndPosition : MonoBehaviour {

	public Text TimeText;
	public Text PositionText;
	private GameObject light;

	void Start() {
		light = GameObject.FindGameObjectWithTag ("DirectionalLight");
	}

	void Update () {
		DateTime dt = DateTime.Now;
		TimeText.text = dt.ToShortTimeString().ToString();	// HH:mm:ss 时间
		PositionText.text = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position.ToString();	// 位置
		//changeLight(dt.Hour);		// 光照
	}

	private void changeLight(int hour) {
		float i = 1 - Math.Abs(12 - hour) / 12f;
		this.light.GetComponent<Light> ().intensity = i;
	}	
}
