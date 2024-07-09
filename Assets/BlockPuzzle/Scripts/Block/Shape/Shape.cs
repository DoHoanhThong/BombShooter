using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;
using UnityEngine.EventSystems;
namespace BlockPuzzle
{
    public class Shape : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField]
        private Image[] listCellImage;

        private Image squareShapeImage;
        [HideInInspector]
        public ShapeData CurrentShapeData;
        [SerializeField]
        private Vector2 originPos;
        [SerializeField]
        private float duration = 0.25f;

        public Vector3 shapeSelectScale;

        [SerializeField]
        private Vector2 offset = new Vector2(0, 2f);

        private List<Image> listShapeGrid = new List<Image>();
        private List<List<Vector2>> boardPos = new List<List<Vector2>>();
        private Vector3 _shapeStartScale;
        private RectTransform _rectTransform;
        private Canvas _canvas;
        private RectTransform _canvasRect;
        private bool _isDraggable = true;

        private bool _isDragging;

        public List<Image> ListShapeGrid { get => listShapeGrid; set => listShapeGrid = value; }

        //public List<List<Vector2>> BoardPos { get => boardPos; set => boardPos = value; }
        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _shapeStartScale = _rectTransform.localScale;
            _canvas = _rectTransform.parent.GetComponentInParent<Canvas>();
            _canvasRect = _canvas.GetComponent<RectTransform>();
            _isDraggable = true;
            _isDragging = false;
        }
        private void Start()
        {
            //RequestNewShape(CurrentShapeData);
        }
        public void RequestNewShape(ShapeData shapeData)
        {
            CreateShape(shapeData);
        }
        public void CreateShape(ShapeData shapeData)
        {
            _rectTransform.localPosition = originPos;
            _rectTransform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

            CurrentShapeData = shapeData;
            int totalSquareNumber = GetNumerOfSquare(shapeData);
            squareShapeImage = listCellImage[UnityEngine.Random.Range(0, listCellImage.Length)];

            for (int i = 0; i < totalSquareNumber; i++)
            {
                ListShapeGrid.Add(Instantiate(squareShapeImage, transform));
            }
            foreach (var square in ListShapeGrid)
            {
                square.gameObject.transform.position = Vector3.zero;
                square.gameObject.SetActive(false);
            }
            RectTransform squareRect = squareShapeImage.GetComponent<RectTransform>();
            Vector2 moveDistance = new Vector2(squareRect.rect.width * squareRect.localScale.x, squareRect.rect.height * squareRect.localScale.y);

            int currentIndexInList = 0;
            for (int row = 0; row < shapeData.rows; row++)
            {
                //BoardPos.Add(new List<Vector2>());
                for (int col = 0; col < shapeData.columns; col++)
                {
                    if (shapeData.board[row].column[col])
                    {
                        ListShapeGrid[currentIndexInList].gameObject.SetActive(true);
                        RectTransform rect = ListShapeGrid[currentIndexInList].GetComponent<RectTransform>();
                        rect.localPosition = GetCellPosition(shapeData, row, col, moveDistance.x);
                        //BoardPos[row].Add(rect.localPosition);
                        currentIndexInList++;
                    }
                }
            }

            _rectTransform.DOScale(_shapeStartScale, duration).SetEase(Ease.OutBack);

        }

        private Vector2 GetCellPosition(ShapeData shapeData, int row, int col, float cellSize)
        {

            Vector2Int center = GetCenterIndex(shapeData);
            Vector2Int offset = new Vector2Int(row - center.x, col - center.y);

            //double distanceToCenter = Math.Sqrt((offset.x * cellSize) * (offset.x * cellSize) + (offset.y * cellSize) * (offset.y * cellSize));
            return new Vector2(offset.y * cellSize, -offset.x * cellSize);
        }
        private int GetNumerOfSquare(ShapeData shapeData)
        {
            int number = 0;
            foreach (var rowData in shapeData.board)
            {
                foreach (var active in rowData.column)
                {
                    if (active)
                    {
                        number++;
                    }
                }
            }
            return number;
        }

        public Vector2Int[] ListGridIndex(int currentRow, int currentColumn, ShapeData shapeData)
        {
            //ShapeData shapeData = CurrentShapeData;
            List<Vector2Int> listGridIndex = new List<Vector2Int>();
            Vector2Int center = GetCenterIndex(shapeData);
            for (int i = 0; i < shapeData.rows; i++)
            {
                Vector2Int index = Vector2Int.zero;
                for (int j = 0; j < shapeData.columns; j++)
                {
                    if (shapeData.board[i].column[j] == true)
                    {
                        Vector2Int offset = new Vector2Int(center.x - i, center.y - j);
                        index = new Vector2Int(currentRow, currentColumn) - offset;
                        listGridIndex.Add(index);
                    }
                }
            }

            return listGridIndex.ToArray();
        }
        public Vector2Int GetCenterIndex(ShapeData shapeData)
        {
            return new Vector2Int((shapeData.rows - 1) / 2, (shapeData.columns - 1) / 2);
        }
        public void OnBeginDrag(PointerEventData eventData)
        {
            if (!GameController.Instance.isCarrying)
            {
                transform.SetAsLastSibling();
                GameController.Instance.OnCarryingShape();
                GridController.Instance.CurrentShape = this;
                _isDragging = true;
                foreach (Image shapeSquare in ListShapeGrid)
                {
                    shapeSquare.raycastTarget = false;
                }
            }

        }
        public void OnPointerDown(PointerEventData eventData)
        {
            _rectTransform.DOScale(shapeSelectScale, duration);
            transform.DOMoveY(transform.position.y + offset.y, duration);
        }
        public void OnPointerUp(PointerEventData eventData)
        {
            _rectTransform.DOAnchorPos(originPos, duration * 2);
            _rectTransform.DOScale(_shapeStartScale, duration);
        }


        public void OnDrag(PointerEventData eventData)
        {
            if (_isDragging)
            {
                transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
                GridController.Instance.OnDragShape(transform.position);
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (GameController.Instance.isCarrying)
            {
                GameController.Instance.OnDropShape();

                _isDragging = false;
                foreach (Image shapeSquare in ListShapeGrid)
                {
                    shapeSquare.raycastTarget = true;
                }
                if (GridController.Instance.IsDropAble)
                {
                    GridController.Instance.ActiveGhost();
                    GridController.Instance.OnDrop();

                    foreach (Image shapeSquare in ListShapeGrid)
                    {
                        Destroy(shapeSquare.gameObject);
                    }
                    ListShapeGrid.Clear();
                    gameObject.SetActive(false);
                    GridController.Instance.CurrentShape = null;
                    ShapeStorage.OnLockShape?.Invoke(this);
                }
                else
                {
                    //return origin               
                    //GridController.Instance.ResetGhost();               
                }

            }

        }
        public void ClearShape()
        {
            foreach (Image shapeSquare in ListShapeGrid)
            {
                Destroy(shapeSquare.gameObject);
            }
            _rectTransform.localPosition = originPos;
            _rectTransform.localScale = _shapeStartScale;
            ListShapeGrid.Clear();
            gameObject.SetActive(false);
        }


    }
}

