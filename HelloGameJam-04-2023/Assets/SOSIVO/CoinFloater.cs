using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinFloater : MonoBehaviour
{
    [SerializeField]
    float rotationSpeed = 30f;
    [SerializeField]
    float floatingSpeed = 3f;
    [SerializeField]
    float floatHight=1f;
    float offset=0f;
    Vector3 startPosition;
    // Start is called before the first frame update
    void Start()
    {
        offset= Random.value;
        startPosition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.localRotation *= Quaternion.Lerp(Quaternion.identity,Quaternion.Euler(0f, rotationSpeed, 0f),Time.fixedDeltaTime);
        if(floatHight>0f)
        transform.position= startPosition+transform.up*Mathf.Sin(Mathf.PI*floatingSpeed*Time.time)*floatHight;
    }
}
