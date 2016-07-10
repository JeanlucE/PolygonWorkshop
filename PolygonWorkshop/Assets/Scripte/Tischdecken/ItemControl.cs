using UnityEngine;
using System.Collections;

public class ItemControl : MonoBehaviour {

    public ItemMovement Item;
    public Transform TargetLocation;
    public bool MovementAllowed;

    //[HideInInspector]
    public float targetLockRadius;  //this value is used for the distance a item snaps to its target, propably it should be set globaly in Tischdecken_Manager

	// Use this for initialization
	void Start () {
        Item.Moveable = MovementAllowed;
	}
	
	// Update is called once per frame
	void Update () {

	}





    //--------------------Methods-----------------------

    public void unlockItem(){
        MovementAllowed = true;
        Item.Moveable = true;


        //select animation?
    }

    public void lockItem(){
        MovementAllowed = false;
        Item.Moveable = false;


        //deselect animation?
    }


    public void enableItem() {
        unlockItem();

        Item.gameObject.GetComponent<MeshRenderer>().enabled = true;


        //move to the playing height
        Vector3 playPos = transform.position;
        playPos.y = playPos.y + 6;
        transform.position = playPos;
    }


    public void disableItem() {
        lockItem();

        Item.gameObject.GetComponent<MeshRenderer>().enabled = false;


        //move the a save height so it doesnt interfere with other colliders while deactivated
        Vector3 savePos = transform.position;
        savePos.y = savePos.y - 6;
        transform.position = savePos;
    }


    public void checkForTargetLocation(Vector3 position){
        float distance = Vector3.Distance(position, TargetLocation.position);

        if (distance < targetLockRadius) {
            Item.moveToTarget(TargetLocation.position);

            lockItem();


            GetComponentInParent<Tischdecken_Manager>().getNextItem();
            //add Points, check item in List etc. (do the stuff you want to do when a new item spawns)
            //when this is the last item -> behaviour in Tischdecken_Manager.getNextitem()
        }
    }



    //draw editor Gizmo for Target
    void OnDrawGizmos(){
        Gizmos.color = new Color(1, 0, 0, 0.5f);

        Matrix4x4 rotationMatrix = Matrix4x4.TRS(TargetLocation.position, TargetLocation.rotation, TargetLocation.lossyScale);
        Gizmos.matrix = rotationMatrix;

        Gizmos.DrawCube(Vector3.zero, Vector3.one);
        Gizmos.DrawCube(new Vector3(0, -1, 0), Vector3.one);
        Gizmos.DrawCube(new Vector3(0, -2, 0), Vector3.one);
    }
}
