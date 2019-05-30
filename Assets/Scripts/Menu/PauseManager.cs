using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PauseManager : MonoBehaviour
{

	// public GameObject pausePanel;

	public Button resumeButton;
	// public Button restartButton;
	// public Button menuButton;
	// public Button quitButton;

	bool isPaused = false;

	[SerializeField] EventSystem eventSystem;

	[SerializeField] KeyCode pauseKey = KeyCode.Escape;
	[SerializeField] KeyCode pauseJSButton = KeyCode.JoystickButton7;
	[SerializeField] UnityEvent OnPause, OnUnpause;

	void Awake()
	{
		eventSystem = EventSystem.current;
	}

	void Start()
	{
		OnPause.AddListener(Pause);
		OnUnpause.AddListener(Resume);

		// resumeButton.GetComponent<Button>().onClick.AddListener(Resume);
		// restartButton.GetComponent<Button>().onClick.AddListener(Restart);
		// menuButton.GetComponent<Button>().onClick.AddListener(ReturnToMainMenu);
		// quitButton.GetComponent<Button>().onClick.AddListener(QuitGame);
		// pausePanel.SetActive(false);
	}

	void Update()
	{
		if (Input.GetKeyDown(pauseKey) || Input.GetKeyDown(pauseJSButton))
		{
			isPaused = !isPaused;

			if (isPaused)
				OnPause.Invoke();
			else
				OnUnpause.Invoke();
		}


		// if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Joystick1Button7))
		// {
		// 	isPaused = !isPaused;
		// 	if (isPaused)
		// 	{
		// 		Time.timeScale = 0.0f;
		// 		pausePanel.SetActive(true);

		// 		eventSystem.

		// 		// Deselect previous selection and play its deselect transition:
		// 		if (eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().currentSelectedGameObject != null)
		// 		{
		// 			var previous = eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().currentSelectedGameObject.GetComponent<Selectable>();
		// 			if (previous != null)
		// 			{
		// 				previous.OnDeselect(null);
		// 				eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
		// 			}
		// 		}
		// 		// Select button and play its selection transition:
		// 		eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(resumeButton.gameObject);
		// 		resumeButton.OnSelect(null);
		// 	}
		// 	else
		// 	{
		// 		Time.timeScale = 1.0f;
		// 		pausePanel.SetActive(false);
		// 	}
		// }
	}

	public void Pause()
	{
		Time.timeScale = 0f;
		isPaused = true;
		HandleEventSystem();
		// pausePanel.SetActive(true);
	}

	public void Resume()
	{
		Time.timeScale = 1.0f;
		isPaused = false;
		// pausePanel.SetActive(false);
	}

	public void Restart()
	{
		// Time.timeScale = 1.0f;
		SceneManager.LoadScene(1);
	}

	public void ReturnToMainMenu()
	{
		// Time.timeScale = 1.0f;
		SceneManager.LoadScene(0);
	}

	public void QuitGame()
	{
		Application.Quit();
	}

	private void HandleEventSystem()
	{
		// Deselect previous selection and play its deselect transition:
		if (eventSystem.currentSelectedGameObject != null)
		{
			var previous = eventSystem.currentSelectedGameObject.GetComponent<Selectable>();
			if (previous != null)
			{
				previous.OnDeselect(null);
				eventSystem.SetSelectedGameObject(null);
			}
		}
		// Select button and play its selection transition:
		eventSystem.SetSelectedGameObject(resumeButton.gameObject);
		resumeButton.OnSelect(null);
	}
}
