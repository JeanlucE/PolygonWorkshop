using UnityEngine;
using System.Collections;

public class CloseOverlay : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void closeOverlay()
    {
        gameObject.GetComponentInParent<Canvas>().enabled = false;
    }
}
