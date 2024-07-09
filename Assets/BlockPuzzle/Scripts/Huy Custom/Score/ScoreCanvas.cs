using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using HwiTera;
namespace BlockPuzzle
{
    public class ScoreCanvas : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text scoreText;
        [SerializeField]
        private TMP_Text timeText;
        [Header("Effect")]
        [SerializeField]
        private ScoreText addScoreText;

        private int timeCounter = 0;
        private void Start()
        {
            GameController.OnScoreChange += UpdateScore;
            timeCounter = 0;
            StartCoroutine(TimeCounter());
        }
        private void OnDestroy()
        {
            GameController.OnScoreChange -= UpdateScore;
        }
        private void UpdateScore(int value)
        {
            scoreText.text = GameController.Instance.score.ToString();
            Instantiate(addScoreText, Vector2.zero, Quaternion.identity, transform.parent).OnSpawn(value);
        }
        IEnumerator TimeCounter()
        {
            WaitForSeconds wait = new WaitForSeconds(1);
            while (true)
            {
                timeText.text = timeCounter.SecondsToMinute();
                yield return wait;
                timeCounter++;
            }
        }
    }

}
