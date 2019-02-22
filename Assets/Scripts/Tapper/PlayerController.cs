using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject player;
    public GameObject[] points;
    public int currentPoint;
    public float holdTime = 1;
    public float holdingTime = 0;
    public GameObject track;
    private GameObject holdingTrack;
    private IEnumerator coroutine;
    public bool currentHoldingTrack;
    public AudioSource audioSource;
    public AudioClip[] downloadSounds;

    private int audioNumber;
    [Tooltip("this value * 10 for the skip chance percentage")]
    [Range(0,20)]
    public int skipChancePercentage;

    // Start is called before the first frame update
    void Start()
    {
        player.transform.position = points[0].transform.position;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            if (currentPoint + 1 < points.Length)
            {
                currentPoint++;
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            if (currentPoint - 1 >= 0)
            {
                currentPoint--;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!currentHoldingTrack)
                StartCoroutine(scaleTrack());

        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            holdingTime = 0;
            StopAllCoroutines();
            audioSource.Stop();
            if (audioNumber >= downloadSounds.Length - 1)
                audioNumber = 0;
            if (!currentHoldingTrack && holdingTrack != null)
            {
                Destroy(holdingTrack);
                return;
            }
            if (currentHoldingTrack)
            {
                holdingTrack.transform.position = new Vector2(holdingTrack.transform.position.x, points[currentPoint].GetComponentInParent<Laptop>().transform.position.y);
                holdingTrack.GetComponent<Rigidbody2D>().velocity = new Vector2(5, 0);
            }
            currentHoldingTrack = false;
        }
        player.transform.position = points[currentPoint].transform.position;
    }


    IEnumerator scaleTrack()
    {
        float time = 1f;
        Vector2 position = new Vector2(player.transform.position.x, player.transform.position.y + 2f);
        holdingTrack = Instantiate(track, position, Quaternion.identity);

        audioSource.clip = downloadSounds[audioNumber];
        audioNumber++;
        audioSource.Play();

        int skipChance = Random.Range(0, 20);
        print("Skip chance " + skipChance);
        holdingTrack.GetComponent<TrackScript>().skipped = skipChance <= skipChancePercentage;
        holdingTrack.GetComponent<SpriteRenderer>().sprite = points[currentPoint].GetComponentInParent<Laptop>().trackSprite;
        holdingTrack.transform.localScale = new Vector2(0, 0);
        float currentTime = 0.0f;
        Vector2 original = new Vector3(0, 0,0);
        Vector2 destination = new Vector3(1, 1,1);
        do
        {
            holdingTrack.transform.localScale = Vector3.Lerp(original, destination, currentTime / time);
            currentTime += Time.deltaTime;
            yield return null;
        } while (currentTime <= time);
        currentHoldingTrack = true;



        //while (holdingTime < holdTime && !currentHoldingTrack)
        //{
        //    holdingTime += Time.deltaTime;
        //    holdingTrack.transform.localScale = new Vector2(holdingTrack.transform.localScale.x + .01f, holdingTrack.transform.localScale.y + .01f);
        //}
        //currentHoldingTrack = true;
        //yield return new WaitForSeconds(1f);
    }
}
