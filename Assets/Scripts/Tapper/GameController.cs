﻿using System.Collections;
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
    public GameObject player;
    public GameObject gameoverSprite;
    //public GameObject gameoverSpriteAlpha;
    public GameObject[] masks;
    public GameObject dude;


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
        if (currentLevel == 1)
        {
            print("Game done");
            setGameOverMasks();
            return;
        }

        if (showingUI)
        {
            return;
        }
        currentTimeLeft -= Time.deltaTime;
        if (currentTimeLeft < 0)
        {
            foreach (GameObject spawner in userSpawners)
            {
                spawner.GetComponent<UserSpawner>().setTimerBeforeSpawn(100000);
            }
            currentTimeDelay = 100000;
            openUI();
            UserScript[] users = GameObject.FindObjectsOfType<UserScript>();
            int count = users.Length;
            for (int x = 0; x < count; x++)
            {
                DestroyImmediate(users[x].gameObject);
            }

        }
    }
    public void openUI()
    {
        if (currentLevel == speedRatesPerLevel.Length)
            return;
        showingUI = true;
        uiCanvas.gameObject.SetActive(true);
        ui.showUI();
    }

    public void closeUI()
    {
        uiCanvas.gameObject.SetActive(false);

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
        currentLevel++;
    }

    void setGameOverMasks()
    {
        int point = player.GetComponent<PlayerController>().currentPoint;
        gameoverSprite.gameObject.SetActive(true);
        masks[point].gameObject.SetActive(true);
        dude.transform.position = masks[point].transform.position;
        StartCoroutine(exposeMask());
        StartCoroutine(rotatePlayer());
        foreach (GameObject spawner in userSpawners)
        {
            spawner.GetComponent<UserSpawner>().setTimerBeforeSpawn(1000000);
        }
    }
    IEnumerator rotatePlayer()
    {
        float currentTime = 0.0f;
        float time = 1;

        Quaternion original = dude.transform.rotation;
        Quaternion destination = new Quaternion(0, 0, -90, 0);
        do
        {
            dude.transform.rotation = Quaternion.Lerp(original, destination, 1 - (time / currentTime));
            yield return null;
        } while (currentTime <= time);
    }
    IEnumerator exposeMask()
    {
        float alpha = 0;
        float currentTime = 0.0f;
        float time = 1;
        do
        {
            alpha = Mathf.Lerp(0, 1, currentTime / time);
            gameoverSprite.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, alpha);
            currentTime += Time.deltaTime;
            yield return null;
        } while (currentTime <= time);
    }
}
