using UnityEngine;
using UnityEngine.Tilemaps;

public enum Tetromino
{
    I, J, L, O, S, T, Z
}

[System.Serializable]
public class TetrominoData
{
    public Tile tile;
    public Tetromino tetromino;

    public Vector2Int[] cells;
    public Vector2Int[,] wallKicks { get; private set; }

    public void Initialize()
    {
        cells = Data.Cells[tetromino];       
        wallKicks = Data.WallKicks[tetromino];
    }

}