using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Text;
using System.IO;
using System;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
public class Levelübergang : MonoBehaviour
{
    public Text text;
    private Image EPBalken;

    void Start()
    {
        text.text = "Level: " + GameManager.gameManager.lvl;
        lvlCheck();
        EPBalken = GameObject.Find("EPBalken").GetComponent<Image>();
        Material mat = EPBalken.material;
        mat.SetFloat("_Swipe",SwipeByEP());
     
    }
    
    //*----------------------------------Menu methods
    // Is called when changing a szene
    public void starten(string name)
    {
        lvlCheck();
        SceneManager.LoadScene(name);  
    }
    // is called when leaving the game
    public void verlassen()
    {
        Save();
        Application.Quit();
    }

    //**-------------------------------- Lvl Methods
    //Checks if the lvl is still consistent with the EP
   public static void lvlCheck()
    {
        // By ~300Ep lvl 2
        GameManager.gameManager.lvl = Math.Log((GameManager.gameManager.getEP()/100.0d), 2);
        if (GameManager.gameManager.lvl <= 0)
        {
            GameManager.gameManager.lvl = 1;
        }
        // Rundet das lvl auf eine ganze Zahl
        int i =(int)GameManager.gameManager.lvl;
        GameManager.gameManager.lvl = i;
        
    }
    // Fils the Epbar by Ep
    float SwipeByEP()
    {
        if (GameManager.gameManager.lvl == 1)
        {
            // Bei anderem lvl System muss hinterer teilgeändet werden.
            return (float)GameManager.gameManager.ep / (float)(Math.Pow(2d,2d)*100d);
        }
        float epSince =(float)( GameManager.gameManager.ep - (Math.Pow(2d, GameManager.gameManager.lvl) * 100d));
        float nextLvlEp = (float)((Math.Pow(2d, GameManager.gameManager.lvl + 1) * 100d) - (Math.Pow(2d, GameManager.gameManager.lvl) * 100d));
        return (float)epSince/nextLvlEp ;
    }
    //**------------------------------------- Highscore
    // updates the highscore
    public static void HighscoreCheck(int newScore, string Name, int ID)
    {
        if (newScore > GameManager.gameManager.highscore[ID][GameManager.gameManager.highscore[ID].Length-1])
        {
            GameManager.gameManager.highscore[ID][GameManager.gameManager.highscore[ID].Length-1] = newScore;
            GameManager.gameManager.highscoreName[ID][GameManager.gameManager.highscore[ID].Length-1] = Name;
            for (int i = GameManager.gameManager.highscore[ID].Length - 2; i >= 0; i--)
            {
                if (GameManager.gameManager.highscore[ID][i] < GameManager.gameManager.highscore[ID][i + 1])
                {
                    // tauscht die highscores
                    int score = GameManager.gameManager.highscore[ID][i];
                    string scoreName = GameManager.gameManager.highscoreName[ID][i];
                    GameManager.gameManager.highscore[ID][i] = GameManager.gameManager.highscore[ID][i + 1];
                    GameManager.gameManager.highscoreName[ID][i] = GameManager.gameManager.highscoreName[ID][i + 1];
                    GameManager.gameManager.highscore[ID][i + 1] = score;
                    GameManager.gameManager.highscoreName[ID][i + 1] = scoreName;
                }
            }
        }
    }
    // Checks if highscore was broke
    public static bool brokeHighscore(int newScore, int ID)
    {
        if (newScore > GameManager.gameManager.highscore[ID][GameManager.gameManager.highscore[ID].Length - 1])
        {
            return true;
        }
        return false;
    }


