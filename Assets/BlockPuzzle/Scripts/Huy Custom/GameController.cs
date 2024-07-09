using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TeraJet;
using System;
using UnityEngine.SceneManagement;
namespace BlockPuzzle
{
    public class GameController : Singleton<GameController>
    {
        public static Action<int> OnScoreChange;

        public static Action OnOutOfPlace;

        public static Action OnGameOver;

        public bool isCarrying;

        public int score;

        private void Start()
        {
            isCarrying = false;
            score = 0;
            Application.targetFrameRate = 300;
            OnGameOver += OpenHomeScene;
        }
        public void OnDestroy()
        {
            OnGameOver -= OpenHomeScene;
        }
        public void AddPoint(int value)
        {
            score += value;
            OnScoreChange?.Invoke(value);
        }

        public void OnCarryingShape()
        {
            isCarrying = true;
        }
        public void OnDropShape()
        {
            isCarrying = false;
        }
        public void OpenHomeScene()
        {
            SceneManager.LoadSceneAsync("HomeScene");
        }
    }

}
