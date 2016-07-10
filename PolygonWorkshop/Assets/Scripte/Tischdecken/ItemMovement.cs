using UnityEngine;
using System.Collections;

public class ItemMovement : MonoBehaviour {
    private Vector3 screenPoint;
    private Vector3 offset;

    public float ItemHeightOffset;

    private float height;

    [HideInInspector]
    public bool Moveable;



    // Use this for initialization
    void Start(){
        height = transform.position.y;
    }



    void OnMouseDown(){
        if (Moveable){
            screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

            offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        }
    }

    void OnMouseDrag(){
        if (Moveable){
            Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

            Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenPoint) + offset;
            currentPosition.y = height;
            transform.position = currentPosition;
        }
    }

    void OnMouseUp() {
        GetComponentInParent<ItemControl>().checkForTargetLocation(gameObject.transform.position);
    }


    public void moveToTarget(Vector3 target) {
        Vector3 newPos = target;
        newPos.y = newPos.y + ItemHeightOffset;
        transform.position = newPos;     
    }
}
