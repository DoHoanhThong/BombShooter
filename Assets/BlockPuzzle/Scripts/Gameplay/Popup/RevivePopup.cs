using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
namespace BlockPuzzle
{
    public class RevivePopup : Popup
    {
        private bool isRevived = false;
        private void Start()
        {
            isRevived = false;
            GameController.OnOutOfPlace += OpenPopup;
        }
        private void OnDestroy()
        {
            GameController.OnOutOfPlace -= OpenPopup;
        }
        public override void ClosePopup()
        {
            popup.DOAnchorPosY(1000, duration).SetEase(Ease.InBack);
            canvas.DOFade(0, duration).OnComplete(() =>
            {
                canvas.gameObject.SetActive(false);
            });
        }

        public override void OpenPopup()
        {
            if (isRevived)
            {
                //GameController.OnGameOver?.Invoke();
                //return;
            }
            else
            {
                isRevived = true;
            }
            canvas.gameObject.SetActive(true);
            canvas.DOFade(1, duration);
            popup.DOAnchorPosY(300, duration).SetEase(popupEase);
        }

    }

}
