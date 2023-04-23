using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : InteractableObject
{
    [SerializeField]
    GameObject rotator;
    [SerializeField]
    Quaternion rotation1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rotator.transform.rotation *= Quaternion.Euler(new Vector3(0f, 90f, 0f) * Time.deltaTime);
    }
}
