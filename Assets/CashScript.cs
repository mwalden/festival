using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CashScript : MonoBehaviour {
	Text cashText;
	int totalCash;
	// Use this for initialization
	void Start () {
		cashText = GetComponent<Text> ();
		Messenger.AddListener<int> ("addCash", addCash);
	}
	
	// Update is called once per frame
	void addCash (int cash) {
		totalCash += cash;
		cashText.text = "Cash: " + totalCash;
	}
}
