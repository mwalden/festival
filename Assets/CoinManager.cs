using UnityEngine;
using System.Collections;

public class CoinManager : MonoBehaviour {
	public AudioSource coinPlay;
	// Use this for initialization
	void Start () {
		Messenger.AddListener ("coin", playCoin);
		coinPlay = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void playCoin () {
		coinPlay.Play ();
	}
}
