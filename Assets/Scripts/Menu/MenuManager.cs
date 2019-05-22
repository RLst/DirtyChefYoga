﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    // Main Buttons
    public Button playButton;
    public Button controlsButton;
    public Button exitButton;

    // UI Panels
    public GameObject menuPanel;
    public GameObject controlsPanel;

    // Back Buttons
    public Button backButton;

    // View State
    CurrentView currentView;
    public enum CurrentView {

        MenuView,
        ControlsView,

    }

    void Start () {

        // Assign buttons to methods
        playButton.GetComponent<Button>().onClick.AddListener(PressPlay);
        controlsButton.GetComponent<Button>().onClick.AddListener(PressControls);
        exitButton.GetComponent<Button>().onClick.AddListener(PressExit);

        backButton.GetComponent<Button>().onClick.AddListener(PressBack);
        
        // Set the view to the main menu
        currentView = CurrentView.MenuView;
    }

    void Update() {

        // Show and hide panels depending on view
        switch (currentView) {
            case CurrentView.MenuView:
                menuPanel.SetActive(true);
                controlsPanel.SetActive(false);
                break;

            case CurrentView.ControlsView:
                menuPanel.SetActive(false);
                controlsPanel.SetActive(true);
                break;

            default:
                menuPanel.SetActive(true);
                controlsPanel.SetActive(false);
                break;
        }

    }

    void PressPlay() {
        SceneManager.LoadScene(1);
    }

    void PressControls() {
        currentView = CurrentView.ControlsView;
    }

    void PressExit() {
        Application.Quit();
    }

    void PressBack() {
        currentView = CurrentView.MenuView;
    }
}
