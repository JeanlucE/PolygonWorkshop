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
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Memorytile");
        if (Memory_Manager.openTiles == 2)
        {
            foreach (GameObject go in tiles)
            {
                if (go.GetComponent<Memory_Tile>().open)
                {
                    go.GetComponent<Memory_Tile>().FlipBack();
                }
            }
            Memory_Manager.openTiles = 0;
        }

        Text t = GetComponentInChildren<Text>();
        t.text = titel;
        open = true;
        Memory_Manager.openTiles++;

        if (Memory_Manager.openTiles == 2)
        {
            foreach (GameObject go in tiles)
            {
                if (go.GetComponent<Memory_Tile>().open && go != this.gameObject)
                {
                    GameObject.FindGameObjectWithTag("Overlay").GetComponent<Overlay>().make(go.GetComponent<Memory_Tile>().titel, titel);
                }
            }
        }
    }

    public void FlipBack()
    {
        GetComponentInChildren<Text>().text = "";
        open = false;
    }
}
