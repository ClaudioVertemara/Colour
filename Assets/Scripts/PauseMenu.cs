using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Project: Colour
 * Code Author: Claudio Vertemara
*/

public class PauseMenu : MonoBehaviour
{
    AudioSource buttonAudio;
    public GameObject continueButton;
    public GameObject mainMenuButton;

    public static bool pause;

    // Start is called before the first frame update
    void Start()
    {
        buttonAudio = GameObject.Find("ButtonAudio").GetComponent<AudioSource>();
        continueButton.SetActive(false);
        mainMenuButton.SetActive(false);

        pause = false;
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            pause = !pause;

            if (pause) {
                continueButton.SetActive(true);
                mainMenuButton.SetActive(true);
                Time.timeScale = 0;
            } else {
                continueButton.SetActive(false);
                mainMenuButton.SetActive(false);
                Time.timeScale = 1;
            }

            buttonAudio.Play();
        }
    }

    public void ContinueButton() {
        Time.timeScale = 1;
        pause = false;

        continueButton.SetActive(false);
        mainMenuButton.SetActive(false);
        buttonAudio.Play();
    }

    public void MainMenuButton() {
        SceneManager.LoadScene("Menu");
        buttonAudio.Play();
    }
}
