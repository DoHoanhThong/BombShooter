using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class Ghost : MonoBehaviour
{
    public Tile tile;
    public Board mainBoard;
    public Piece trackingPiece;

    private Tilemap tilemap;
    public Vector3Int[] cells;
    public Vector3Int position;

    private void Awake()
    {
        tilemap = GetComponentInChildren<Tilemap>();
        cells = new Vector3Int[4];
    }
    private void LateUpdate()
    {
        Clear();
        Copy();
        Drop();
        Set();
    }
    private void Clear()
    {
        foreach (Vector3Int cell in cells)
        {
            Vector3Int tilePosition = cell + position;
            this.tilemap.SetTile(tilePosition, null);
        }
    }
    private void Copy()
    {
        //cells = trackingPiece.cells;
        for (int i = 0; i < cells.Length; i++)
        {
            cells[i] = trackingPiece.cells[i];
        }
    }
    private void Drop()
    {
        Vector3Int position = trackingPiece.position;

        int current = position.y;
        int bottom = -mainBoard.boardSize.y / 2 - 1;

        mainBoard.Clear(trackingPiece);

        for (int row = current; row >= bottom; row--)
        {
            position.y = row;

            if (mainBoard.IsValidPosition(trackingPiece, position))
            {
                this.position = position;
            }
            else
            {
                break;
            }
        }

        mainBoard.Set(trackingPiece);
    }
    private void Set()
    {
        //foreach (Vector3Int cell in cells)
        //{
        //    Vector3Int tilePosition = cell + position;
        //    this.tilemap.SetTile(tilePosition, tile);
        //}
        for (int i = 0; i < cells.Length; i++)
        {
            Vector3Int tilePosition = cells[i] + position;
            tilemap.SetTile(tilePosition, tile);
        }
    }
}
