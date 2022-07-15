using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitActionSystem : MonoBehaviour
{
    public static UnitActionSystem Instance { get; private set; }
    
    public event EventHandler OnSelectedUnitChanged; // type standard c#

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
            selectedUnit.SetTargetPosition(new Vector3(mousePos.x, 0, mousePos.z));
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

        // impl�mentation similaire
        //if (OnSelectedUnitChanged != null)
        //{
        //    OnSelectedUnitChanged(this, EventArgs.Empty);
        //}
    }    
}
