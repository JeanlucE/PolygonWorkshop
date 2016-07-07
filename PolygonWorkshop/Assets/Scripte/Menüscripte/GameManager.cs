using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public double ep;
    public double lvl;
    public int[][] highscore = new int[2][];
    public string[][] highscoreName = new string[2][];
    // Zeitstempel 
    public System.DateTime[] time = new System.DateTime[2];
    // Canvas for Highscore names
    public int eptemp;
    public int IDtemp;

    public static GameManager gameManager;
    void Awake()
    {

        if (gameManager == null)
        {
            DontDestroyOnLoad(gameObject);
            gameManager = this;
            highscore[0] = new int[5];
            highscore[1] = new int[5];
            highscoreName[0] = new string[5];
            highscoreName[1] = new string[5];
            Levelübergang.Load();
        }
        else if (gameManager != this)
        {
            Destroy(gameObject);
        }

    }
    void Start()
    {

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

    // 0 Memory 1 Tischdecken
    public void AddPoints(int EP, int ID)
    {
       
        if (firstWinOfDayCheck(ID))
        {
            ep += EP * 2;
            time[ID] = System.DateTime.Now;
        }
        else
        {
            ep += EP;
        }
        if (Levelübergang.brokeHighscore(EP, ID))
        {
            IDtemp = ID;
            eptemp = EP;
            SceneManager.LoadScene("nameenter");
        }
        else
        {
            SceneManager.LoadScene("Levelauswahl");
        }
    }
    public double getEP()
    {
        return ep;
    }
}
