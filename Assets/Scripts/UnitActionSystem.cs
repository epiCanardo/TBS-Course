using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitActionSystem : MonoBehaviour
{
    [SerializeField]
    private Unit selectedUnit;

    [SerializeField]
    private LayerMask unitLayerMask;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // selection de l'unit�
            if (TryHandleUnitSelection()) return;

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
                selectedUnit = unit;
                return true;
            }
        }

        return false;
    }
}
