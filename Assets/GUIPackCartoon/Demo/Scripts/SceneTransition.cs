
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ricimi
{
    // This class is responsible for loading the next scene in a transition (the core of
    // this work is performed in the Transition class, though).
    public class SceneTransition : MonoBehaviour
    {
        public string scene = "<Insert scene name>";
        public float duration = .5f;
        public Color color = Color.black;

        public void SetTransitionScene(string sceneName, float duration = 0.5f)
        {
            scene = sceneName;
            this.duration = duration;
        }
        public void PerformTransition()
        {
            //Transition.LoadLevel(scene, duration, color);
            if (SceneTransitionHandle.Instance)
            {
                SceneTransitionHandle.Instance.LoadScene(scene);
            }
            else
            {
                Transition.LoadLevel(scene, duration, color);
            }
            
        }
        public void ReloadCurrentScene()
        {
            //Transition.LoadLevel(SceneManager.GetActiveScene().name, duration, color);
            if (SceneTransitionHandle.Instance)
            {
                SceneTransitionHandle.Instance.LoadScene(SceneManager.GetActiveScene().name);
            }
            else
            {
                Transition.LoadLevel(SceneManager.GetActiveScene().name, duration, color);
            }
                
        }
    }
}
