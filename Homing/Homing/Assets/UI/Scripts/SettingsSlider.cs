using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsSlider : MonoBehaviour {

	public Slider slider;
	public Text text;
	public AudioSource BGM;
	public AudioSource SFC;
	public AudioSource PlayerAudio;

	public void changeTextAndBGM() {
		
		text.text = Mathf.Ceil(slider.value * 100) + "%";
		BGM.volume = slider.value;
	}

	public void changeTextAndSFX() {
		text.text = Mathf.Ceil(slider.value * 100) + "%";
		SFC.volume = slider.value;
		PlayerAudio.volume = slider.value;
	}
}
