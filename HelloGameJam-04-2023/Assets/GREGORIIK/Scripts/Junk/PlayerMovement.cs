using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float player_speed=5f;
    [SerializeField]
    float jump_force = 5f;
    [SerializeField]
    float mouse_sensitivity = 10f;
    [SerializeField]
    float sprint_speed = 14f;
    [SerializeField]
    float interaction_range = 4f;
    Rigidbody rb;
    [SerializeField]
    GameObject cam;
    // Start is called before the first frame update
    void Start()
    {
        rb=GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb == null)
            return;
        rb.velocity = Vector3.zero + rb.velocity.y * Vector3.up;
        if (Input.GetKey(KeyCode.W))
        {
            if (Input.GetKey(KeyCode.LeftShift))
                rb.velocity += rb.transform.forward * sprint_speed;
            else
                rb.velocity += rb.transform.forward * player_speed;
        }
        if (Input.GetKey(KeyCode.D))
            rb.velocity += rb.transform.right * player_speed;
        if (Input.GetKey(KeyCode.S))
            rb.velocity -= rb.transform.forward * player_speed;
        if (Input.GetKey(KeyCode.A))
            rb.velocity -= rb.transform.right * player_speed;
        if (Input.GetKeyDown(KeyCode.Space) && Physics.Raycast(transform.position, Vector3.down, 1.1f))
            rb.velocity += rb.transform.up * jump_force;
        rb.rotation *= Quaternion.Euler(new Vector3(0f, mouse_sensitivity, 0f) * Input.GetAxis("Mouse X"));
        worldInteraction();
    }
    void worldInteraction()
    {
        if (cam != null)
        {
            cam.transform.rotation *= Quaternion.Euler(new Vector3(-mouse_sensitivity, 0f, 0f) * Input.GetAxis("Mouse Y"));
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                RaycastHit hit;
                if (Physics.Raycast(cam.transform.position, rb.rotation * cam.transform.localRotation * Vector3.forward, out hit, interaction_range))
                {
                    Debug.DrawLine(hit.point, hit.point + Vector3.up * 1f, Color.green);
                    GameObject interactableObject = hit.transform.gameObject;
                    while (interactableObject != null && interactableObject.tag != "interactable")
                    {
                        if (interactableObject.transform.parent != null)
                            interactableObject = interactableObject.transform.parent.gameObject;
                        else
                            interactableObject = null;
                    }
                    if (interactableObject != null && interactableObject.tag == "interactable")
                    {
                        InteractableObject interactable = interactableObject.GetComponent<InteractableObject>();
                        if (interactable != null)
                        {
                            Debug.Log("Interaction");
                            interactable.onInteraction();
                        }
                    }
                }
                //Debug.DrawRay(camera.transform.position, rb.rotation * camera.transform.rotation * Vector3.forward, Color.red, 10f);
            }
        }
    }
}