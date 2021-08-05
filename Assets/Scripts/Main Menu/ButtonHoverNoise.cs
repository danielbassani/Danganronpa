using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHoverNoise : MonoBehaviour, IPointerEnterHandler
{
    public AudioSource audioSource;
    public AudioClip hoverSound;

    public void OnPointerEnter(PointerEventData eventData)
    {
        audioSource.PlayOneShot(hoverSound);
    }
}
