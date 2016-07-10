using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Memory_Manager : MonoBehaviour {

    public static int score = 0;
    public static string[] titel = {
        "Beet & Fennel Salad",
        "Charcuterie",
        "Dungeness Crab Cakes",
        "King Salmon",
        "Hanger Steak",
        "Rib Eye Steak",
        "Chocolate Slice",
        "Prawns",
        "Macaroons & Dipping Sauce",
        "Tiramisu",
    };
     /*public static string[] titel = {
        "Beef Tapar",
        "Jakobsmuscheln",
        "Riesengarnelen",

        "USDA Prime Beef",
        "Spanferkelrücken",
        "Hummer",
        "Meeresfrüchte für 2 Personen",

        "Key Lime Pie",
        "Créme Brûlée",
        "Regionale Bio-Käse-Auswahl"
        };
    */
     //...

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
            tiles[rid].GetComponentInChildren<Text>().text = titel[i]; //Debug only
        }
        //for (int i = 0; i < 18; i++)
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
            tiles[rid].GetComponentInChildren<Text>().text = "" + i; //Debug only
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
