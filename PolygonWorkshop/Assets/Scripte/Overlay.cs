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
        Debug.Log("make");
        //check if a is a number
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
            Debug.Log("make a: " + a + " is a number");
            //check if the tiles match
            if (Memory_Manager.titel[ia - 1].Equals(b))
            {
                Debug.Log("match");
                //Match
                disableTiles(a, b);
                Memory_Manager.score += 10;
                Debug.Log("Score: " + Memory_Manager.score);
            }
            else
            {
                Memory_Manager.score =(int)  Math.Ceiling(0.9f * ((float) Memory_Manager.score));
                Debug.Log("Score: " + Memory_Manager.score);
            }
        }
        //check if b is a number
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
                Debug.Log("make b: " + b + " is a number");
                //check if the tiles match
                if (Memory_Manager.titel[ib - 1].Equals(a))
                {
                    Debug.Log("match");
                    //Match
                    disableTiles(a, b);
                    Memory_Manager.score += 10;
                    Debug.Log("Score: " + Memory_Manager.score);
                }
                else
                {
                    Memory_Manager.score = (int) Math.Ceiling(0.9f * ((float)Memory_Manager.score));
                    Debug.Log("Score: " + Memory_Manager.score);
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
        Debug.Log("disable Tiles: " + a + "   " + b);
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
        Debug.Log("Check win");
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Memorytile");
        if (tiles.Length == 0)
        {
            Debug.Log("Win!");
            GameObject endScreen = GameObject.FindGameObjectWithTag("EndScreen");
            endScreen.GetComponent<EndScreen>().displayEndScreen(Memory_Manager.score);
            GameObject gamecontrol = GameObject.FindGameObjectWithTag("GameController");
            gamecontrol.GetComponent<GameManager>().AddPoints(Memory_Manager.score, 0);
            return; //WIN
        }
    }
}
