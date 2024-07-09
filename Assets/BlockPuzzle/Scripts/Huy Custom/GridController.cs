using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TeraJet;
using DG.Tweening;
namespace BlockPuzzle
{
    [RequireComponent(typeof(GridLayoutGroup))]
    public class GridController : Singleton<GridController>
    {
        [SerializeField]
        private int column = 8;
        [SerializeField]
        private int row = 8;
        private GridLayoutGroup gridLayout;
        private Vector2 originOffset = Vector2.zero;

        private float cellSize = 0;

        private float dropDuration = 0.25f;

        private Shape currentShape;

        List<List<GridSquare>> listGrid = new List<List<GridSquare>>();

        //private Vector2[][] listGrid = new Vector2[][]{};
        [SerializeField]
        private GridSquare gridSquarePrefab;

        private List<GridSquare> ghosts = new List<GridSquare>();
        [SerializeField]
        private GridSquare[][] listSquare;
        private bool isDropAble;

        private List<int> listRowClaim = new List<int>();
        private List<int> listColumnClaim = new List<int>();

        [SerializeField]
        private ComboConfenti comboConfenti;

        [SerializeField]
        private ParticleSystem claimEffect;


        public Shape CurrentShape { get => currentShape; set => currentShape = value; }
        public bool IsDropAble { get => isDropAble; set => isDropAble = value; }

        private int currentCenterRow;
        private int currentCenterColumn;

