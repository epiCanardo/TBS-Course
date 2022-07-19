using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitActionSystem : MonoBehaviour
{
    public static UnitActionSystem Instance { get; private set; }

    public event EventHandler OnSelectedUnitChanged; // type standard c#
    //public event EventHandler OnUnitTargetPosChanged;

    [SerializeField]
    private Unit selectedUnit;
    public Unit SelectedUnit => selectedUnit;

    [SerializeField]
    private LayerMask unitLayerMask;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        // s�lection du perso : cli gauche
        if (Input.GetMouseButtonDown(0) && TryHandleUnitSelection()) return;

        // d�placement : clic droit
        if (Input.GetMouseButtonDown(1))
        {
            // r�cup�ration de la position
            var mousePos = MouseWorld.GetPosition();
            GridPosition mouseGridPos = LevelGrid.Instance.GetGridPosition(mousePos);

            if (selectedUnit.MoveAction.IsValidActionGridPosition(mouseGridPos))
            {
                selectedUnit.MoveAction.SetTargetPosition(mouseGridPos);
                //OnUnitTargetPosChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    private bool TryHandleUnitSelection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // rayon � la recherche de colliders
        if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, unitLayerMask))
        {
            if (hit.transform.TryGetComponent(out Unit unit))
            {
                SetSelectedUnit(unit);
                return true;
            }
        }

        return false;
    }

    private void SetSelectedUnit(Unit unit)
    {
        selectedUnit = unit;
        OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);
    }
}