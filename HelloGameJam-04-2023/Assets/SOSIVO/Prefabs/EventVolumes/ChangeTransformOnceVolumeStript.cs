using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTransformOnceVolumeStript : MonoBehaviour
{
    [SerializeField]
    Transform changedTransform;
    [SerializeField]
    Transform desiredTransform;
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
        if (desiredTransform == null || changedTransform == null)
            Destroy(this.gameObject);
        if (other.gameObject.GetComponent<FPSController>())
        {
            changedTransform.position = desiredTransform.position;
            //changedTransform.rotation = desiredTransform.rotation;
            //changedTransform.localScale = desiredTransform.localScale;
        }
        Destroy(this.gameObject);
    }
}
