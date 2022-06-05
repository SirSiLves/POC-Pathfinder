using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways] // execute in editor & start
public class LevelFactory : MonoBehaviour
{
    [SerializeField] public int cellCountX = 10; // x-axis in world and x-axis in grid
    [SerializeField] public int cellCountY = 10; // z-axis in world and y-axis in grid
    [SerializeField] public float cellScale, pieceScale = 1f;
    [SerializeField] public int homeCellID, playerCellID, enemyCellID, gras1CellId, gras2CellId;

    private PieceFactory pieceFactory;
    private GridCell[] gridCells;


    public void Start()
    {
        // clean up
        Delete();

        // create grid
        CreateGrid();

        // create pieces
        CreatePieces();
    }

    private void Delete()
    {
        GridCell[] gridCells = FindObjectsOfType<GridCell>();
        new List<GridCell>(gridCells).ForEach(cell => DestroyImmediate(cell.gameObject));

        Piece[] pieces = FindObjectsOfType<Piece>();
        new List<Piece>(pieces).ForEach(p => DestroyImmediate(p.gameObject));
    }

    private void CreateGrid()
    {
        GridFactory gridFactory = FindObjectOfType<GridFactory>();
        gridFactory.Initialize(cellCountX, cellCountY, cellScale);
    }

    private void CreatePieces()
    {
        pieceFactory = FindObjectOfType<PieceFactory>();
        gridCells = FindObjectsOfType<GridCell>();

        pieceFactory.CreateHome(GridCell.GetGridCell(homeCellID, gridCells).transform.position, "Home", pieceScale, cellScale);
        pieceFactory.CreatePlayer(GridCell.GetGridCell(playerCellID, gridCells).transform.position, "Player", pieceScale, cellScale);
        pieceFactory.CreateEnemy(GridCell.GetGridCell(enemyCellID, gridCells).transform.position, "Enemy", pieceScale, cellScale);
        pieceFactory.CreateGras(GridCell.GetGridCell(gras1CellId, gridCells).transform.position, "Gras1", pieceScale, cellScale);
        pieceFactory.CreateGras(GridCell.GetGridCell(gras2CellId, gridCells).transform.position, "Gras2", pieceScale, cellScale);
    }



}
