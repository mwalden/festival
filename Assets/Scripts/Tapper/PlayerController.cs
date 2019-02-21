using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject player;
    public GameObject[] points;
    public int currentPoint;
    // Start is called before the first frame update
    void Start()
    {
        player.transform.position = points[0].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (currentPoint + 1 < points.Length)
            {
                currentPoint++;
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (currentPoint - 1 >= 0)
            {
                currentPoint--;
            }
        }
        player.transform.position = points[currentPoint].transform.position;
    }
}
