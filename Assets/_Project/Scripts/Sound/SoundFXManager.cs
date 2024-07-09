using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TeraJet;

public class SoundFXManager : Singleton<SoundFXManager>
{
    public Sound[] sounds;

    public AudioSource sortSource;

    public void PlaySFX(string name)
    {
        Sound s = System.Array.Find(sounds, sound => sound.name == name);
        if (s != null)
        {
            sortSource.PlayOneShot(s.clip, s.volume);
        }
    } 
    public void PlaySound(AudioClip a)
    {
        sortSource.PlayOneShot(a);
    }
    public void PlayButtonClick()
    {
        PlaySFX("BUTTONCLICK");
    }
    public void PlayLevelComplete()
    {
        PlaySFX("LEVELCOMPLETE");
    }
    public void PlayCoinCollect()
    {
        PlaySFX("COINCOLLECT");
    }
    public void PlayCatLaugh()
    {
        PlaySFX("CATLAUGH");
    }
    public void PlayCatYes()
    {
        PlaySFX("CATYES");
    }
    public void PlayKnifeHit()
    {
        PlaySFX("KNIFEHIT");
    }
    public void PlayKnifeHitIron()
    {
        PlaySFX("HITIRON");
    }
    public void PlayKnifeComplete()
    {
        PlaySFX("KNIFECOMPLETE");
    }
    public void PlayCardCorrect()
    {
        PlaySFX("CARDCORRECT");
    }
    public void PlayCardFlip()
    {
        PlaySFX("FLIPCARD");
    }
    //public void Play
    public void PlayCatHappy()
    {
        PlaySFX("MEOW" + ((int)Random.Range(1, 6)).ToString());
    }
    public void PlayCatSad()
    {
        PlaySFX("CATSAD" + ((int)Random.Range(1, 4)).ToString());
    }
    public void PlayCatEat()
    {
        PlaySFX("CATEAT" + ((int)Random.Range(1, 4)).ToString());
    }
    public void PlayWateringBubble()
    {
        PlaySFX("WATERING");
    }
}
