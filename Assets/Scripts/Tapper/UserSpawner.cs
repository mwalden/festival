using System.Collections;
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

    public void setTimerBeforeSpawn(float time)
    {
        timerBeforeSpawn = time;
        timeLeft = timerBeforeSpawn;
    }
}
