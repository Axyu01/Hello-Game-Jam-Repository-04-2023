using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : InteractableObject
{
    [SerializeField]
    GameObject rotator;
    [SerializeField]
    Quaternion openRotation;
    private Quaternion startRotation;
    //[SerializeField]
    //float rotationSpeed;
    bool isOpened=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
        isOpened = !isOpened;
    }
}
