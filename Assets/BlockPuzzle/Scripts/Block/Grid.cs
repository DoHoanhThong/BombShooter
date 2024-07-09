using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlockPuzzle
{
    public class Grid : MonoBehaviour
    {
        public int columns = 0;
        public int rows = 0;
        public float squareGap = 0.1f;
        public GameObject gridSquare;
        public Vector2 startPosition = new Vector2(0, 0);
        public float squareScale = 0.5f;
        public float squareOffset = 0;

        private Vector2 _offset = new Vector2(0, 0);
        private List<GameObject> _gridSquares = new List<GameObject>();

        private void Start()
        {
            CreateGrid();
        }
        private void CreateGrid()
        {
            SpawnGridSquares();
            SetGridSquarePosition();
        }
        private void SpawnGridSquares()
        {
            int squareIndex = 0;
            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    GameObject grid = Instantiate(gridSquare, transform);
                    grid.transform.localScale = new Vector3(squareScale, squareScale, squareScale);
                    grid.GetComponent<GridSquare>().SetImage(squareIndex % 2 == 0);
                    _gridSquares.Add(grid);
                    squareIndex++;
                }
            }
        }
        private void SetGridSquarePosition()
        {
            int columnNumber = 0;
            int rowNumber = 0;
            Vector2 squareGapNumber = Vector2.zero;
            bool rowMoved = false;

            RectTransform squareRect = _gridSquares[0].GetComponent<RectTransform>();

            _offset.x = squareRect.rect.width * squareRect.transform.localScale.x + squareOffset;
            _offset.y = squareRect.rect.height * squareRect.transform.localScale.y + squareOffset;

            foreach (GameObject square in _gridSquares)
            {
                if (columnNumber + 1 > columns)
                {
                    squareGapNumber.x = 0;
                    //go to the next column
                    columnNumber = 0;
                    rowNumber++;
                    rowMoved = false;
                }
                Vector2 posOffset;
                posOffset.x = _offset.x * columnNumber + (squareGapNumber.x * squareGap);
                posOffset.y = _offset.y * rowNumber + (squareGapNumber.y * squareGap);

                if (columnNumber > 0 && columnNumber % 3 == 0)
                {
                    squareGapNumber.x++;
                    posOffset.x += squareGap;
                }

                if (rowNumber > 0 && rowNumber % 3 == 0 && rowMoved == false)
                {
                    rowMoved = true;
                    squareGapNumber.y++;
                    posOffset.y += squareGap;
                }

                square.GetComponent<RectTransform>().anchoredPosition = new Vector2(startPosition.x + posOffset.x, startPosition.y + posOffset.y);
                square.GetComponent<RectTransform>().localPosition = new Vector3(startPosition.x + posOffset.x, startPosition.y + posOffset.y, 0f);

                columnNumber++;
            }
        }
    }

}
