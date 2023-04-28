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
    float openRotation=90f;
    [SerializeField]
    float rotationSpeed=180f;
    [SerializeField]
    AudioSource interaction;
    [SerializeField]
    AudioSource unlock;
    [SerializeField]
    GameObject lockObject;
    private float startRotation;
    //[SerializeField]
    //float rotationSpeed;
    bool isOpened=false;
    [SerializeField]
    bool isLocked = true;
    // Start is called before the first frame update
    void Start()
    {
        startRotation = rotator.transform.localRotation.eulerAngles.y;
        currentRotation = rotator.transform.localRotation.eulerAngles.y;

    }
    const float ROTATATION_SPEED= 90f;
    float currentRotation=0f;
    // Update is called once per frame
    void FixedUpdate()
    {
        if (isLocked)
            return;
        if (isOpened)
        {
            currentRotation = Mathf.MoveTowards(currentRotation, openRotation,Time.fixedDeltaTime * rotationSpeed);
            //rotator.transform.rotation = openRotation;
        }
        else
        {
            currentRotation = Mathf.MoveTowards(currentRotation, startRotation, Time.fixedDeltaTime * rotationSpeed);
            //rotator.transform.rotation = startRotation;
        }
        Vector3 eulerRot = rotator.transform.localRotation.eulerAngles;
        eulerRot = new Vector3(eulerRot.x,(360000f+currentRotation)%360f,eulerRot.z);
        rotator.transform.localRotation=Quaternion.Euler(eulerRot);
    }
    public override void onInteraction()
    {
        base.onInteraction();
        if (inventory != null && inventory.hasKey(keyhole_index) && isLocked)
        { unlock.Play(); isLocked = false; Destroy(lockObject); return;}
        interaction.Play();
        isOpened = !isOpened;
    }
}
