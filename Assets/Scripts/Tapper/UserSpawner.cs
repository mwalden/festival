﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserSpawner : MonoBehaviour
{
    public GameObject[] users;
    [Tooltip("Spawn rate")]
    public float timerBeforeSpawn;
    [Tooltip("Time left to spawn")]
    private float timeLeft;
    private float scaleSize;
    private List<GameObject> userList = new List<GameObject>();
    public float userSpeed;


    // Start is called before the first frame update
    void Start()
    {
        timeLeft = timerBeforeSpawn + Random.Range(0, 1);
        userList = new List<GameObject>(users);
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            int rand = Random.Range(0, userList.Count - 1);
            GameObject go = userList[rand];
            GameObject user = Instantiate(go, transform.position, Quaternion.identity);
            user.GetComponent<UserScript>().speed = userSpeed;
            timeLeft = timerBeforeSpawn + Random.Range(0,1);
        }
    }
    //weee hacks! 
    //this is so the cheating way of pausing the spawns will still work
    public void setTimerBeforeSpawn(float time)
    {
        timerBeforeSpawn = time;
        if (time > 99999)
            timeLeft = time;
        else timeLeft = .5f;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("colliding");
        if (collision.GetComponent<UserScript>() != null && collision.GetComponent<UserScript>().hasTrack == false)
            return;
        else
            Destroy(collision.gameObject);
    }
}
