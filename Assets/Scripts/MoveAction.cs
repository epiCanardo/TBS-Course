using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : MonoBehaviour
{
    private Vector3 targetPosition;
    private Unit unit;

    private float moveSpeed = 4f;
    private float rotateSpeed = 10f;
    private float stoppingDistance = .15f;

    [SerializeField]
    private int maxMoveDistance = 5;

    [SerializeField]
    private Animator animator;

    /// <summary>
    /// placement de la case cible
    /// </summary>
    /// <param name="inPutPosition"></param>
    public void SetTargetPosition(GridPosition gridPosition)
    {
        targetPosition = LevelGrid.Instance.GetWorldPosition(gridPosition);
    }

    /// <summary>
    /// la case cliquée est elle valide ?
    /// </summary>
    /// <param name="gridPos"></param>
    /// <returns></returns>
    public bool IsValidActionGridPosition(GridPosition gridPos)
    {
        return GetValidActionGridPositions().Contains(gridPos);
    }

    /// <summary>
    /// récupération de l'ensemble des cases valides
    /// </summary>
    /// <returns></returns>
    public List<GridPosition> GetValidActionGridPositions()
    {
        List<GridPosition> validGridPositions = new List<GridPosition>();

        GridPosition unitGridPos = unit.GridPos;        
        for (int x = -maxMoveDistance; x <= maxMoveDistance; x++)
        {
            for (int z = -maxMoveDistance; z <= maxMoveDistance; z++)
            {
                GridPosition offSetGridPos = new GridPosition(x, z);
                GridPosition testGridPos = unitGridPos + offSetGridPos;

                // pas de case valide => on zappe
                if (!LevelGrid.Instance.IsValidGridPosition(testGridPos))
                    continue;                

                // on est sur la même case => on zappe
                if (unitGridPos == testGridPos)
                    continue;

                // y'a déjà une unité sur la case => on zappe
                if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPos))
                    continue;

                validGridPositions.Add(testGridPos);                
            }
        }

        return validGridPositions;
    }

    private void Awake()
    {
        targetPosition = transform.position; // code de pourceau
        unit = GetComponent<Unit>();
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
    }
}
