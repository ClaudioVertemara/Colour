using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Project: Colour
 * Code Author: Claudio Vertemara
*/

public class Menu : MonoBehaviour
{
    AudioSource buttonAudio;

    public GameObject titleScreen;
    public GameObject howToPlayPanel;
    public GameObject creditsPanel;

    // Start is called before the first frame update
    void Start()
    {
        buttonAudio = GameObject.Find("ButtonAudio").GetComponent<AudioSource>();
        titleScreen.SetActive(true);
        howToPlayPanel.SetActive(false);
        creditsPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayButton() {
        SceneManager.LoadScene("LoadScene");
        buttonAudio.Play();
    }

    public void HowToPlayButton() {
        buttonAudio.Play();
        titleScreen.SetActive(false);
        howToPlayPanel.SetActive(true);
    }

    public void CreditsButton() {
        buttonAudio.Play();
        titleScreen.SetActive(false);
        creditsPanel.SetActive(true);
    }

    public void ExitGameButton() {
        Application.Quit();
        buttonAudio.Play();
    }

    public void BackButton() {
        buttonAudio.Play();
        titleScreen.SetActive(true);
        howToPlayPanel.SetActive(false);
        creditsPanel.SetActive(false);
    }
}
