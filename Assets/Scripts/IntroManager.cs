using UnityEngine;
using System.Collections;

public class IntroManager : MonoBehaviour {
	public GameObject guy;
	public GameObject letters;


	private bool showGuyAndTitle;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame	
	void Update () {
		if (Input.GetKeyDown ("space")) {
			showGuyAndTitle = true;
			letters.SetActive (true);
			guy.SetActive (true);
			letters.GetComponent<Animator> ().Play ("title");
		}
	}
}
