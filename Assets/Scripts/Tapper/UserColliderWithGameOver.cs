using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserColliderWithGameOver : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("Game over collided");
        GameController gamecontroller = FindObjectOfType<GameController>();
        gamecontroller.setGameOver();
    }
}
