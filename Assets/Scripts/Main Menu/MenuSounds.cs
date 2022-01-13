using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSounds : MonoBehaviour
{
    public AudioClip menuSelect;
    public AudioClip textConfirm;
    public AudioClip menuBack;
    public AudioClip no;
    public AudioClip pause;
    public AudioClip unPause;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
    }

    public void PlaySound(string clipName)
    {
        if (clipName == "menuSelect")
        {
            audioSource.PlayOneShot(menuSelect);
        }
        else if (clipName == "textConfirm")
        {
            audioSource.PlayOneShot(textConfirm);
        }
        else if (clipName == "menuBack")
        {
            audioSource.PlayOneShot(menuBack);
        }
        else if (clipName == "no")
        {
            audioSource.PlayOneShot(no);
        }
        else if (clipName == "pause")
        {
            audioSource.PlayOneShot(pause);
        }
        else if (clipName == "unPause")
        {
            audioSource.PlayOneShot(unPause);
        }

    }
}
