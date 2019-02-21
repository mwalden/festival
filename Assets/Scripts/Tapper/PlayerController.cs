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
    public bool currentHoldingTrack;
    // Start is called before the first frame update
    void Start()
    {
        player.transform.position = points[0].transform.position;
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
        if (Input.GetKey(KeyCode.Space))
        {
            holdingTime += Time.deltaTime;
            if (holdingTime > holdTime && !currentHoldingTrack)
            {
                Vector2 position = new Vector2(player.transform.position.x, player.transform.position.y + 2f); 
                currentHoldingTrack = true;
                holdingTrack = Instantiate(track, position, Quaternion.identity);
                holdingTrack.GetComponent<SpriteRenderer>().sprite = points[currentPoint].GetComponentInParent<Laptop>().trackSprite;
            }

        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            holdingTime = 0;
            if (currentHoldingTrack)
            {
                holdingTrack.transform.position = new Vector2(holdingTrack.transform.position.x, points[currentPoint].GetComponentInParent<Laptop>().transform.position.y);
                holdingTrack.GetComponent<Rigidbody2D>().velocity = new Vector2(4, 0);
            }
            currentHoldingTrack = false;
        }
        player.transform.position = points[currentPoint].transform.position;
    }
}
