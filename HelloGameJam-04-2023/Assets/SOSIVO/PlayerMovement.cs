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
    Rigidbody rb;
    [SerializeField]
    GameObject camera;
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
        rb.velocity = Vector3.zero+rb.velocity.y*Vector3.up;
        if (Input.GetKey(KeyCode.W))
        {
            if(Input.GetKey(KeyCode.LeftShift))
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
        if (Input.GetKeyDown(KeyCode.Space)&& Physics.Raycast(transform.position,Vector3.down,1.1f))
            rb.velocity += rb.transform.up * jump_force;
        rb.rotation*=Quaternion.Euler(new Vector3(0f,mouse_sensitivity,0f)*Input.GetAxis("Mouse X"));
        if(camera!=null)
        {
            camera.transform.rotation *= Quaternion.Euler(new Vector3(-mouse_sensitivity, 0f, 0f) * Input.GetAxis("Mouse Y"));
        }
    }
}
