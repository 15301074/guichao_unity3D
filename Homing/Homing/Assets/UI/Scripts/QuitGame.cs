﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class QuitGame : MonoBehaviour, IPointerClickHandler {

	public void OnPointerClick(PointerEventData eventData){
		Application.Quit ();
	}

}