using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class GridObject
{
    private GridPosition gridPosition;
    //private GridSystem gridSystem;
    private List<Unit> gridUnitList;

    public GridObject(GridSystem gridSystem, GridPosition gridPosition)
    {
        //this.gridSystem = gridSystem;
        this.gridPosition = gridPosition;
        gridUnitList = new List<Unit>();
    }

    public GridPosition GridPositon => gridPosition;

    public List<Unit> GridUnits => gridUnitList;

    public void AddUnitOnGrid(Unit unit)
    {
        gridUnitList.Add(unit);
    }

    public void RemoveUnit(Unit unit)
    {
        gridUnitList.Remove(unit);
    }
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        
        foreach (Unit unit in gridUnitList)
        {
            sb.AppendLine(unit.ToString());
        }

        sb.AppendLine(gridPosition.ToString());
        return sb.ToString();
    }
}
