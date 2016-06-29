using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Memory_Tile : MonoBehaviour {

    public string titel = "Debug";
    public bool open = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnClick()
    {
        Text t = GetComponentInChildren<Text>();
        t.text = titel;
        open = true;

        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Memorytile");
        foreach(GameObject go in tiles)
        {
            if(go.GetComponent<Memory_Tile>().open)
            {
                //display tiles
                //FlipBack();
                go.GetComponent<Memory_Tile>().FlipBack();
                return;
            }
        }
    }

    public void FlipBack()
    {
        GetComponentInChildren<Text>().text = "";
        open = false;
    }
}
