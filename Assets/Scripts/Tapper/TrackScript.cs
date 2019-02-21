using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackScript : MonoBehaviour
{

    public Sprite sprite;
    private SpriteRenderer spriteRenderer;
    public bool skipped;
    public AudioClip[] skipSounds;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    public void playSkippedTrackAudio()
    {
        AudioClip clip = skipSounds[Random.Range(0, skipSounds.Length - 1)];
        audioSource.clip = clip;
        audioSource.Play();
    }


}
