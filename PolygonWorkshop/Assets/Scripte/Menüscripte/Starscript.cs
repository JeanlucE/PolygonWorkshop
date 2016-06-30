using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Starscript : MonoBehaviour {

    public RawImage[] Stern = new RawImage[2];

    // Update is called once per frame
    void Start()
    {
        StarCheck(0);
        StarCheck(1);
    }
    private void StarCheck(int ID)
    {
        RawImage i = Stern[ID];
        if (GameManager.gameManager.firstWinOfDayCheck(ID))
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        }
        else
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        }
    }
}
