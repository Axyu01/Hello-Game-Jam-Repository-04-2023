using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : InteractableObject
{
    [SerializeField]
    Inventory inventory;
    [SerializeField]
    int keyhole_index=0;
    [SerializeField]
    GameObject rotator;
    [SerializeField]
    Quaternion openRotation;
    private Quaternion startRotation;
    //[SerializeField]
    //float rotationSpeed;
    bool isOpened=false;
    [SerializeField]
    bool isLocked = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isLocked)
            return;
        if (isOpened)
        { 
            rotator.transform.rotation = openRotation;
        }
        else
        {
            rotator.transform.rotation = startRotation;
        }
    }
    public override void onInteraction()
    {
        base.onInteraction();
        if (inventory != null && inventory.hasKey(keyhole_index) && isLocked)
        { isLocked = false; return;}
        isOpened = !isOpened;
    }
}
