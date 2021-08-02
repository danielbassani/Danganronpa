using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeWriterEffect : MonoBehaviour
{
    public float delay = 0.015f;
    public string fullText;
    public bool updateText = false;
    private string currentText = "";

    // Update is called once per frame
    void Update()
    {
        if (updateText)
        {
            StartCoroutine(ShowText());
            updateText = false;
        }
    }
    
    //scrolls text, must be stopped if you need to display a new line of dialogue, updateText must be true for it to be called
    public IEnumerator ShowText()
    {
        for(int i = 0; i <= fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i);
            this.GetComponent<Text>().text = currentText;
            yield return new WaitForSeconds(delay);
        }
    }
}
