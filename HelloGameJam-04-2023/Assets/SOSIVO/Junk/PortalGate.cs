using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class PortalGate : MonoBehaviour
{
    public Transform playerCamera;
    public Transform portal;
    public Transform otherPortal;
    [SerializeField]
    private Camera portalCamera;

    private void Start()
    {
        //portalCamera = new Camera();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerOffsetFromPortal = playerCamera.position - otherPortal.position;
        portalCamera.transform.position = portal.position - playerOffsetFromPortal;

        float angularDifferenceBetweenPortalRotations = Quaternion.Angle(portal.rotation, otherPortal.rotation);

        Quaternion portalRotationalDifference = Quaternion.AngleAxis(angularDifferenceBetweenPortalRotations, Vector3.up);
        Vector3 newCameraDirection = portalRotationalDifference * -playerCamera.forward;
        portalCamera.transform.rotation = Quaternion.LookRotation(newCameraDirection, Vector3.up);
    }
}
