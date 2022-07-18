using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private Vector3 targetPosition;
    private GridPosition gridPosition;

    private float moveSpeed = 4f;
    private float rotateSpeed = 10f;
    private float stoppingDistance = .15f;

    [SerializeField]
    private Animator animator;

    public void SetTargetPosition(Vector3 inPutPosition)
    {
        targetPosition = inPutPosition;
    }

    private void Awake()
    {
        SetTargetPosition(transform.position);
    }

    private void Start()
    {
        gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.AddUnitAtGridPosition(gridPosition, this);
    }

    private void Update()
    { 
        if (Vector3.Distance(targetPosition, transform.position) > stoppingDistance)
        {
            // calcul du mouvement
            Vector3 moveDirection = (targetPosition - transform.position).normalized;            
            transform.position += moveDirection * moveSpeed * Time.deltaTime;

            // rotation du perso
            transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed);

            // animation de marche
            animator.SetBool("IsWalking", true);
        }       
        else
            animator.SetBool("IsWalking", false);

        GridPosition newGridPos = LevelGrid.Instance.GetGridPosition(transform.position);
        if (newGridPos != gridPosition)
        {
            LevelGrid.Instance.UnitMovedGridPosition(this, gridPosition, newGridPos);
            gridPosition = newGridPos;
        }
    }
}
