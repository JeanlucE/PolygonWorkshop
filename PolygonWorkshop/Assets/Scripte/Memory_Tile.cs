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
        //Wenn gerade 2 angezeigt werden keinen click akzeptieren
        //auskommentiert, da sonst spielstopp nach den ersten beiden tiles
        //if (GameObject.FindGameObjectWithTag("Overlay").GetComponent<Canvas>().enabled == true)
        //    return;
        Text t = GetComponentInChildren<Text>();
        t.text = titel;
        open = true;

        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Memorytile");
        foreach(GameObject go in tiles)
        {
            if(go.GetComponent<Memory_Tile>().open && go != this.gameObject)
            {
                GameObject.FindGameObjectWithTag("Overlay").GetComponent<Overlay>().make(go.GetComponent<Memory_Tile>().titel, titel);
                FlipBack();
                go.GetComponent<Memory_Tile>().FlipBack();
            }
        }
    }

    public void FlipBack()
    {
        GetComponentInChildren<Text>().text = "";
        open = false;
    }
}
