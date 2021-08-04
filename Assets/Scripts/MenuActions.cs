using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuActions : MonoBehaviour
{
    // audio
    public AudioSource audioSource;
    public AudioClip buttonPress;
    public AudioClip confirm;
    public AudioClip no;

    // various UI panels
    public GameObject mainMenuPanel;
    public GameObject gameSelectPanel;
    public GameObject newGamePanel;
    float duration = 0.85f;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    public void PressStart()
    {
        audioSource.PlayOneShot(buttonPress);
        StartCoroutine(FadeOut(mainMenuPanel));
        StartCoroutine(FadeIn(gameSelectPanel));
        mainMenuPanel.SetActive(false);
        gameSelectPanel.SetActive(true);
    }

    public void PressQuit()
    {
        audioSource.PlayOneShot(buttonPress);
        StartCoroutine(FadeOut(gameSelectPanel));
        StartCoroutine(FadeIn(mainMenuPanel));
        gameSelectPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void PressNewGame()
    {
        audioSource.PlayOneShot(buttonPress);
        StartCoroutine(FadeOut(gameSelectPanel));
        StartCoroutine(FadeIn(newGamePanel));
        gameSelectPanel.SetActive(false);
        newGamePanel.SetActive(true);
    }

    public void NewGameYes()
    {
        audioSource.PlayOneShot(confirm);
    }

    public void NewGameNo()
    {
        audioSource.PlayOneShot(no);
        StartCoroutine(FadeOut(newGamePanel));
        StartCoroutine(FadeIn(gameSelectPanel));
        newGamePanel.SetActive(false);
        gameSelectPanel.SetActive(true);
    }

    /*
     * This method fades in a panel
     */
    public IEnumerator FadeIn(GameObject panel)
    {
        float start = 0f, end = 1f;
        float counter = 0f;
        Color curColor = panel.GetComponent<Image>().color;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            curColor.a = Mathf.Lerp(start, end, counter / duration);
            panel.GetComponent<Image>().color = curColor;

            yield return null;
        }
    }

    /*
     * This method fades out a panel
     */
    public IEnumerator FadeOut(GameObject panel)
    {
        float start = 1f, end = 0f;
        float counter = 0f;
        Color curColor = panel.GetComponent<Image>().color;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            curColor.a = Mathf.Lerp(start, end, counter / duration);
            panel.GetComponent<Image>().color = curColor;

            yield return null;
        }
    }
}
