using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
namespace BlockPuzzle
{
    public class GridSquare : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private Status status;

        [SerializeField]
        private Image normalImage;
        [SerializeField]
        private Image hoverImage;
        [SerializeField]
        private Image activeImage;
        [SerializeField]
        private Image previewImage;
        [SerializeField]
        private List<Sprite> normalImages;

        [HideInInspector]
        public bool isActive;
        [HideInInspector]
        public bool isGhost;
        private int row;
        private int column;

        public enum Status
        {
            EMPTY,
            GHOST,
            ACTIVE,
            PREVIEW,
        }

        private void Start()
        {
            isActive = false;
            isGhost = false;
        }

        public void SetState(Status status)
        {
            this.status = status;
            switch (status)
            {
                case Status.EMPTY:
                    normalImage.gameObject.SetActive(true);
                    break;
                case Status.GHOST:
                    break;
                case Status.ACTIVE:
                    break;
                case Status.PREVIEW:
                    break;
            }
        }

        public void OnSpawn(int row, int column)
        {
            this.row = row;
            this.column = column;
        }
        public void SetImage(bool setFirstImage)
        {
            normalImage.GetComponent<Image>().sprite = setFirstImage ? normalImages[1] : normalImages[0];
        }
        public void ShowGhost(bool isShow)
        {
            isGhost = isShow;
            if (isShow)
            {
                hoverImage.gameObject.SetActive(true);
            }
            else
            {
                hoverImage.gameObject.SetActive(false);
            }
        }
        public void Active()
        {
            isActive = true;
            isGhost = false;
            normalImage.gameObject.SetActive(false);
            hoverImage.gameObject.SetActive(false);
            activeImage.gameObject.SetActive(true);
            previewImage.gameObject.SetActive(false);
        }
        public void Clear()
        {
            isActive = false;
            isGhost = false;
            hoverImage.gameObject.SetActive(false);
            activeImage.gameObject.SetActive(false);
            normalImage.gameObject.SetActive(true);
            previewImage.gameObject.SetActive(false);
        }
        public void Review()
        {
            hoverImage.gameObject.SetActive(false);
            activeImage.gameObject.SetActive(false);
            normalImage.gameObject.SetActive(false);
            previewImage.gameObject.SetActive(true);
        }
        public void UnReview()
        {
            hoverImage.gameObject.SetActive(false);
            previewImage.gameObject.SetActive(false);
            if (isActive)
            {
                activeImage.gameObject.SetActive(true);
            }
            else
            {
                normalImage.gameObject.SetActive(true);
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            //if (GameController.Instance.isCarrying)
            //{
            //    GridController.Instance.OnShapeInGrid(row, column);
            //}
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            //hoverImage.gameObject.SetActive(false);
            //if (GameController.Instance.isCarrying)
            //{

            //}

        }
    }

}
