using UnityEngine;
using System.Collections;

public class Annoucer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Dialoguer.Initialize ();
		Messenger.AddListener<int> ("announcement", handleAnnouncement);
	}
	void handleAnnouncement(int number){
		Dialoguer.StartDialogue (number);
	}
}
