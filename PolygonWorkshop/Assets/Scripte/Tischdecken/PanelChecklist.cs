using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PanelChecklist : MonoBehaviour {
    public Toggle[] toggles;
    public Text finishedText;
    public GameObject FinishButton;

    public bool hasTimer;
    public float MaximumTime;
    public Text timerText;
    public Image timerImage;

    private float endTime;
    private float timeRemaining;
    private bool isDone;
    private Tischdecken_Manager tManager;

    public void OnItemPlaced(int currentItem)
    {
        toggles[currentItem].isOn = true;
    }

    public void OnFinished()
    {
        finishedText.enabled = true;
        finishedText.text = "+" + GetPoints(); 
        isDone = true;

        FinishButton.SetActive(true);
    }

    public void SetTableManager(Tischdecken_Manager t)
    {
        tManager = t;
    }

	// Use this for initialization
	void Start () {
        endTime = Time.time + MaximumTime;
	}
	
	// Update is called once per frame
	void Update () {
        timeRemaining = endTime - Time.time;

        if (hasTimer && !isDone)
        {
            if (timeRemaining >= 0)
            {
                UpdateTimer();
            }
            else
            {
                OnTimeOver();
            }
        }
    }

    private void UpdateTimer()
    {
        timerText.text = "" + Mathf.CeilToInt(timeRemaining);
        timerImage.fillAmount = (timeRemaining) / MaximumTime;
    }

    private void OnTimeOver()
    {
        tManager.OnTimeOver();
    }

    private int GetPoints()
    {
        if (hasTimer)
            return Mathf.CeilToInt(timeRemaining);
        else
            return tManager.GetPointsForPrecision();
    }
}
