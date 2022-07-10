using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private Vector3 targetPosition;
    private Vector3 endPosition = new Vector3(4, 0, 4);

    private void SetTargetPosition(Vector3 inPutPosition)
    {
        targetPosition = inPutPosition;
    }

    private void Update()
    {
        float stoppingDistance = .1f;
        
        if (Vector3.Distance(endPosition, transform.position) > stoppingDistance)
        {
            if (Input.GetKeyDown(KeyCode.T))
                SetTargetPosition(new Vector3(4, 0, 4));

            Vector3 moveDirection = (targetPosition - transform.position).normalized;
            float moveSpeed = 4f;
            transform.position += moveDirection * moveSpeed * Time.deltaTime;
        }        
    }
}
