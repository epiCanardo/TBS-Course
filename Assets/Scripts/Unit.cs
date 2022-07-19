using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private GridPosition gridPosition;
    public GridPosition GridPos => gridPosition;

    private MoveAction moveAction;
    public MoveAction MoveAction => moveAction;

    private void Awake()
    {
        moveAction = GetComponent<MoveAction>();
        //UnitActionSystem.Instance.OnUnitTargetPosChanged += UnitActionSystem_OnUnitTargetPosChanged;
    }

    private void Start()
    {
        gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.AddUnitAtGridPosition(gridPosition, this);
    }

    /// <summary>
    /// TODO : à améliorer => ne devrait être appelé qu'à chaque changement de position (évènement)
    /// </summary>
    private void Update()
    {
        GridPosition newGridPos = LevelGrid.Instance.GetGridPosition(transform.position);
        if (newGridPos != gridPosition)
        {
            LevelGrid.Instance.UnitMovedGridPosition(this, gridPosition, newGridPos);
            gridPosition = newGridPos;
        }
    }
    //private void UnitActionSystem_OnUnitTargetPosChanged(object sender, EventArgs empty)
    //{
    //    GridPosition newGridPos = LevelGrid.Instance.GetGridPosition(transform.position);
    //    LevelGrid.Instance.UnitMovedGridPosition(this, gridPosition, newGridPos);
    //    gridPosition = newGridPos;
    //}
}
