using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TeraJet;
namespace BlockPuzzle
{
    public class GameManager : Singleton<GameManager>
    {
        public UserData userData;

        private void Start()
        {
            Application.targetFrameRate = 300;
        }
    }

}
