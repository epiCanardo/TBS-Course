using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private Vector3 targetPosition;

    private void SetTargetPosition(Vector3 inPutPosition)
    {
        targetPosition = inPutPosition;
    }

    private void Update()
    {
        float stoppingDistance = .15f;
        
        if (Input.GetMouseButtonDown(0))
        {
            var mousePos = MouseWorld.GetPosition();
            SetTargetPosition(new Vector3(mousePos.x, 0, mousePos.z));
        }

        if (Vector3.Distance(targetPosition, transform.position) > stoppingDistance)
        {
            Vector3 moveDirection = (targetPosition - transform.position).normalized;
            float moveSpeed = 4f;
            transform.position += moveDirection * moveSpeed * Time.deltaTime;
        }        
    }
}
