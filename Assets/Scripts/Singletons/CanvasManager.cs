using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour {

	public GameObject panelMenu;
	public GameObject panelStarting;
	public GameObject panelPlaying;
	public GameObject panelPausing;
	public GameObject panelEnding;
	public static CanvasManager Instance { get; private set; }

	private void Awake() {
		if (Instance == null) {
			Instance = this;
		} else if (Instance != this) {
			Destroy(gameObject);
		}
		DontDestroyOnLoad(gameObject);
	}

	// Start is called before the first frame update
	void Start() {
		GameStatesManager.Instance.GameStateChanged.AddListener(OnGameStateChange);
		OnGameStateChange();
	}

	// Update is called once per frame
	void Update() {

	}

	//Called when the GameState changes
	private void OnGameStateChange() {
		switch (GameStatesManager.Instance.gameState) {
			case (GameStatesManager.AvailableGameStates.Menu):
				ShowPanel("Panel Menu");
				break;
			case (GameStatesManager.AvailableGameStates.Starting):
				ShowPanel("Panel Starting");
				break;
			case (GameStatesManager.AvailableGameStates.Playing):
				ShowPanel("Panel Playing");
				break;
			case (GameStatesManager.AvailableGameStates.Pausing):
				ShowPanel("Panel Pausing");
				break;
			case (GameStatesManager.AvailableGameStates.Ending):
				ShowPanel("Panel Ending");
				break;
		}
	}

	private void ShowPanel(string panelName) {
		panelMenu.SetActive(panelMenu.name.Equals(panelName));
		panelStarting.SetActive(panelStarting.name.Equals(panelName));
		panelPlaying.SetActive(panelPlaying.name.Equals(panelName));
		panelPausing.SetActive(panelPausing.name.Equals(panelName));
		panelEnding.SetActive(panelEnding.name.Equals(panelName));
	}
}