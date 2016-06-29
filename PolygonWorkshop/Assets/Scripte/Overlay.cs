using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Overlay : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void make(string a, string b)
    {
        gameObject.GetComponent<Canvas>().enabled = true;
        Text[] t = gameObject.GetComponentsInChildren<Text>();
        t[0].text = a;
        t[1].text = b;
    }
}