  //**----------------------------------------------- Save and Load
    //Saves the Ep und lvl to a file
    public static void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath+"/ep.dat");
        epData data = new epData();
        data.ep = GameManager.gameManager.getEP();
        data.lvl = GameManager.gameManager.lvl;
        bf.Serialize(file,data);
        file.Close();
        SaveFirstWin("Memory", 0);
        SaveFirstWin("Tischdecken",1);
        SaveHighscore("highscore0",0);
        SaveHighscore("highscore1",1);
    }
    // Saves the first win of the Daydata
    private static void SaveFirstWin(string fileName, int ID)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/"+fileName+".dat");
        firstWin data = new firstWin();
        System.DateTime t = GameManager.gameManager.time[ID];
        
        data.day = t.Day;
        data.minute = t.Minute;
        data.hour = t.Hour;
        bf.Serialize(file, data);
        file.Close();

    }
    private static void SaveHighscore(string fileName, int ID)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath+"/"+fileName+".dat");
        highscoreData data = new highscoreData();
        for (int i = 0; i < GameManager.gameManager.highscore[ID].Length; i++)
        {
            data.scores[i] = GameManager.gameManager.highscore[ID][i];
            data.scoresName[i] = GameManager.gameManager.highscoreName[ID][i];
        }
        bf.Serialize(file, data);
        file.Close();
    }

    // Loads the Ep und lvl form a file
    public static void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/ep.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/ep.dat", FileMode.Open);
            epData data = (epData)bf.Deserialize(file);
            GameManager.gameManager.ep = data.ep;
            GameManager.gameManager.lvl = data.lvl;
            file.Close();
        }
        else
        {
            GameManager.gameManager.ep = 0.0d;
            GameManager.gameManager.lvl = 1.0d;
        }
        LoadFirstWin("Memory",0);
        LoadFirstWin("Tischdecken", 1);
        LoadHighscore("highscore0",0);
        LoadHighscore("highscore1", 1);
    }
    // ID 0 Memory, ID 1 Tischdecken
    // Läd die Firstwin of the Day daten
    private static void LoadFirstWin(string fileName,int ID)
    {
        if (File.Exists(Application.persistentDataPath + "/" + fileName + ".dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + fileName + ".dat", FileMode.Open);
            firstWin data = (firstWin)bf.Deserialize(file);
            GameManager.gameManager.time[ID] = new DateTime(System.DateTime.Now.Year,System.DateTime.Now.Month,data.day,data.hour,data.minute,System.DateTime.Now.Second);
        }
        else
        {
               GameManager.gameManager.time[ID] = System.DateTime.Now.AddDays(-1);
                GameManager.gameManager.time[ID] = GameManager.gameManager.time[ID].AddHours(-1);
        }
    }

    static void LoadHighscore(string fileName, int ID)
    {
        if (File.Exists(Application.persistentDataPath + "/" + fileName + ".dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + fileName + ".dat", FileMode.Open);
            highscoreData data = (highscoreData)bf.Deserialize(file);
            for (int i = 0; i < GameManager.gameManager.highscore[ID].Length; i++)
            {
                GameManager.gameManager.highscore[ID][i] = data.scores[i];
                GameManager.gameManager.highscoreName[ID][i] = data.scoresName[i];
            }

        }
        else
        {
            for (int i = 0;i<GameManager.gameManager.highscore[ID].Length;i++)
            {           
                GameManager.gameManager.highscore[ID][i] = 50 * (GameManager.gameManager.highscore[ID].Length-1- i)+50;
            }
            GameManager.gameManager.highscoreName[ID][0] = "Bert";
            GameManager.gameManager.highscoreName[ID][1] = "Hans";
            GameManager.gameManager.highscoreName[ID][2] = "Susanne";
            GameManager.gameManager.highscoreName[ID][3] = "Tom";
            GameManager.gameManager.highscoreName[ID][4] = "Tim";
        }
    }

}
//
[Serializable]
class highscoreData
{
    public int[] scores=new int[GameManager.gameManager.highscore[0].Length];
    public string[] scoresName = new string[GameManager.gameManager.highscore[0].Length];
}

[Serializable]
class epData
{
    public double lvl;
    public double ep;
}
[Serializable]
class firstWin
{
    public int day;
    public int hour;
    public int minute;
}