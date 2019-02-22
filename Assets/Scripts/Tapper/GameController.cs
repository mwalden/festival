using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public int currentLevel;
    [Tooltip("User Spawners")]
    public GameObject[] userSpawners;
    [Tooltip("The speed per level that a user will move down the belt.")]
    public float[] speedRatesPerLevel;
    [Tooltip("How long the level will last.")]
    public float[] levelTime;
    [Tooltip("The spawn rate per level that a user will be created.")]
    public float[] spawnRatesByLevel;

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
    private bool gameOver;
    public bool startGameOver;
    public Canvas gameoverCanvas;
    public AudioSource audioSource;
    public AudioClip gameoverAudio;
    public AudioClip goodbye;
    public AudioSource backgroundAudioSource;
    public AudioClip[] backgroundSpeedMusic;
    public AudioSource soundEffects;
    public AudioClip godzilla;
    public AudioClip alarm;
    public GameObject gameoverColliders;
    private bool playedGodzilla;
    private bool playedAlert;
    private void Start()
    {
        currentLevel = 0;
        currentTimeLeft = 15;
        ui = GetComponent<TapperUI>();
        backgroundAudioSource.clip = backgroundSpeedMusic[0];
        backgroundAudioSource.Play();
        foreach (GameObject spawner in userSpawners)
        {
            spawner.GetComponent<UserSpawner>().userSpeed = speedRatesPerLevel[currentLevel];
            spawner.GetComponent<UserSpawner>().setTimerBeforeSpawn(spawnRatesByLevel[currentLevel]);
        }
    }
    private void Update()
    {
        if (startGameOver)
        {
            if (!gameOver)
            {
                destroyUsers();
                print("Game done");
                setGameOverMasks();
                audioSource.clip = goodbye;
                audioSource.Play();
                gameOver = true;
            }
            return;
        }

        if (showingUI)
        {
            return;
        }
        currentTimeLeft -= Time.deltaTime;
        if (currentTimeLeft < 2 && currentLevel == 2 && !playedGodzilla)
        {
            playedGodzilla = true;
            soundEffects.clip = godzilla;
            soundEffects.Play();
        }
        if (currentTimeLeft < 4 && currentLevel == 4 && !playedAlert)
        {
            gameoverColliders.SetActive(true);
            playedAlert = true;
            soundEffects.clip = alarm;
            soundEffects.Play();
        }

        if (currentTimeLeft < 0)
        {
            foreach (GameObject spawner in userSpawners)
            {
                spawner.GetComponent<UserSpawner>().setTimerBeforeSpawn(100000);
            }
            currentTimeDelay = 100000;
            openUI();
            destroyUsers();
        }
    }

    void destroyUsers()
    {
        print("Destroying users");
        UserScript[] users = GameObject.FindObjectsOfType<UserScript>();
        int count = users.Length;
        for (int x = 0; x < count; x++)
        {
            DestroyImmediate(users[x].gameObject);
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
        if (currentLevel == 1)
            player.GetComponent<PlayerController>().skipChancePercentage = 15;
        else
            player.GetComponent<PlayerController>().skipChancePercentage = 1;
        backgroundAudioSource.clip = backgroundSpeedMusic[currentLevel];
        backgroundAudioSource.Play();
    }

    void setGameOverMasks()
    {
        int point = player.GetComponent<PlayerController>().currentPoint;
        dude.GetComponent<Animator>().enabled = false;
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
        float time = 2;
        float destination = 0;

        do
        {
            destination = Mathf.Lerp(0, -90, currentTime / time);
            dude.transform.rotation = Quaternion.Euler(0, 0, destination);
            currentTime += Time.deltaTime;

            yield return null;
        } while (currentTime <= time);
        audioSource.clip = gameoverAudio;
        audioSource.Play();
        gameoverCanvas.gameObject.SetActive(true);
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

    public void setGameOver()
    {
        print("Setting game over ");
        if (!startGameOver)
        {
            destroyUsers();
            backgroundAudioSource.Stop();
            startGameOver = true;

        }
    }
}
