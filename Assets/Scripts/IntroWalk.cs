using UnityEngine;
using System.Collections;

public class IntroWalk : MonoBehaviour {
	
	float distance = 2.5f;
	public float speed = 1.0F;
	private float startTime;
	public int dialogNumber;
	private Vector3 startPosition;
	private Vector3 destination;
	private bool atPosition;
	private bool showGuyAndTitle;
	void Awake () {
		Dialoguer.Initialize ();
		startTime = Time.time;
		startPosition = transform.position;
		destination = new Vector3 (startPosition.x + 2, startPosition.y, startPosition.z);
	}

	void Update () {
		float distCovered = (Time.time - startTime) * speed;
		float fracJourney = distCovered / distance;
		transform.position = Vector3.Lerp (startPosition, destination, fracJourney);
		if (transform.position.x == destination.x) {
			if (!atPosition) {
				Dialoguer.StartDialogue (dialogNumber);
				print ("Showing disable");
				atPosition = true;
			}
		}
	}
}
