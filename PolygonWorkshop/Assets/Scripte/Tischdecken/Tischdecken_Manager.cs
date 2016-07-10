using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tischdecken_Manager : MonoBehaviour {
    public ItemControl[] items;

    private ItemControl currentItem; //dont know if needed, but maybe there is a use for it, remove when done and not used
    private int lastItem;

	// Use this for initialization
	void Start () {
        //initialize the scene  
        //(all items are in the scene on start (maybe not the best solution, but should work out for the prototype))
        foreach (ItemControl item in items) {
            item.disableItem();
        }

        currentItem = items[0];

        currentItem.enableItem();
        lastItem = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public void getNextItem() {
        if (lastItem < (items.Length - 1)){
            lastItem++;
            currentItem = items[lastItem];
            currentItem.enableItem();
        }
        else{
            //scene is done, do what ever you need to do
        }
    }
}
