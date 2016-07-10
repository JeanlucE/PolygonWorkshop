using UnityEngine;
using System.Collections.Generic;

public class Tischdecken_Manager : MonoBehaviour {

    public enum Gamemode
    {
        Timer, Precision
    }

    public ItemControl[] items;
    public PanelChecklist panelCheckList;
    public float GlobalItemLockRadius;
    public Gamemode gamemode;
    public Material transparentMaterial;

    private ItemControl currentItem; //dont know if needed, but maybe there is a use for it, remove when done and not used
    private int lastItem;

	// Use this for initialization
	void Start () {
        //initialize the scene  
        //(all items are in the scene on start (maybe not the best solution, but should work out for the prototype))
        foreach (ItemControl item in items) {
            item.SetGameMode(gamemode);
            item.SetTargetLockRadius(GlobalItemLockRadius);
            item.disableItem();
        }

        currentItem = items[0];

        currentItem.enableItem();
        lastItem = 0;

        panelCheckList.SetTableManager(this);

        //ToggleGoalVisibility();
	}
	
	// Update is called once per frame
	void Update () {
        if (goalIsVisible)
        {
            for (int i = 0; i < goalObjects.Count; i++)
            {
                goalObjects[i].transform.position = items[i].TargetLocation.transform.position;
            }
        }
	}


    public void getNextItem() {
        if (lastItem < (items.Length - 1)){
            panelCheckList.OnItemPlaced(lastItem);

            lastItem++;
            currentItem = items[lastItem];
            currentItem.enableItem();
        }
        else
        {
            //scene is done, do what ever you need to do
            panelCheckList.OnItemPlaced(lastItem);
            panelCheckList.OnFinished();
        }
    }
    
    public int GetPointsForPrecision()
    {
        int sum = 0;
        int maxPointsPerItem = 10;
        float minimumDistanceForPoints = 2f;

        foreach(ItemControl ic in items)
        {
            Vector2 targetPos = new Vector2(ic.TargetLocation.position.x, ic.TargetLocation.position.z);
            Vector2 currentPos = new Vector2(ic.Item.transform.position.x, ic.Item.transform.position.z);

            float closeness = 1 - Mathf.Clamp(Vector2.Distance(targetPos, currentPos) / minimumDistanceForPoints, 0.1f, 1f);
            int points = Mathf.RoundToInt(closeness * maxPointsPerItem);

            sum += points;
        }
        

        return sum;
    }

    private bool goalIsVisible = false;
    private bool instantiated = false;
    private List<GameObject> goalObjects = new List<GameObject>();
    public void ToggleGoalVisibility()
    {
        if(goalIsVisible)
        {
            foreach(GameObject g in goalObjects)
            {
                g.GetComponent<Renderer>().enabled = false;
            }
            goalIsVisible = false;
        }
        else
        {
            if (!instantiated)
            {
                foreach (ItemControl ic in items)
                {
                    GameObject g = (GameObject)Instantiate(ic.Item.gameObject, ic.TargetLocation.position, ic.Item.gameObject.transform.rotation);
                    g.GetComponent<Collider>().enabled = false;
                    g.GetComponent<MeshRenderer>().material = transparentMaterial;
                    g.GetComponent<ItemMovement>().enabled = false;

                    goalObjects.Add(g);
                }
                instantiated = true;
            }

            foreach (GameObject g in goalObjects)
            {
                g.GetComponent<Renderer>().enabled = true;
            }

            goalIsVisible = true;
        }
    }

    public void OnTimeOver()
    {
        Debug.Log("Time Over");
    }

    public void OnExit()
    {
        GameManager.gameManager.AddPoints(GetPointsForPrecision(), 1);
    }
}
