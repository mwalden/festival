using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TapperUI : MonoBehaviour
{
    public Text message;
    public Image image;
    public Sprite[] images;
    public string[] messages;
    public int currentMessage;
    public GameObject panel;


    public void showUI()
    {
        image.sprite = images[currentMessage];
        message.text = messages[currentMessage];
        currentMessage++;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
