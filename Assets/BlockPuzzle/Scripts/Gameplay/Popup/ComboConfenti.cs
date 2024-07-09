using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
namespace BlockPuzzle
{
    public class ComboConfenti : MonoBehaviour
    {
        [SerializeField]
        private RectTransform[] combo;

        [SerializeField]
        private float duration = 0.25f;
        [SerializeField]
        private float showDuration = 0.5f;
        private GameObject currentCombo;

        private Tween currentTween;

        public void ShowConfenti(int numCombo)
        {
            switch (numCombo)
            {
                case 0:
                    break;
                case 1:
                    //sound combo
                    break;
                case 2:
                    Action(combo[0]);
                    break;
                case 3:
                    Action(combo[1]);
                    break;
                case 4:
                    Action(combo[2]);
                    break;
                case 5:
                    Action(combo[3]);
                    break;
                default:
                    Action(combo[3]);
                    break;
            }
        }
        private void Action(RectTransform combo)
        {
            if (currentCombo != null)
            {
                currentTween.Kill();
                currentCombo.SetActive(false);
                currentCombo.transform.localScale = Vector2.zero;
            }
            currentCombo = combo.gameObject;
            currentCombo.gameObject.SetActive(true);
            currentTween = currentCombo.transform.DOScale(1, duration).OnComplete(() =>
            {
                currentCombo.transform.DOScale(0, duration).SetEase(Ease.InBack).OnComplete(() =>
                {
                    currentCombo.gameObject.SetActive(false);
                    currentCombo.transform.localScale = Vector2.zero;
                    currentCombo = null;
                }).SetDelay(showDuration);
            });
        }
    }

}
