using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnterNamescript : MonoBehaviour {
    public Text text;
    public void finishtipping()
    {
        SceneManager.LoadScene("Levelauswahl");
        Levelübergang.HighscoreCheck(GameManager.gameManager.eptemp,text.text,GameManager.gameManager.IDtemp);
    }
}
