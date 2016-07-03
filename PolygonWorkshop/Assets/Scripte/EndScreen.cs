using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class EndScreen : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        gameObject.GetComponent<Canvas>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }    

    public void displayEndScreen(double score)
    {
        gameObject.GetComponent<Canvas>().enabled = true;
        Text[] t = gameObject.GetComponentsInChildren<Text>();
        t[0].text = "Score: " + score;
    }
}
