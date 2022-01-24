using UnityEngine;

public class PauseMenuActions : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject presentsUI;

    public void ClickPresents()
    {
        pauseMenuUI.SetActive(false);
        presentsUI.SetActive(true);
    }

    public void SelectAPresent()
    {
        
    }
}
