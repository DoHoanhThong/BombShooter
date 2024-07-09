using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
[System.Serializable]
public class ShapeData : ScriptableObject
{
    [System.Serializable]
    public class Row
    {
        public bool[] column;
        private int _size;
        public Row() { }
        public Row(int size)
        {
            CreateRow(size);
        }
        public void CreateRow(int size)
        {
            _size = size;
            column = new bool[size];
        }
        public void ClearRow()
        {
            for (int i = 0; i < column.Length; i++)
            {
                column[i] = false;
            }
        }
    }

    public int columns = 0;
    public int rows = 0;
    public Row[] board;

    public void Clear()
    {
        for (int i = 0; i < rows; i++)
        {
            board[i].ClearRow();
        }
    }
    public void CreateNewBoard()
    {
        board = new Row[rows];
        for (int i = 0; i < rows; i++)
        {
            board[i] = new Row(columns);
        }
    }
}
