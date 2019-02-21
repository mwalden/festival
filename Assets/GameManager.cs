using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public GameObject[] musicStops;
	public GameObject[] foodStops;
	public GameObject[] medicalStops;
	public GameObject[] bathroomStops;
	public GameObject[] merchandiseStops;

	public Dictionary<int,bool> musicStopMap;
	private Dictionary<int,bool> foodStopMap;
	private Dictionary<int,bool> merchStopMap;
	private Dictionary<int,bool> bathroomStopMap;
	private Dictionary<int,bool> medicalStopMap;

	public AudioSource booClip1;
	public AudioSource booClip2;

	private float timePassed = 0;

	public List<GameObject> dudes;
	private bool goingHome;

	// Use this for initialization
	void Start () {
		musicStopMap = new Dictionary<int,bool> ();
		foodStopMap = new Dictionary<int,bool> ();
		merchStopMap = new Dictionary<int,bool> ();
		bathroomStopMap = new Dictionary<int,bool> ();
		medicalStopMap = new Dictionary<int,bool> ();
		initMap (musicStopMap, musicStops.Length);
		initMap (foodStopMap, foodStops.Length);
		initMap (merchStopMap, merchandiseStops.Length);
		initMap (bathroomStopMap, bathroomStops.Length);
		initMap (medicalStopMap, medicalStops.Length);
		dudes = new List<GameObject> ();
		Messenger.AddListener<GameObject> ("spawnDude", handlerNewDude);
		Messenger.AddListener<GameObject> ("ateFood", didDudeGetSick);
		Messenger.AddListener<GameObject> ("needSomething", giveThemSomething);
		Messenger.AddListener<GameObject> ("sendHome", sendHome);
		Messenger.AddListener<int,int> ("freeSpot", freeSpot);
		Messenger.AddListener ("goHome", goHome);
		Messenger.AddListener ("booSong", booSong);
	}
	bool firstBand = false;
	bool secondBand = false;
	bool thirdBand = false;

	bool firstBandGone = false;
	bool secondBandGone = false;
	bool thirdBandGone = false;
	bool announcedPosters = false;
	bool marketingAnnouncement = false;

	void Update(){
		timePassed += Time.deltaTime;


		if (goingHome && timePassed > 6) {
			SceneManager.LoadScene (4);
			return;
		}
		if (goingHome) {
			return;
		}

		if (timePassed >= 0 && !marketingAnnouncement) {
			Messenger.Broadcast<int> ("announcement", 10);
			marketingAnnouncement = true;
		}


		if (timePassed >= 5 && !firstBand) {
			Messenger.Broadcast<int> ("announcement", 1);
			Messenger.Broadcast ("bringOutBand");
			firstBand = true;
		}

		if (timePassed >= 20 && !firstBandGone) {
			Messenger.Broadcast ("takeAwayBand");
			firstBandGone = true;
		}
		if (timePassed >= 30 && !secondBand) {
			Messenger.Broadcast<int> ("announcement", 2);
			Messenger.Broadcast ("bringOutBand");
			secondBand = true;
		}
		if (timePassed >= 37 && !announcedPosters) {
			announcedPosters = true;
			Messenger.Broadcast<int> ("announcement", 9);
		}

		if (timePassed >= 45 && !secondBandGone) {
			Messenger.Broadcast ("takeAwayBand");
			secondBandGone = true;
		}
		if (timePassed >= 55 && !thirdBand) {
			Messenger.Broadcast<int> ("announcement", 7);
			Messenger.Broadcast ("bringOutBand");
			thirdBand = true;
		}

		if (timePassed >= 70 && !thirdBandGone) {
			Messenger.Broadcast ("takeAwayBand");

			thirdBandGone = true;
		}
			
	}

	void giveThemSomething(GameObject dude){
		int n = Random.Range (0, 3);
		Dictionary<int,bool> map = getMapFromIndex (n);
		GameObject[] stops = getStopsIndex (n);
		sendDudeSomewhere (map, stops, dude,n);
			
	}

	GameObject[] getStopsIndex(int n){
		if (n == 0)
			return foodStops;
		else if (n == 1)
			return merchandiseStops;
		else if (n == 2)
			return bathroomStops;
		else if (n == 3)
			return medicalStops;

		return null;
	}

	Dictionary<int,bool> getMapFromIndex(int n){
		if (n == 0)
			return foodStopMap;
		else if (n == 1)
			return merchStopMap;
		else if (n == 2)
			return bathroomStopMap;
		else if (n == 3)
			return medicalStopMap;

		return null;
	}
	//map index is used for the dude to tell teh manager what map its using and what stop it is at 
	//in order to return that spot as available in the Map once it leaves;
	void sendDudeSomewhere(Dictionary<int,bool> map,GameObject[] stops,GameObject dude,int mapIndex){
		int index = getNextIndex (map);
		if (index == -1)
			return;
		GameObject destination = stops [index];
		Vector3 stopPosition = destination.transform.position;
		setTaken (map, index);
		dude.GetComponent<DudeScript> ().setDestination (stopPosition,dude.transform.position,mapIndex,index);
	}

	void freeSpot(int mapIndex, int stopIndex){
		Dictionary<int,bool> map = getMapFromIndex (mapIndex);
		map [stopIndex] = true;
	}

	void handlerNewDude(GameObject gameObject){
		int index = getNextIndex (musicStopMap);
		GameObject destination = musicStops [index];
		Vector3 stopPosition = destination.transform.position;
		setTaken (musicStopMap, index);
		dudes.Add (gameObject);
		gameObject.GetComponent<DudeScript> ().setDestination (stopPosition);
	}

	void initMap(Dictionary<int,bool> map, int totalStops){
		for (int x = 0; x < totalStops; x++) {
			map.Add (x, true);
		}
	}

	int getNextIndex(Dictionary<int,bool> map){
		for (int x = 0; x < map.Keys.Count; x++) {
			if (map[x] == true) {
				return x;
			}
		}
		return -1;
	}

	void setTaken(Dictionary<int,bool> map,int index){
		map [index] = false;
	}

	void setOpen(Dictionary<int,bool> map,int index){
		
		map [index] = true;
	}
	bool enoughPeopleSick;
	int sickPeople = 0;
	void didDudeGetSick(GameObject dude){
		if (enoughPeopleSick)
			return;

		sickPeople++;
		if (sickPeople >= 2)
			enoughPeopleSick = true;

		Messenger.Broadcast<int>("announcement",4);
		SpriteRenderer rends = dude.GetComponentInChildren<SpriteRenderer> ();
		DudeScript dudeScript = dude.GetComponent<DudeScript> ();
		if (dudeScript.isSick)
			return;
		dudeScript.emotionSad ();
		rends.material.color = new Color (0, 1, 0);
		rends.sortingOrder = 1;
		Dictionary<int,bool> map = getMapFromIndex (3);
		GameObject[] stops = getStopsIndex (3);
		changeDirection (map, stops, dude,3);
		dudeScript.isSick = true;

	}

	void changeDirection(Dictionary<int,bool> map,GameObject[] stops,GameObject dude,int mapIndex){
		int index = getNextIndex (map);
		if (index == -1)
			return;
		GameObject destination = stops [index];
		Vector3 stopPosition = destination.transform.position;
		setTaken (map, index);
		dude.GetComponent<DudeScript> ().setDestination (stopPosition);
	}

	void sendHome(GameObject dude){
		dude.GetComponent<DudeScript> ().setDestination (new Vector3(0,-10,0));
	}


	void goHome(){
		goingHome = true;
		timePassed = 0;
		Messenger.Broadcast<int> ("announcement", 11);
	}

	void booSong(){
		booClip1.Play ();
		booClip2.Play ();
	}
}
