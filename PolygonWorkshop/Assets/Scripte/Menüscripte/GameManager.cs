using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    public double ep;
    public double lvl;
    // Zeitstempel fürs Memory
    public System.DateTime[] time = new System.DateTime[2];
    // Zeitstempel fürs Tischdecken
 //   public System.DateTime timeT;

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
    // übergib GameID 0 Memory, 1 Tischdecken
    public bool firstWinOfDayCheck(int ID)
    {
        System.TimeSpan ts = System.DateTime.Now - time[ID];
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

    public void AddPoints(int EP, int ID)
    {
    }
    public double getEP()
    {
        return ep;
    }
}
