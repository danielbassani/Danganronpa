using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSounds : MonoBehaviour
{
    public AudioClip menuSelect;
    public AudioClip textConfirm;

    public void PlaySound(string clipName)
    {
        if(clipName == "menuSelect")
        {
            this.GetComponent<AudioSource>().PlayOneShot(menuSelect);
        }
        else if(clipName == "textConfirm")
        {
            this.GetComponent<AudioSource>().PlayOneShot(textConfirm);
        }
        
    }
}
