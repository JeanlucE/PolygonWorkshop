using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Levelübergang : MonoBehaviour
{

    public void starten(string name)
    {
        SceneManager.LoadScene(name);
    }
    public void verlassen()
    {
        Application.Quit();
    }
}
