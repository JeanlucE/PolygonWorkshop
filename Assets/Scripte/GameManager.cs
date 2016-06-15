using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    public float ep;
    public float lvl;
    static GameManager gameManager;
    void Awake()
    {
        if (gameManager == null)
        {
            DontDestroyOnLoad(gameObject);
            gameManager = this;
            
        }
        else if (gameManager != this)
        {
            Destroy(gameObject);
        }

    }
}
