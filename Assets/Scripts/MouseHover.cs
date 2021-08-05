using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Sprite normalSprite;
    public Sprite hoverSprite;

    public void OnPointerEnter(PointerEventData eventData)
    {
        gameObject.GetComponent<Image>().sprite = hoverSprite;
        gameObject.GetComponentInChildren<Text>().color = Color.black;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gameObject.GetComponent<Image>().sprite = normalSprite;
        gameObject.GetComponentInChildren<Text>().color = Color.white;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        gameObject.GetComponent<Image>().sprite = normalSprite;
        gameObject.GetComponentInChildren<Text>().color = Color.white;
    }
}
