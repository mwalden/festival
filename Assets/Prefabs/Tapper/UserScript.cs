using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserScript : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private bool state;
    public float speed;
    public Sprite happy;
    private Rigidbody2D rb;
    private Sprite angry;
    public bool hasTrack;
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        angry = spriteRenderer.sprite;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(-speed, 0);
        animator.speed = .5f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<TrackScript>() == null)
            return;

        float skipChance = Random.Range(0, 10);
        //if (skipChance < 2)
        //{

        //}
        //else
        {
            hasTrack = true;
            GameObject track = collision.gameObject;
            track.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            track.transform.parent = transform;
            track.transform.position = new Vector2(transform.position.x, transform.position.y + .75f);
            animator.enabled = false;
            spriteRenderer.sprite = happy;
            collision.enabled = false;
            rb.velocity = new Vector2(speed + 3, 0);
        }
    }
}
