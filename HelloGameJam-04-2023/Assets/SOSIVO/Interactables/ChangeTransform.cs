using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ChangeTransform : InteractableObject
{
    [SerializeField]
    List<Transform> transforms= new List<Transform>();
    Transform currentTransform;
    int index=0;
    [SerializeField]
    float position_change_speed=1f;
    [SerializeField]
    float rotation_change_speed = 1f;
    [SerializeField]
    float scale_change_speed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        var temp=new List<Transform>();
        var gameObj=new GameObject("interact_helper");
        gameObj.transform.position = transform.position;
        gameObj.transform.rotation = transform.rotation;
        gameObj.transform.localScale = transform.localScale;
        temp.Add(gameObj.transform);
        for(int i=0;i<transforms.Count;i++) { temp.Add(transforms[i]); }
        transforms = temp;
        currentTransform= transforms[0];
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        int prevIndex = (transforms.Count + index - 1) % transforms.Count;
        transform.position = moveToEnd(transform.position, currentTransform.position, position_change_speed * Time.fixedDeltaTime);
        /*new Vector3(
        clamp(currentTransform.position.x, transform.position.x, transform.position.x, (currentTransform.position.x > transform.position.x ? position_change_speed : -position_change_speed * Time.fixedDeltaTime)),
        clamp(currentTransform.position.y, transform.position.y, transform.position.y, (currentTransform.position.y > transform.position.y ? position_change_speed : -position_change_speed * Time.fixedDeltaTime)),
        clamp(currentTransform.position.z, transform.position.z, transform.position.z, (currentTransform.position.z > transform.position.z ? position_change_speed : -position_change_speed * Time.fixedDeltaTime))
        );*/
        Quaternion destinyRotation = currentTransform.rotation;
        Quaternion rotation = transform.rotation;
        transform.rotation = Quaternion.RotateTowards(rotation, destinyRotation, rotation_change_speed * Time.fixedDeltaTime);
        /*Quaternion.Euler(new Vector3(
       clamp(destinyRotation.x, rotation.x, rotation.x, (destinyRotation.x > rotation.x ? rotation_change_speed : -rotation_change_speed * Time.fixedDeltaTime)),
       clamp(destinyRotation.y, rotation.y, rotation.y, (destinyRotation.y > rotation.y ? rotation_change_speed : -rotation_change_speed * Time.fixedDeltaTime)),
       clamp(destinyRotation.z, rotation.z, rotation.z, (destinyRotation.z > rotation.z ? rotation_change_speed : -rotation_change_speed * Time.fixedDeltaTime))
       )); */

        transform.localScale = moveToEnd(transform.localScale, currentTransform.localScale, scale_change_speed * Time.fixedDeltaTime);
        /*new Vector3(
        clamp(currentTransform.localScale.x, transform.localScale.x, transform.localScale.x, (currentTransform.localScale.x > transform.localScale.x ? scale_change_speed : -scale_change_speed * Time.fixedDeltaTime)),
        clamp(currentTransform.localScale.y, transform.localScale.y, transform.localScale.y, (currentTransform.localScale.y > transform.localScale.y ? scale_change_speed : -scale_change_speed * Time.fixedDeltaTime)),
        clamp(currentTransform.localScale.z, transform.localScale.z, transform.localScale.z, (currentTransform.localScale.z > transform.localScale.z ? scale_change_speed : -scale_change_speed * Time.fixedDeltaTime))
        ); */
    }
    public override void onInteraction()
    {
        base.onInteraction();
        index=(++index)%transforms.Count;
        currentTransform= transforms[index];
    }
    private Vector3 moveToEnd(Vector3 start,Vector3 end,float delta)
    {
        if ((end - start).magnitude < delta)
        {
            return end;
        }
        Vector3 direction = (end - start).normalized*delta;
        Vector3 newVector =start +direction;
        return newVector;
    }
    private float clamp(float min, float max, float value, float delta)
    {
        if (min > max)
        {
            var temp = max;
            max = min;
            min = temp;
        }
        value += delta;
        if (value < min)
        { value = min; /*Debug.Log("min");*/ }
        else if (value > max)
        { value = max; /*Debug.Log("max");*/ }
        else
        { /*Debug.Log("else");*/ }
;        return value;
    }
    private void OnDrawGizmos()
    {
        //for (int i = 0; i < transforms.Count; i++) { Gizmos.DrawWireCube(transforms[i].position, transforms[i].localScale); }
    }
}
