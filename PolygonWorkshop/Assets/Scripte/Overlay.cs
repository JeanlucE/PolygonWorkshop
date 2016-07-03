using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Overlay : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void make(string a, string b)
    {
        int ia;
        try
        {
            ia = int.Parse(a);
        }
        catch (Exception)
        {
            ia = -1;
        }
        if(ia != -1)
        {
            if(Memory_Manager.titel[ia].Equals(b))
            {
                //Match
                disableTiles(a, b);
            }
        }
        else
        {
            int ib;
            try
            {
                ib = int.Parse(b);

            }
            catch (Exception)
            {
                ib = -1;
            }
            if(ib != -1)
            {
                if(Memory_Manager.titel[ib].Equals(a))
                {
                    //Match
                    disableTiles(a, b);
                }
            }
        }
        gameObject.GetComponent<Canvas>().enabled = true;
        Text[] t = gameObject.GetComponentsInChildren<Text>();
        t[0].text = a;
        t[1].text = b;
    }

    private void disableTiles(string a, string b)
    {
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Memorytile");
        foreach(GameObject tile in tiles)
        {
            if (tile.GetComponent<Memory_Tile>().titel.Equals(a) || tile.GetComponent<Memory_Tile>().titel.Equals(b))
            {
                tile.SetActive(false);
            }
        }
        checkWin();

    }

    private void checkWin()
    {
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Memorytile");
        if (tiles.Length == 0)
            return; //WIN
    }
}
