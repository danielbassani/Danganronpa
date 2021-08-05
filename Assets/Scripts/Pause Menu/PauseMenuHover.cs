using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseMenuHover : MonoBehaviour, IPointerEnterHandler
{
    public Image displayImage;
    public Sprite spriteToDisplay;

    public void OnPointerEnter(PointerEventData eventData)
    {
        displayImage.sprite = spriteToDisplay;
    }
}
