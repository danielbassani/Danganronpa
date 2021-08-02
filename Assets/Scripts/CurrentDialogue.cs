using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentDialogue : MonoBehaviour
{
    public string[] currentDialogueNames;
    public string[] currentDialogue;
    public Sprite[] currentDialogueSprites;
    public AudioClip[] currentAudioClips;

    public string[] getCurrentDialogueNames()
    {
        return currentDialogueNames;
    }

    public string[] getCurrentDialogue()
    {
        return currentDialogue;
    }

    public Sprite[] getCurrentSprites()
    {
        return currentDialogueSprites;
    }

    public AudioClip[] getCurrentAudioClips()
    {
        return currentAudioClips;
    }
}
