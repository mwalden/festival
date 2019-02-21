using UnityEngine;
using System.Collections;

public class BandScript : MonoBehaviour {
	private float endingY = 3.25f;
	private float startingY = 7.5f;
	public AudioSource song1;
	public AudioSource song2;
	public AudioSource song3;

	private bool bringOutBandNow = false;
	private bool takeAwayBandNow = false;

	private int songNumber;

	private AudioSource currentSong;
	private bool isPlaying;
	// Use this for initialization
	void Start () {
		Messenger.AddListener ("takeAwayBand", takeAwayBand);
		Messenger.AddListener ("bringOutBand", bringOutBand);
	}
	void takeAwayBand(){
		currentSong.Stop ();
		bringOutBandNow = false;
		takeAwayBandNow = true;
		isPlaying = false;
		if (songNumber == 2) {
			Messenger.Broadcast ("booSong");
			Messenger.Broadcast ("sad");
		}
		if (songNumber == 3)
			Messenger.Broadcast ("goHome");
	}

	void Update(){
		if (transform.position.y < startingY && takeAwayBandNow) {
			transform.position = new Vector3 (transform.position.x, transform.position.y + 3f * Time.deltaTime, transform.position.z);
		}
		if (transform.position.y > endingY && bringOutBandNow) {
			transform.position = new Vector3 (transform.position.x, transform.position.y - 3f * Time.deltaTime, transform.position.z);
		}
		if (transform.position.y < endingY && bringOutBandNow && !isPlaying) {
			if (songNumber == 0) {
				currentSong = song1;
			}
			if (songNumber == 1) {
				currentSong = song2;
			}
			if (songNumber == 2) {
				currentSong = song3;
			}
			currentSong.Play ();
			isPlaying = true;
			songNumber++;
		}

	}

	void bringOutBand(){
		takeAwayBandNow = false;
		bringOutBandNow = true;
	}
}
