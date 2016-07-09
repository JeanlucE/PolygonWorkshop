using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tischdecken_Manager : MonoBehaviour {
    public ItemControl[] items;

    private ItemControl currentItem;
    private int lastItem;

	// Use this for initialization
	void Start () {
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
