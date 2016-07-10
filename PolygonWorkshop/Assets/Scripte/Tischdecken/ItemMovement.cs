using UnityEngine;
using System.Collections;

public class ItemMovement : MonoBehaviour {
    
    public float ItemHeightOffset;
    public float LockAnimationTime;
    public AnimationCurve LockAnimationCurve;
    
    [HideInInspector]
    public bool Moveable;

    private Vector3 screenPoint;
    private Vector3 offset;
    private float height;

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
            transform.position = Vector3.Lerp(transform.position, currentPosition, 0.4f);
        }
    }

    void OnMouseUp() {
        GetComponentInParent<ItemControl>().checkForTargetLocation(gameObject.transform.position);
    }

    public void moveToTarget(Vector3 target) {
        Vector3 newPos = target;
        //newPos.y = newPos.y + ItemHeightOffset;
        //transform.position = newPos;
        StartCoroutine(MoveToTargetLerp(newPos));   
    }
    
    private IEnumerator MoveToTargetLerp(Vector3 target)
    {
        
        Vector3 startPos = transform.position;
        float startTime = Time.time;
        float endTime = Time.time + LockAnimationTime;
        while (Time.time < endTime)
        {
            transform.position = Vector3.Lerp(startPos, target, LockAnimationCurve.Evaluate((Time.time - startTime) / LockAnimationTime));
            yield return null;
        }
    }
}
