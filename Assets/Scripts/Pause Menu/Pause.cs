using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    // UI menus
    public GameObject HUD;
    public GameObject pauseMenu;

    // player camera
    public Camera playerCamera;

    // duration to fade out
    float duration = 0.35f;

    // boolean to determine if currently paused
    bool paused = false;

    // Update is called once per frame
    void Update()
    {
       if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!paused)
            {
                Time.timeScale = 0f;
                HUD.SetActive(false);
                pauseMenu.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                paused = true;
            }
            else
            {
                Time.timeScale = 1f;
                pauseMenu.SetActive(false);
                HUD.SetActive(true);
                Cursor.lockState = CursorLockMode.Locked;
                paused = false;   
            }
        }
    }
}
