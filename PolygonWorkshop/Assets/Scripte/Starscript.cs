using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Starscript : MonoBehaviour {

    public RawImage SternM;
    public RawImage SternT;

    // Update is called once per frame
    void Start()
    {
        StarCheck(1);
        StarCheck(2);
    }
    private void StarCheck(int ID)
    {
        RawImage i;
        if (ID == 1)
        {
            i = SternM;
        }
        else
        {
            if (ID == 2)
            {
                i = SternT;
            }
            else
            {
                i = null;
            }
        }
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
