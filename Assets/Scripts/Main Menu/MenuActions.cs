using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

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
    public GameObject loadingPanel;
    float duration = 0.85f;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    public void PressStart()
    {
        audioSource.PlayOneShot(buttonPress);
        StartCoroutine(FadeOut(mainMenuPanel, duration));
        StartCoroutine(FadeIn(gameSelectPanel, duration));
        mainMenuPanel.SetActive(false);
        gameSelectPanel.SetActive(true);
    }

    public void PressQuit()
    {
        audioSource.PlayOneShot(buttonPress);
        StartCoroutine(FadeOut(gameSelectPanel, duration));
        StartCoroutine(FadeIn(mainMenuPanel, duration));
        gameSelectPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void PressNewGame()
    {
        audioSource.PlayOneShot(buttonPress);
        StartCoroutine(FadeOut(gameSelectPanel, duration));
        StartCoroutine(FadeIn(newGamePanel, duration));
        gameSelectPanel.SetActive(false);
        newGamePanel.SetActive(true);
    }

    public void NewGameYes()
    {
        audioSource.PlayOneShot(confirm);
        StartCoroutine(FadeOut(gameSelectPanel, duration)); // make fade out and fade in longer
        StartCoroutine(FadeIn(loadingPanel, duration)); // make fade out and fade in longer
        gameSelectPanel.SetActive(false);
        loadingPanel.SetActive(true);
        StartCoroutine(WaitToSwitchScenes());
    }

    public void NewGameNo()
    {
        audioSource.PlayOneShot(no);
        StartCoroutine(FadeOut(newGamePanel, duration));
        StartCoroutine(FadeIn(gameSelectPanel, duration));
        newGamePanel.SetActive(false);
        gameSelectPanel.SetActive(true);
    }

    public void PressLoadGame()
    {
        audioSource.PlayOneShot(buttonPress);
        StartCoroutine(FadeOut(gameSelectPanel, duration)); // make fade out and fade in longer
        StartCoroutine(FadeIn(loadingPanel, duration)); // make fade out and fade in longer
        gameSelectPanel.SetActive(false);
        loadingPanel.SetActive(true);
        StartCoroutine(WaitToSwitchScenes());
    }

    /*
     * This method fades in a panel
     */
    public IEnumerator FadeIn(GameObject panel, float duration)
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
    public IEnumerator FadeOut(GameObject panel, float duration)
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

    /*
     * Method waits to switch scenes
     */
    IEnumerator WaitToSwitchScenes()
    {
        yield return new WaitForSeconds(3.2f);
        SceneManager.LoadScene(1);
    }
}
