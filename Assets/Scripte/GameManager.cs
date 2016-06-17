using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    public double ep;
    public double lvl;
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
}
