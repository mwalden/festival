using UnityEngine;
using System.Collections;

public class DestinationScript : MonoBehaviour {
	public GameObject[] stops;

	public string name;
	// Use this for initialization
	void Start () {
		Messenger.AddListener<GameObject,string> ("setDestination", setDestination);
	}

	void setDestination(GameObject dude, string destination){
		
	}



}
