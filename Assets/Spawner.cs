using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {
	public GameObject[] people;
	public float timeBetweenSpawn;
	public bool createNewDude;
	private float timeRemaining;
	private List<GameObject> peopleList;
	private int currentDude;
	bool enabled = true;
	private int dudeCount = 0;
	public GameObject dancingPrefab;

	private bool startingPeople = true;
	void Start () {
		timeRemaining = timeBetweenSpawn;
	}
	
	// Update is called once per frame
	void Update () {
		if (!enabled)
			return;
//		if (dudeCount == 1)
//			enabled = false;
		if (dudeCount >= people.Length * 2-3)
			return;

		if (startingPeople) {
			GameObject go = people [currentDude];
			currentDude++;
			dudeCount++;
			if (currentDude == 11)
				currentDude = 0;
			GameObject dude = Instantiate(go,new Vector3(gameObject.transform.localPosition.x,gameObject.transform.localPosition.y,0),Quaternion.identity) as GameObject;
			GameObject wrapper = Instantiate(dancingPrefab,new Vector3(0,0,0),Quaternion.identity) as GameObject;
			dude.transform.SetParent (wrapper.transform);
			Messenger.Broadcast<GameObject> ("spawnDude", dude);
			Messenger.Broadcast<int> ("addCash",1000);
			if (currentDude == 8)
				startingPeople = false;
		}

		timeRemaining -= Time.deltaTime;
		if (timeRemaining <= 0)
			createNewDude = true;
		if (createNewDude) {
			createNewDude = false;
			timeRemaining = timeBetweenSpawn;
			GameObject go = people [currentDude];
			currentDude++;
			dudeCount++;
			if (currentDude == 11)
				currentDude = 0;
			GameObject dude = Instantiate(go,new Vector3(gameObject.transform.localPosition.x,gameObject.transform.localPosition.y,0),Quaternion.identity) as GameObject;
			GameObject wrapper = Instantiate(dancingPrefab,new Vector3(0,0,0),Quaternion.identity) as GameObject;
			dude.transform.SetParent (wrapper.transform);
			Messenger.Broadcast<GameObject> ("spawnDude", dude);
			Messenger.Broadcast<int> ("addCash",1000);

		}
	}
}
