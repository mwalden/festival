using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Tooltip("User Spawners")]
    public GameObject[] userSpawners;
    [Tooltip("The speed per level that a user will move down the belt.")]
    public float[] speedRatesPerLevel;
    [Tooltip("How long the level will last.")]
    public float[] levelTime;
    [Tooltip("The spawn rate per level that a user will be created.")]
    public float[] spawnRatesByLevel;
    public int currentLevel;
    public float currentTimeLeft;
    [Tooltip("The time that user spawns are delayed between levels")]
    public float timeDelay;
    public float currentTimeDelay;

    private void Start()
    {
        currentLevel = 0;
        currentTimeLeft = levelTime[0];
    }
    private void Update()
    {
        if (currentLevel == 4) {
            print("Game done");
            return;
        }

        if (currentTimeDelay > 0) {
            currentTimeDelay -= Time.deltaTime;
            if (currentTimeDelay < 0) {
                currentLevel++;
                if (currentLevel < 3)
                {
                    foreach (GameObject spawner in userSpawners)
                    {
                        spawner.GetComponent<UserSpawner>().userSpeed = speedRatesPerLevel[currentLevel];
                        spawner.GetComponent<UserSpawner>().setTimerBeforeSpawn(spawnRatesByLevel[currentLevel]);
                    }
                    currentTimeLeft = levelTime[currentLevel];
                }
            }
            return;
        }
        currentTimeLeft -= Time.deltaTime;
        if (currentTimeLeft < 0)
        {
            foreach(GameObject spawner in userSpawners)
            {
                spawner.GetComponent<UserSpawner>().setTimerBeforeSpawn(100000);
            }
            currentTimeDelay = timeDelay;
        }
    }
}
