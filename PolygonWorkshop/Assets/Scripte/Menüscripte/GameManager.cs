using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    public double ep;
    public double lvl;
    public int[][] highscore = new int[2][];
    public string[][] highscoreName = new string[2][];
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
    }
    public double getEP()
    {
        return ep;
    }
}
