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

    //only use for the end game screen
    public void exitGame()
    {
        GameObject gamecontrol = GameObject.FindGameObjectWithTag("GameController");
        gamecontrol.GetComponent<GameManager>().AddPoints(Memory_Manager.score, 0);
    }
}
