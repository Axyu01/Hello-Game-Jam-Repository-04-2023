using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 1;
    [SerializeField] float verticalMovementAmplitude = 10;
    [SerializeField] float verticalMovementSpeed = 3;

    float timer;

    PlayerHealth target;

    private void Start()
    {
        target = FindAnyObjectByType<PlayerHealth>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        float verticalMovement = Mathf.PingPong(timer * verticalMovementSpeed, verticalMovementAmplitude);
        float yPos = target.transform.position.y + verticalMovement;
        Vector3 targetPosition = new Vector3(target.transform.position.x+ Mathf.PingPong(timer * verticalMovementSpeed, verticalMovementAmplitude), yPos, target.transform.position.z+ Mathf.PingPong(timer * verticalMovementSpeed, verticalMovementAmplitude));
        if (targetPosition != gameObject.transform.position)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, targetPosition, movementSpeed*Time.deltaTime);
        }
    }
}
