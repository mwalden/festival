using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackScript : MonoBehaviour
{

    public Sprite sprite;
    private SpriteRenderer spriteRenderer;
    public bool skipped;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   

}
