using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
namespace BlockPuzzle
{
    public abstract class Popup : MonoBehaviour
    {
        [SerializeField]
        protected CanvasGroup canvas;
        [SerializeField]
        protected RectTransform popup;
        [SerializeField]
        protected Ease popupEase = Ease.OutBack;
        [SerializeField]
        protected float duration = 0.25f;

        public abstract void OpenPopup();
        public abstract void ClosePopup();
    }

}
