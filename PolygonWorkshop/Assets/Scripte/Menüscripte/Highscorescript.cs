using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Highscorescript : MonoBehaviour
{

    public Text[] NameMemo;
    public Text[] ScoreMemo;
    public Text[] NameTable;
    public Text[] ScoreTable;

    // Use this for initialization
    void Start()
    {
        // Memory score
                for (int i = 0; i < GameManager.gameManager.highscore[0].Length; i++)
                {
                    NameMemo[i].text = (1 + i) + ". " + GameManager.gameManager.highscoreName[0][i];
                    ScoreMemo[i].text = GameManager.gameManager.highscore[0][i] + "";
                }
        // Table score
        for (int i = 0; i < GameManager.gameManager.highscore[1].Length; i++)
        {
            NameTable[i].text = (1 + i) + ". " + GameManager.gameManager.highscoreName[1][i];
            ScoreTable[i].text = GameManager.gameManager.highscore[1][i] + "";
        }
    }
}

