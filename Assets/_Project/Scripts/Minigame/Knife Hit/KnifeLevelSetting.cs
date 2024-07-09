using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "LevelSetting", menuName = "Knife Throw/Level Setting", order = 1)]
[System.Serializable]
public class KnifeLevelSetting : ScriptableObject
{
    public int level;
    [Space]
    [Header("Level setting: ")]
   
    public float maxSpeed = 10;
    public int numKnife = 5;
    public int numObstacle;
    public int numHeart = 1;
    public int numCoin = 0;

    [Space]
    [Header("Rotate => SlowDown => Delay => SpeedUp => NextWave")]
    
    [Tooltip("This will repeat")]
    public Wave[] waves;
    [System.Serializable]
    public class Wave
    {
        public float speedMultiplier = 1;
        public float rotateTime;
        public float slowDownSpeed;
        public float delayTime;
        public float accelerationSpeed;
        public bool isReverse = false;
        
    }
}
