
using System.Collections;
using UnityEngine;

namespace Ricimi
{
    // This class manages the audio source used to play the looping background song
    // in the demo. The player can choose to mute the music, and this preference is
    // persisted via Unity's PlayerPrefs.
    public class BackgroundMusic : MonoBehaviour
    {
        public static BackgroundMusic Instance;

        private AudioSource m_audioSource;

        [SerializeField]
        private Sound[] sounds;

        private void Start()
        {
            if (Instance != null)
            {
                DestroyImmediate(gameObject);
            }
            else
            {
                DontDestroyOnLoad(gameObject);
                Instance = this;
                m_audioSource = GetComponent<AudioSource>();
                m_audioSource.ignoreListenerVolume = true;
                m_audioSource.volume = PlayerPrefs.GetInt("music_on");
                AudioListener.volume = PlayerPrefs.GetInt("sound_on");
            }
        }

        public void FadeIn()
        {
            if (PlayerPrefs.GetInt("music_on") == 1)
            {
                StartCoroutine(FadeAudio(1.0f, Fade.In));
            }
        }

        public void FadeOut()
        {
            if (PlayerPrefs.GetInt("music_on") == 1)
            {
                StartCoroutine(FadeAudio(1.0f, Fade.Out));
            }
        }

        private enum Fade
        {
            In,
            Out
        }

        private IEnumerator FadeAudio(float time, Fade fadeType)
        {
            var start = fadeType == Fade.In ? 0.0f : 1.0f;
            var end = fadeType == Fade.In ? 1.0f : 0.0f;
            var i = 0.0f;
            var step = 1.0f / time;

            while (i <= 1.0f)
            {
                i += step * Time.deltaTime;
                m_audioSource.volume = Mathf.Lerp(start, end, i);
                yield return new WaitForSeconds(step * Time.deltaTime);
            }
        }
        public void PlaySFX(string name)
        {
            Sound s = System.Array.Find(sounds, sound => sound.name == name);
            if (s != null)
            {
                m_audioSource.clip = s.clip;
                m_audioSource.Play();
                //m_audioSource.PlayOneShot(s.clip, s.volume);
            }
        }
        public void PlayLivingRoom()
        {
            PlaySFX("LIVINGROOM");
        }
        public void PlayMiniGame()
        {
            PlaySFX("MINIGAME");            
        }
        public void PlayMinigameChoose()
        {
            PlaySFX("SELECTION");
        }
    }
}
