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
    //Checks if the lvl is still consistent with the EP
    void lvlCheck()
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
    }
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

}
//
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