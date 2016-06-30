﻿using UnityEngine;
using System.Collections;

public class Memory_Manager : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        string[] titel = {
        "Beef Tapar",
        "Jakobsmuscheln",
        "Riesengarnelen",
        "Ziegenkäse & Apfel",
        "Junge Blattsalate",
        "Beef Tea",

        "USDA Prime Beef",
        "Auswahl verschiedener dry-aged Steaks",
        "Spanferkelrücken",
        "Lammkarree \"Donald Russell\"",
        "Stubenküken \"Piri Piri\"",
        "Fleischplatte für 2 Personen",
        "Hummer",
        "Meeresfrüchte für 2 Personen",
        "Regionaler Fisch vom Forellenhof Nadler aus Eching bei München",

        "Key Lime Pie",
        "Créme Brûlée",
        "Regionale Bio-Käse-Auswahl" 
        };
        //...
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Memorytile");
        int[] ids = new int[36];
        for (int i = 0; i < 18; i++)
        {
            int rid = (int)(Random.value * 36) % 36;
            while (ids[rid] == 1)
            {
                rid = (int)(Random.value * 36) % 36;
            }
            ids[rid]++;
            tiles[rid].GetComponent<Memory_Tile>().titel = titel[i];
        }
        for (int i = 0; i < 18; i++)
        {
            int rid = (int)(Random.value * 36) % 36;
            while (ids[rid] == 1)
            {
                rid = (int)(Random.value * 36) % 36;
            }
            ids[rid]++;
            tiles[rid].GetComponent<Memory_Tile>().titel = "" + i;
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}