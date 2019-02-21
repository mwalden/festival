using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI.Extensions;
using UnityEngine.SceneManagement;

public class BossSelector : MonoBehaviour {
	public GameObject selectPrefab;
	public HorizontalScrollSnap scroll;

	void Start(){

	}


	public void onClick(){
		SceneManager.LoadScene (2);
	}

	public void startFestival(){
		SceneManager.LoadScene (3);
	}
}
