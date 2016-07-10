using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Memory_Manager : MonoBehaviour {

    public static int score = 0;
    public static int openTiles = 0;
    //Debug only
    /*public static string[] titel = {
        "1a",
        "2a",
        "3a",
        "4a",
        "5a",
        "6a",
        "7a",
        "8a",
        "9a",
        "10a"
    };
    */
    
    public static string[] titel = {
       "Beet & Fennel Salad",
       "Charcuterie",
       "Dungeness Crab Cakes",
       "King Salmon",
       "Hanger Steak",
       "Rib Eye Steak",
       "Prawns",
       "Chocolate Slice",
       "Macaroons & Dipping Sauce",
       "Tiramisu",
       };
    
    // Use this for initialization
    void Start()
    {
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Memorytile");
        int[] ids = new int[20];
        for (int i = 0; i < 10; i++)
        {
            int rid = (int)(Random.value * 20) % 20;
            while (ids[rid] == 1)
            {
                rid = (int)(Random.value * 20) % 20;
            }
            ids[rid]++;
            tiles[rid].GetComponent<Memory_Tile>().titel = titel[i];
            tiles[rid].GetComponentInChildren<Text>().text = ""; // titel[i]; //Debug only
        }
        //start with 1
        for (int i = 1; i <= 10; i++)
        {
            int rid = (int)(Random.value * 20) % 20;
            while (ids[rid] == 1)
            {
                rid = (int)(Random.value * 20) % 20;
            }
            ids[rid]++;
            tiles[rid].GetComponent<Memory_Tile>().titel = "" + i;
            tiles[rid].GetComponentInChildren<Text>().text = "";// + i; //Debug only
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
