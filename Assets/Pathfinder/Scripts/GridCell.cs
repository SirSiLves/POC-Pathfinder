using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridCell : MonoBehaviour
{
    [SerializeField] private GameObject text;


    private int posX;
    private int posY;
    private int cellID;


    public void SetCoordinates(int x, int y, int cellCount)
    {
        posX = x;
        posY = y;
        cellID = cellCount;
        text.GetComponent<Text>().text = cellID.ToString();
    }

    public Vector2Int GetCoordinate()
    {
        return new Vector2Int(posX, posY);
    }

    public int GetCellID()
    {
        return this.cellID;
    }


    public static GridCell GetGridCell(int cellID, GridCell[] gridCells)
    {
        for (int i = 0; i < gridCells.Length; i++)
        {
            GridCell gridCell = gridCells[i];

            if (gridCell.GetCellID() == cellID)
            {
                return gridCell;
            }

        }


        throw new Exception("Cell ID: " + cellID + " could not be found.");
    }
}
