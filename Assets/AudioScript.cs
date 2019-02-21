using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class AudioScript : MonoBehaviour {

	public AudioSource audioSource;
	public int sceneNumber;

	bool didItStart = false;

	void Start(){
		Messenger.AddListener ("playAudio", playAudio);
	}

	void Update(){
		if (didItStart) {
			if (!audioSource.isPlaying)
				SceneManager.LoadScene (1);
		}
			
	}
	public void playAudio(){
		audioSource.Play ();
		didItStart = true;
	}
}
