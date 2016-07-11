using UnityEngine;
using System.Collections;

public class ItemControl : MonoBehaviour {

    public ItemMovement Item;
    public Transform TargetLocation;
    public bool MovementAllowed;

    [HideInInspector]
    public float targetLockRadius;  //this value is used for the distance a item snaps to its target, propably it should be set globaly in Tischdecken_Manager


    private Tischdecken_Manager.Gamemode gamemode;
    private bool hasBeenPlacedAlready;

	// Use this for initialization
	void Start () {
        Item.Moveable = MovementAllowed;
	}
	
	// Update is called once per frame
	void Update () {

	}

    //--------------------Methods-----------------------
    public void SetGameMode(Tischdecken_Manager.Gamemode g)
    {
        gamemode = g;
    }
    
    public void SetTargetLockRadius(float radius)
    {
        targetLockRadius = radius;
    }

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
        Item.gameObject.GetComponent<Collider>().enabled = true;
        /*Vector3 playPos = transform.position;
        playPos.y = playPos.y + 6;
        transform.position = Vector3.Lerp(transform.position, playPos, 0.4f);*/
    }


    public void disableItem() {
        lockItem();

        Item.gameObject.GetComponent<MeshRenderer>().enabled = false;


        //move the a save height so it doesnt interfere with other colliders while deactivated
        Item.gameObject.GetComponent<Collider>().enabled = false;
        /*Vector3 savePos = transform.position;
        savePos.y = savePos.y - 6;
        transform.position = savePos;*/
    }


    public void checkForTargetLocation(Vector3 position){
        if (gamemode == Tischdecken_Manager.Gamemode.Timer)
        {
            Vector2 pos = new Vector2(position.x, position.z);
            Vector2 targetPos = new Vector2(TargetLocation.position.x, TargetLocation.position.z);
            float distance = Vector2.Distance(pos, targetPos);

            if (distance < targetLockRadius)
            {
                Vector3 t = TargetLocation.position;
                //t.y = Item.ItemHeightOffset;
                Item.moveToTarget(t);
                lockItem();
                //add Points, check item in List etc. (do the stuff you want to do when a new item spawns)
                //when this is the last item -> behaviour in Tischdecken_Manager.getNextitem()

                if (!hasBeenPlacedAlready)
                {
                    GetComponentInParent<Tischdecken_Manager>().getNextItem();
                    hasBeenPlacedAlready = true;
                }
            }
        }
        else
        {
            Vector3 t = Item.transform.position;
            t.y = TargetLocation.transform.position.y;
            Item.moveToTarget(t);

            if (!hasBeenPlacedAlready)
            {
                GetComponentInParent<Tischdecken_Manager>().getNextItem();
                hasBeenPlacedAlready = true;
            }
        }

        
    }



    //draw editor Gizmo for Target
    void OnDrawGizmos(){
        Gizmos.color = new Color(1, 0, 0, 0.25f);

        Matrix4x4 rotationMatrix = Matrix4x4.TRS(TargetLocation.position, TargetLocation.rotation, TargetLocation.lossyScale);
        Gizmos.matrix = rotationMatrix;

        //Gizmos.DrawCube(Vector3.zero, Vector3.one);
        //Gizmos.DrawCube(new Vector3(0, -1, 0), Vector3.one);
        //Gizmos.DrawCube(new Vector3(0, -2, 0), Vector3.one);

        Gizmos.DrawSphere(Vector3.zero, targetLockRadius);
    }
}
