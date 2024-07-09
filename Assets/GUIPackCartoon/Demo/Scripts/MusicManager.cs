
using UnityEngine;
using UnityEngine.UI;

namespace Ricimi
{
    // This class handles updating the music UI widgets depending on the player's selection.
    public class MusicManager : MonoBehaviour
    {
        private Slider m_musicSlider;
        private GameObject m_musicButton;

        private float m_sliderValue;

        private void Start()
        {
            m_musicSlider = GetComponent<Slider>();
            m_sliderValue = PlayerPrefs.GetInt("music_on");
            m_musicSlider.value = m_sliderValue;
            m_musicButton = GameObject.Find("MusicButton/Button");
        }

        public void SwitchMusic()
        {
            m_sliderValue = m_sliderValue == 0 ? 1 : 0;
            m_musicSlider.value = m_sliderValue;
            var backgroundAudioSource = GameObject.Find("BackgroundMusic").GetComponent<AudioSource>();
            backgroundAudioSource.volume = m_musicSlider.value;
            PlayerPrefs.SetInt("music_on", (int)m_musicSlider.value);
            if (m_musicButton != null)
                m_musicButton.GetComponent<MusicButton>().ToggleSprite();
        }
    }
}
