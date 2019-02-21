using UnityEngine;
using System.Collections;

public class DudeScript : MonoBehaviour {
	public Vector3 destination;
	public bool newDestination;
	public float speed = 1.0F;
	private float startTime;
	private float journeyLength;
	private Vector3 startPosition;
	private Vector3 returnDestination;

	public bool reachedDestination;
	public float timeAtDestination = 5;
	public float timeAtDestinationRemaining;

	public bool hasReturnDestination;

	private int mapIndex;
	private int spotIndex; 
	public GameObject happy;
	public GameObject sad;
	private bool isDancing;
	private GameObject h;
	private GameObject s;
	private DanceScript dancer;
	public bool isSick;
	public bool stopMoving;

	// Use this for initialization
	void Start () {
		Messenger.AddListener ("goHome", goHome);
		Messenger.AddListener ("sad", emotionSad);
		Vector3 p = transform.position;
		h = (GameObject)Instantiate (happy, p, Quaternion.identity);
		s = (GameObject)Instantiate (sad, p, Quaternion.identity);
		h.transform.localPosition = (new Vector3 (p.x, p.y + 1.5f, 0));
		s.transform.localPosition = (new Vector3 (p.x, p.y + 1.5f, 0));
		h.transform.SetParent (transform);
		s.transform.SetParent (transform);
		h.SetActive (false);
		s.SetActive (false);
		dancer = gameObject.GetComponentInParent<DanceScript> ();
	}
	private bool showingEmo = false;
	void emotion(){
		showingEmo = true;
		float emotionRange = Random.Range (-10.0f, 10.0f);
		StartCoroutine (moveEmotion(emotionRange > 0));

	}

	public void emotionSad(){
		h.SetActive (false);
		StartCoroutine (moveEmotion(false));

	}
	GameObject emo;
	IEnumerator moveEmotion(bool isHappy){
		
		if (isHappy)
			emo = h;
		else
			emo = s;
		
		emo.SetActive (true);
		print("showing emotion");
		yield return new WaitForSeconds (4);
		emo.SetActive (false);
		showingEmo = false;

	}

	// Update is called once per frame
	void Update () {
		if (newDestination && transform.position != destination) {
			float distCovered = (Time.time - startTime) * speed;
			float fracJourney = distCovered / journeyLength;
			transform.position = Vector3.Lerp(startPosition, destination, fracJourney);
		}
		if (transform.position == destination) {
			
			print ("Destination reached");
			if (hasReturnDestination && destination == returnDestination)
				hasReturnDestination = false;
			reachedDestination = true;
			if (!hasReturnDestination && timeAtDestinationRemaining <= 0) {
				print (gameObject.name + " : Asking for something to do");
				if (isDancing) {
					print ("Should not be dancing");
					dancer.EndDance ();
					isDancing = false;
				}
				Messenger.Broadcast<GameObject> ("needSomething",gameObject);

			}
			if (!hasReturnDestination && timeAtDestinationRemaining > 0) {
				
				if (!isDancing){
					print ("Should be dancing");
					dancer.StartDance ();
					isDancing = true; 
				}
			} 
		}
		if (reachedDestination) {
			if (timeAtDestinationRemaining <= 0 && hasReturnDestination) {
				//time to go back
				print("returning home");
				if (isSick) {
					Messenger.Broadcast<GameObject> ("sendHome", gameObject);
					return;
				}
					
				Messenger.Broadcast<int,int> ("freeSpot", mapIndex, spotIndex);

				setDestination(returnDestination);
				if (!isSick)
					emotion ();
				if (mapIndex == 0)
					Messenger.Broadcast<GameObject> ("ateFood", gameObject);
				if (mapIndex == 0 || mapIndex == 1) {
					Messenger.Broadcast<int> ("addCash",100);
					Messenger.Broadcast ("coin");
				}
				
			} else {
				//still waiting
				timeAtDestinationRemaining -= Time.deltaTime;
			}
		}
	}

	public void setDestination(Vector3 vector, Vector3 returnDestination,int mapIndex, int spotIndex){
		destination = vector;
		timeAtDestinationRemaining = timeAtDestination;
		newDestination = true;
		reachedDestination = false;
		startPosition = this.transform.position;
		startTime = Time.time;
		this.returnDestination = returnDestination;
		hasReturnDestination = true;
		this.mapIndex = mapIndex;
		this.spotIndex = spotIndex;
		journeyLength = Vector3.Distance(startPosition, destination);
	}
	public void setDestination(Vector3 vector){
		destination = vector;
		reachedDestination = false;
		timeAtDestinationRemaining = timeAtDestination;
		newDestination = true;
		startPosition = this.transform.position;
		startTime = Time.time;
		journeyLength = Vector3.Distance(startPosition, destination);
	}

	public void goHome(){
		setDestination (new Vector3 (0, -10, 0));
	}




}
