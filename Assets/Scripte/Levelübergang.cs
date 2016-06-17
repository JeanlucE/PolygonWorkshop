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
    void Awake()
    {
        text.text = "Level: " + GameManager.gameManager.lvl;
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
    }

}
//
[Serializable]
class epData
{
    public double lvl;
    public double ep;
}