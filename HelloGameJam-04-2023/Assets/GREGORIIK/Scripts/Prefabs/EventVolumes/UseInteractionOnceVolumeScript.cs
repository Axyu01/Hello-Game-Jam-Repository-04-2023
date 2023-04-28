using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseInteractionOnceVolumeScript : MonoBehaviour
{
    [SerializeField]
    InteractableObject interactableScript;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (interactableScript==null)
            Destroy(this.gameObject);
        if (other.gameObject.GetComponent<FPSController>())
        {
            interactableScript.onInteraction();
        }
        Destroy(this.gameObject);
    }
}
