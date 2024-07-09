using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class Board : MonoBehaviour
{
    [SerializeField]
    private Tilemap tilemap;
    [SerializeField]
    private Piece piece;
    public TetrominoData[] tetrominoDatas;

    [SerializeField]
    private Vector3Int spawnPos;
    
    public Vector2Int boardSize = new Vector2Int(10, 20);

    private RectInt Bounds
    {
        get
        {
            Vector2Int position = new Vector2Int(-this.boardSize.x / 2, -this.boardSize.y / 2);
            return new RectInt(position, this.boardSize);
        }
    }
    private void Start()
    {
        foreach (TetrominoData data in tetrominoDatas)
        {
            data.Initialize();           
        }
        SpawnPiece();
    }
    public void SpawnPiece()
    {
        int random = Random.Range(0, tetrominoDatas.Length);
        TetrominoData data = tetrominoDatas[random];

        piece.Initialize(this, spawnPos, data);

        if (IsValidPosition(piece, spawnPos))
        {
            Set(piece);
        }
        else
        {
            GameOver();
        }
        
    }
    public void GameOver()
    {
        tilemap.ClearAllTiles();
    }
    public void Set(Piece piece)
    {
        foreach (Vector3Int cell in piece.cells)
        {
            Vector3Int tilePosition = cell + piece.position;
            this.tilemap.SetTile(tilePosition, piece.data.tile);
        }
    }
    public void Clear(Piece piece)
    {
        foreach (Vector3Int cell in piece.cells)
        {
            Vector3Int tilePosition = cell + piece.position;
            this.tilemap.SetTile(tilePosition, null);
        }
    }
    public bool IsValidPosition(Piece piece, Vector3Int position)
    {
        RectInt bounds = this.Bounds;

        foreach (Vector3Int cellPos in piece.cells)
        {
            Vector3Int tilePosition = cellPos + position;
            if (!bounds.Contains((Vector2Int)tilePosition))
            {
                return false;
            }
            if (this.tilemap.HasTile(tilePosition))
            {
                return false;
            }
        }
        return true;
    }
    public void ClearLines()
    {
        RectInt bounds = Bounds; 

        int row = bounds.yMin;

        while (row < bounds.yMax)
        {
            if (IsLineFull(row))
            {
                LineClear(row);
            }
            else
            {
                row++;
            }
        }
    }
    private bool IsLineFull(int row)
    {
        RectInt bounds = Bounds;

        for (int col = bounds.xMin; col < bounds.xMax; col++)
        {
            Vector3Int pos = new Vector3Int(col, row, 0);
            if (!tilemap.HasTile(pos))
            {
                return false;
            }
        }
        return true;
    }
    private void LineClear(int row)
    {
        RectInt bounds = Bounds;
        for (int col = bounds.xMin; col < bounds.xMax; col++)
        {
            Vector3Int pos = new Vector3Int(col, row, 0);
            tilemap.SetTile(pos, null);
        }

        while (row < bounds.yMax)
        {
            for (int col = bounds.xMin; col < bounds.xMax; col++)
            {
                Vector3Int pos = new Vector3Int(col, row + 1, 0);
                TileBase above = tilemap.GetTile(pos);

                pos = new Vector3Int(col, row, 0);
                tilemap.SetTile(pos, above);
            }
            row++;
        }
    }
}
