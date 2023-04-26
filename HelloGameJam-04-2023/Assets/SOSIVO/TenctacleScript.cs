using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.PackageManager;
using UnityEngine;

public class TenctacleScript : MonoBehaviour
{
    public Transform top_node;
    public Transform followPoint;
    private List<Transform> nodes = new List<Transform>();
    private float tentacleLength = 0f;
    // Start is called before the first frame update
    void Start()
    {
        var temp = top_node;
        while (temp != transform)
        {
            nodes.Add(temp);
            temp = temp.parent;
            tentacleLength+=(temp.position-temp.parent.position).magnitude;
        }
        nodes.Add(transform);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //top_node.LookAt(followPoint);
        if (followPoint == null || nodes.Count<=1)
            return;
        Vector3 direction = followPoint.position - nodes[0].position;
        Vector3 baseToTip = top_node.position - transform.position;

        Transform node = nodes[0];
        Transform parentNode = nodes[0 + 1];
        //Quaternion destiny_rotation = Quaternion.FromToRotation(node.position-parentNode.position,direction);
        Quaternion destiny_rotation = Quaternion.FromToRotation(node.position - parentNode.position, followPoint.position - parentNode.position);
        parentNode.rotation *= Quaternion.FromToRotation(node.position - parentNode.position, (followPoint.position - parentNode.position));//destiny_rotation;
        //parentNode.LookAt(followPoint);
        return;
        /*
        for (int i= 0;i < nodes.Count-1;i++)
        {
            Transform node = nodes[i];
            Transform parentNode = nodes[i + 1];
            //Quaternion destiny_rotation = Quaternion.FromToRotation(node.position-parentNode.position,direction);
            Quaternion destiny_rotation = Quaternion.FromToRotation(parentNode.position - parentNode.position, followPoint.position-parentNode.position);
            parentNode.rotation=destiny_rotation;
            //parentNode.rotation = Quaternion.RotateTowards(parentNode.rotation, destiny_rotation, 5f * Time.deltaTime); //* direction.magnitude*(baseToTip.magnitude-tentacleLength));
            //node.rotation = Quaternion.RotateTowards(node.rotation,Random.rotation,5f *Time.deltaTime);
            //node.rotation = Quaternion.RotateTowards(node.rotation, node.localRotation * Quaternion.Inverse(parentNode.localRotation), 10f * Time.deltaTime);
        }
        */
    }
}
