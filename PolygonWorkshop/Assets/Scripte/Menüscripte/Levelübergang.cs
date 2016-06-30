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


    void Start()
    {
        text.text = "Level: " + GameManager.gameManager.lvl;
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
        GameManager.gameManager.lvl = Math.Log((GameManager.gameManager.ep/100.0d), 2);
        if (GameManager.gameManager.lvl <= 0)
        {
            GameManager.gameManager.lvl = 1;
        }
        GameManager.gameManager.lvl = Math.Round(GameManager.gameManager.lvl,MidpointRounding.AwayFromZero);
        
    }
  
    //Saves the Ep und lvl to a file
    public static void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath+"/ep.dat");
        epData data = new epData();
        data.ep = GameManager.gameManager.ep;
        data.lvl = GameManager.gameManager.lvl;
        bf.Serialize(file,data);
        file.Close();
        SaveFirstWin("Memory", 1);
        SaveFirstWin("Tischdecken",2);
    }
    private static void SaveFirstWin(string fileName, int ID)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/"+fileName+".dat");
        firstWin data = new firstWin();
        System.DateTime t;
        if (ID == 1)
        {
            t = GameManager.gameManager.timeM;
        }
        else { 
        if (ID == 2)
        {
            t = GameManager.gameManager.timeT;
        }
        else
        {
            // sollte nie ausgeführt werden aber der komplier will es so
            t = System.DateTime.Now;
        }
        }
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
        LoadFirstWin("Memory",1);
        LoadFirstWin("Tischdecken", 2);
    }
    // ID 1 Memory, ID 2 Tischdecken
    // Läd die Firstwin of the Day daten
    private static void LoadFirstWin(string fileName,int ID)
    {
        if (File.Exists(Application.persistentDataPath + "/" + fileName + ".dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + fileName + ".dat", FileMode.Open);
            firstWin data = (firstWin)bf.Deserialize(file);
            if (ID == 1)
            {
                GameManager.gameManager.timeM = new System.DateTime(System.DateTime.Now.Year, System.DateTime.Now.Month, data.day, data.hour, data.minute, System.DateTime.Now.Second);
            }
            if (ID == 2)
            {
                GameManager.gameManager.timeT = new System.DateTime(System.DateTime.Now.Year, System.DateTime.Now.Month, data.day, data.hour, data.minute, System.DateTime.Now.Second);
            }
        }
        else
        {
            if (ID == 1)
            {
               GameManager.gameManager.timeM = System.DateTime.Now.AddDays(-1);
                GameManager.gameManager.timeM = GameManager.gameManager.timeM.AddHours(-1);
            }
            if (ID == 2)
            {
               GameManager.gameManager.timeT = System.DateTime.Now.AddDays(-1);
                GameManager.gameManager.timeT = GameManager.gameManager.timeT.AddHours(-1);
            }
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