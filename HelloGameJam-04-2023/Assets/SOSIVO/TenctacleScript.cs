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
    private float[] boneLenghtTab;
    private Vector3[] desiredPositionTab;
    private Vector3[] rotationModifierTab;
    public float JIGLE_ANGLE = 4f;
    public float JIGLE_ALTERNATION = 4f;
    public float TENCTACLE_STRENGTH = 100f;
    public float JIGLE_STRENGHT = 10f;
    public int iterations=5;
    // Start is called before the first frame update
    void Start()
    {
        var temp = top_node;
        while (temp != transform)
        {
            nodes.Insert(0, temp);
            temp = temp.parent;
            tentacleLength += (temp.position - temp.parent.position).magnitude;
        }
        //nodes.Insert(0, transform);
        desiredPositionTab = new Vector3[nodes.Count];
        rotationModifierTab = new Vector3[nodes.Count];
        boneLenghtTab = new float[nodes.Count - 1];
        initPositionsTab();
        for (int i = 0; i <= nodes.Count - 2; i++)
        {
            boneLenghtTab[i] = (nodes[i + 1].position - nodes[i].position).magnitude;// (desiredPositionTab[i + 1] - desiredPositionTab[i]).magnitude;
        }

    }
    private void initPositionsTab()
    {
        for (int i = nodes.Count - 1; i >= 0; i--)
        {
            desiredPositionTab[i] =nodes[i].position;
        }
    }
    private void inverseKinematic()
    {
        desiredPositionTab[nodes.Count-1]=followPoint.position;
        Debug.DrawLine(followPoint.position,followPoint.position+Vector3.up);
        for (int i = nodes.Count - 2; i >= 0; i--)
        {
            Vector3 boneVector = desiredPositionTab[i] - desiredPositionTab[i+1];
            desiredPositionTab[i] = desiredPositionTab[i + 1] + boneVector.normalized * boneLenghtTab[i];
        }
        //Debug.Log((desiredPositionTab[desiredPositionTab.Length - 1] - desiredPositionTab[desiredPositionTab.Length - 2]).magnitude);
    }
    private void forwardKinematic()
    {
        desiredPositionTab[0] = nodes[0].position;
        for (int i = 0; i <= nodes.Count-2; i++)
        {
            Vector3 boneVector = desiredPositionTab[i+1] - desiredPositionTab[i];
            desiredPositionTab[i + 1] = desiredPositionTab[i] + boneVector.normalized * boneLenghtTab[i];
        }
    }
    // Update is called once per frame
    private void OnDrawGizmos()
    {
        if (desiredPositionTab == null)
            return;
        for (int i = 0; i <= desiredPositionTab.Length - 1; i++)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(desiredPositionTab[i], 0.14f);
        }
    }
    void FixedUpdate()
    {
        //top_node.LookAt(followPoint);
        if (followPoint == null || nodes.Count<=1)
            return;
        initPositionsTab();
        for (int i = 0; i < iterations; i++)
        {
            inverseKinematic();
            forwardKinematic();
        }
        for (int i = 0; i <= nodes.Count - 2; i++)
        {
            Transform node = nodes[i+1];
            Transform parentNode = nodes[i];
            Quaternion destiny_rotation=Quaternion.FromToRotation(Vector3.up, (desiredPositionTab[i + 1] - desiredPositionTab[i]));
            parentNode.rotation=Quaternion.RotateTowards(parentNode.rotation, destiny_rotation, TENCTACLE_STRENGTH * Time.fixedDeltaTime);
            float jigleModif = JIGLE_ALTERNATION * Time.fixedDeltaTime;
            rotationModifierTab[i] = new Vector3(rotationModifierTab[i].x + jigleModif * Random.value, rotationModifierTab[i].y + jigleModif*Random.value, rotationModifierTab[i].z + jigleModif*Random.value);
            Quaternion rotatonionModif = Quaternion.Euler(Mathf.Sin(rotationModifierTab[i].x) * JIGLE_ANGLE,
                Mathf.Sin(rotationModifierTab[i].y) * JIGLE_ANGLE,
                Mathf.Sin(rotationModifierTab[i].z) * JIGLE_ANGLE
                );
            parentNode.rotation = Quaternion.RotateTowards(parentNode.rotation, parentNode.rotation * Quaternion.Euler(rotationModifierTab[i]), JIGLE_STRENGHT * Time.fixedDeltaTime);
            //parentNode.rotation *= Quaternion.FromToRotation(nodes[i+1].position- nodes[i].position, (desiredPositionTab[i + 1] - desiredPositionTab[i]));//node.position - parentNode.position

        }
        //Vector3 direction = followPoint.position - nodes[0].position;
        //Vector3 baseToTip = top_node.position - transform.position;
        //Quaternion destiny_rotation = Quaternion.FromToRotation(node.position-parentNode.position,direction);
        //Quaternion destiny_rotation = Quaternion.FromToRotation(node.position - parentNode.position, followPoint.position - parentNode.position);
        //parentNode.rotation *= Quaternion.FromToRotation(node.position - parentNode.position, (followPoint.position - parentNode.position));//destiny_rotation;
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