        private void Start()
        {
            gridLayout = GetComponent<GridLayoutGroup>();
            originOffset = gridLayout.GetComponent<RectTransform>().anchoredPosition;
            IsDropAble = false;
            Initialize();
        }
        //init grid board
        private void Initialize()
        {
            for (int i = 0; i < row; i++)
            {
                listGrid.Add(new List<GridSquare>());
                for (int j = 0; j < column; j++)
                {
                    GridSquare grid = Instantiate(gridSquarePrefab, gridLayout.transform);
                    grid.OnSpawn(i, j);
                    listGrid[i].Add(grid);
                }
            }
        }
        public void OnShapeInGrid(int pointerRow, int pointerColumn)
        {
            if (currentCenterRow == pointerRow && currentCenterColumn == pointerColumn)
                return;
            //un ghost last grid
            ResetGhost();
            UnReviewCurrentRowAndColum();
            currentCenterRow = pointerRow;
            currentCenterColumn = pointerColumn;

            Vector2Int[] listShapeIndex = CurrentShape.ListGridIndex(pointerRow, pointerColumn, CurrentShape.CurrentShapeData);

            IsDropAble = IsPlaceAble(listShapeIndex);
            if (IsDropAble)
            {
                foreach (var index in listShapeIndex)
                {
                    listGrid[index.x][index.y].ShowGhost(true);
                    ghosts.Add(listGrid[index.x][index.y]);
                }
                ReviewGrid(listShapeIndex);
            }
        }
        public bool IsPlaceAble(Vector2Int[] listShapeIndex)
        {
            for (int i = 0; i < listShapeIndex.Length; i++)
            {
                if (listShapeIndex[i].x > row - 1 || listShapeIndex[i].x < 0 || listShapeIndex[i].y > column - 1 || listShapeIndex[i].y < 0)
                {
                    return false;
                }
                if (listGrid[listShapeIndex[i].x][listShapeIndex[i].y].isActive)
                {
                    return false;
                }

            }
            return true;
        }
        public void OnDragShape(Vector2 currentShapePos)
        {
            if (IsInGrid(currentShapePos))
            {
                Vector2Int posInGrid = GetPositionInGrid(currentShapePos);
                OnShapeInGrid(posInGrid.x, posInGrid.y);
            }
            else
            {
                ResetGhost();
                UnReviewCurrentRowAndColum();
            }
        }
        public bool IsInGrid(Vector2 currentPos)
        {
            if (cellSize == 0)
                cellSize = Mathf.Abs(listGrid[(int)(row / 2f)][(int)(column / 2f)].transform.position.x - transform.position.x) * 2;
            return Mathf.Abs(currentPos.x - transform.position.x) < cellSize * (column / 2f) && Mathf.Abs(currentPos.y - transform.position.y) < cellSize * (row / 2f);
        }
        public Vector2Int GetPositionInGrid(Vector2 currentPos)
        {
            int rowIndex;
            int columIndex;
            if (currentPos.x >= transform.position.x)
            {
                columIndex = column / 2 + (int)(((currentPos.x - transform.position.x) * 1.0f / cellSize));
            }
            else
            {
                columIndex = column / 2 - 1 - (int)((transform.position.x - currentPos.x) * 1.0f / cellSize);
            }
            if (currentPos.y >= transform.position.y)
            {
                rowIndex = row / 2 - 1 - (int)((currentPos.y - transform.position.y) * 1.0f / cellSize);
            }
            else
            {
                rowIndex = row / 2 + (int)((transform.position.y - currentPos.y) * 1.0f / cellSize);
            }
            return new Vector2Int(rowIndex, columIndex);
        }
        public void ResetGhost()
        {
            foreach (GridSquare grid in ghosts)
            {
                grid.ShowGhost(false);
            }
            ghosts.Clear();
        }
        public void ActiveGhost()
        {
            //for (int i = 0; i < ghosts.Count; i++)
            //{
            //    CurrentShape.ListShapeGrid[i].transform.DOMove(ghosts[i].transform.position, dropDuration).OnComplete(() => 
            //    {
            //        CurrentShape.ListShapeGrid[i].gameObject.SetActive(false);
            //        ghosts[i].Active();
            //    });
            //}
            foreach (GridSquare grid in ghosts)
            {
                grid.Active();
            }
            ghosts.Clear();
        }
        public void ReviewGrid(Vector2Int[] listShapeIndex)
        {
            for (int i = 0; i < listShapeIndex.Length; i++)
            {
                if (!listRowClaim.Contains(listShapeIndex[i].x) && CanClaimRow(listShapeIndex[i].x))
                {
                    listRowClaim.Add(listShapeIndex[i].x);
                    ReviewRow(listShapeIndex[i].x);
                }
                if (!listColumnClaim.Contains(listShapeIndex[i].y) && CanClaimColumn(listShapeIndex[i].y))
                {
                    listColumnClaim.Add(listShapeIndex[i].y);
                    ReviewColumn(listShapeIndex[i].y);
                }
            }
        }
        private void ReviewRow(int rowIndex)
        {
            for (int i = 0; i < column; i++)
            {
                listGrid[rowIndex][i].Review();
            }
        }
        private void ReviewColumn(int columnIndex)
        {
            for (int i = 0; i < row; i++)
            {
                listGrid[i][columnIndex].Review();
            }
        }
        private void UnReviewRow(int rowIndex)
        {
            for (int i = 0; i < column; i++)
            {
                listGrid[rowIndex][i].UnReview();

            }
        }
        private void UnReviewColumn(int columnIndex)
        {
            for (int i = 0; i < row; i++)
            {
                listGrid[i][columnIndex].UnReview();
            }
        }
        public void UnReviewCurrentRowAndColum()
        {
            isDropAble = false;
            foreach (var row in listRowClaim)
            {
                UnReviewRow(row);
            }
            foreach (var col in listColumnClaim)
            {
                UnReviewColumn(col);
            }
            listRowClaim.Clear();
            listColumnClaim.Clear();
        }
        public void OnDrop()
        {
            //caculate point
            isDropAble = false;
            //10 - 30 - 60 - 90
            int count = listRowClaim.Count + listColumnClaim.Count;
            if (count > 0)
            {
                if (count == 1)
                {
                    GameController.Instance.AddPoint(10);
                }
                else
                {
                    GameController.Instance.AddPoint(30 * (count - 1));
                }
                comboConfenti.ShowConfenti(count);
            }
            //clear claim row and col
            foreach (int row in listRowClaim)
            {
                ClaimRow(row);
            }
            foreach (int col in listColumnClaim)
            {
                ClaimColumn(col);
            }
            listRowClaim.Clear();
            listColumnClaim.Clear();

        }
        public bool CanClaimRow(int rowIndex)
        {
            for (int i = 0; i < column; i++)
            {
                if (!listGrid[rowIndex][i].isActive && !listGrid[rowIndex][i].isGhost)
                {
                    return false;
                }
            }
            return true;
        }
        public bool CanClaimColumn(int columnIndex)
        {
            for (int i = 0; i < row; i++)
            {
                if (!listGrid[i][columnIndex].isActive && !listGrid[i][columnIndex].isGhost)
                {
                    return false;
                }
            }
            return true;
        }
        public void ClearRow(int rowIndex)
        {
            for (int i = 0; i < column; i++)
            {
                listGrid[rowIndex][i].Clear();
            }
        }
        public void ClearColumn(int columnIndex)
        {
            for (int i = 0; i < row; i++)
            {
                listGrid[i][columnIndex].Clear();
            }
        }

        public void ClaimRow(int rowIndex)
        {
            for (int i = 0; i < column; i++)
            {
                listGrid[rowIndex][i].Clear();
                Destroy(Instantiate(claimEffect, listGrid[rowIndex][i].transform.position, Quaternion.identity).gameObject, 2);
            }
        }
        public void ClaimColumn(int columnIndex)
        {
            for (int i = 0; i < row; i++)
            {
                listGrid[i][columnIndex].Clear();
                Destroy(Instantiate(claimEffect, listGrid[i][columnIndex].transform.position, Quaternion.identity).gameObject, 2);
            }
        }

        public bool CanPlace(ShapeData shapeData)
        {
            Shape shape = new Shape();
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    Vector2Int[] listShapeIndex = shape.ListGridIndex(i, j, shapeData);
                    if (IsPlaceAble(listShapeIndex) == true)
                        return true;
                }
            }
            return false;
        }


    }

}
