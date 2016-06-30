using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    public double ep;
    public double lvl;
    // Zeitstempel fürs Memory
    public System.DateTime timeM;
    // Zeitstempel fürs Tischdecken
    public System.DateTime timeT;

    public static GameManager gameManager;
    void Awake()
    {

        if (gameManager == null)
        {
            DontDestroyOnLoad(gameObject);
            gameManager = this;
            Levelübergang.Load();

        }
        else if (gameManager != this)
        {
            Destroy(gameObject);
        }

    }
    // übergib GameID 1 Memory, 2 Tischdecken
    public bool firstWinOfDayCheck(int ID)
    {
        System.DateTime t;
        if (ID == 1)
        {
            t = timeM;
        }
        else {
            if (ID == 2)
            {
                t = timeT;
            }
            else
            {   // Sollte nie ausgeführt werden aber der Komplier will es so
                t = System.DateTime.Now;
            }
        }
        System.TimeSpan ts = System.DateTime.Now - t;
        int difference = ts.Days;   
        if (difference>=1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
