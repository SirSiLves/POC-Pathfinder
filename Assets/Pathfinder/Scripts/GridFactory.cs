using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[ExecuteAlways] Execute in Editor & Start
//[ExecuteInEditMode] Execute only in Editor
public class GridFactory : MonoBehaviour 
{
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private float cellMargin = 10f;

    private int cellCountX; // x-axis in World
    private int cellCountY; // z-axis in World
    private float cellScale;
    private GameObject[,] grid;

    
    // called from levelfactory
    public void Initialize(int cellCountX, int cellCountY, float cellScale)
    {
        this.cellCountX = cellCountX;
        this.cellCountY = cellCountY;
        this.cellScale = cellScale;

        if (cellPrefab == null)
        {
            throw new Exception("Grid Cell Prefab of the Game is empty and not assigned");
        }

        grid = new GameObject[cellCountX, cellCountY];

        Vector3 startPosition = transform.position + new Vector3(cellMargin, cellMargin, cellMargin) / 2;


        // display grid
        int cellID = 1;
        for (int z = 0; z < cellCountY; z++) {
            for (int x = 0; x < cellCountX; x++)
            {
                // create cell
                grid[x,z] = Instantiate(cellPrefab, new Vector3(x * (this.cellMargin + this.cellScale) + startPosition.x, this.transform.position.y, z * (this.cellMargin + this.cellScale) + startPosition.z), Quaternion.identity);
                grid[x,z].GetComponent<GridCell>().SetCoordinates(x, z, cellID);
                grid[x,z].GetComponent<GridCell>().transform.localScale = new Vector3(cellScale, cellScale, cellScale);
                grid[x,z].transform.parent = transform;
                grid[x,z].gameObject.name = "X/Y: " + x.ToString() + "/" + z.ToString() + " ID: " + cellID;

                cellID++;
            }
        }
    }



    public Vector2Int GetGridPosFromWorld(Vector3 worldPosition)
    {
        int x = Mathf.FloorToInt(worldPosition.x / cellMargin);
        int z = Mathf.FloorToInt(worldPosition.z / cellMargin);

        x = Mathf.Clamp(x, 0, cellCountX);
        z = Mathf.Clamp(z, 0, cellCountY);

        return new Vector2Int(x, z);
    }

    public Vector3 GetWroldPosFromGridPos(Vector3Int gridPos)
    {
        float x = gridPos.x * cellMargin;
        float z = gridPos.z * cellMargin;

        return new Vector3(x, 0, z);
    }

    public GameObject[,] GetGrid()
    {
        return this.grid;
    }


}
