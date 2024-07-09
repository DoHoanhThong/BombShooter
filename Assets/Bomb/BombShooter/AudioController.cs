using Ricimi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    private static AudioController _instant;
    public static AudioController Instant => _instant;
    public AudioSource soundsource;
    [SerializeField] AudioSource _bgMusic;
    private void Awake()
    {
        soundsource = this.GetComponent<AudioSource>();

        if (_instant == null)
        {
            _instant = this;
            soundsource = this.GetComponent<AudioSource>();
            return;
        }
        if (_instant.gameObject.GetInstanceID() != this.gameObject.GetInstanceID())
        {
            Destroy(this.gameObject);
        }
    }
    private void OnDestroy()
    {
        BombShooterController.End -= DisabledBgMusic;
    }
    public void PlaySound(AudioClip sound)
    {
        soundsource.PlayOneShot(sound);//phat am thanh 1 lan
    }
    public void DisabledBgMusic()
    {
        _bgMusic.enabled = false;
    }
}
