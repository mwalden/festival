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
    private TapperUI ui;
    public bool showingUI;
    public Canvas uiCanvas;


    private void Start()
    {
        currentLevel = 0;
        currentTimeLeft = levelTime[0];
        ui = GetComponent<TapperUI>();
        foreach (GameObject spawner in userSpawners)
        {
            spawner.GetComponent<UserSpawner>().userSpeed = speedRatesPerLevel[currentLevel];
            spawner.GetComponent<UserSpawner>().setTimerBeforeSpawn(spawnRatesByLevel[currentLevel]);
        }
    }
    private void Update()
    {
        if (currentLevel == 5) {
            print("Game done");
            return;
        }

        if (showingUI)
        {
            return;
        }
        currentTimeLeft -= Time.deltaTime;
        if (currentTimeLeft < 0)
        {
            foreach(GameObject spawner in userSpawners)
            {
                spawner.GetComponent<UserSpawner>().setTimerBeforeSpawn(100000);
            }
            currentTimeDelay = 100000;
            openUI();
            UserScript[] users = GameObject.FindObjectsOfType<UserScript>();
            int count = users.Length;
            for(int x =0;x < count; x++)
            {
                DestroyImmediate(users[x].gameObject);
            }

        }
    }
    public void openUI()
    {
        showingUI = true;
        uiCanvas.gameObject.SetActive(true); 
        ui.showUI();
    }

    public void closeUI()
    {
        uiCanvas.gameObject.SetActive(false);
        currentLevel++;
        if (currentLevel < speedRatesPerLevel.Length)
        {
            foreach (GameObject spawner in userSpawners)
            {
                spawner.GetComponent<UserSpawner>().userSpeed = speedRatesPerLevel[currentLevel];
                spawner.GetComponent<UserSpawner>().setTimerBeforeSpawn(spawnRatesByLevel[currentLevel]);
            }
            currentTimeLeft = levelTime[currentLevel];
        }
        showingUI = false;
    }
}
