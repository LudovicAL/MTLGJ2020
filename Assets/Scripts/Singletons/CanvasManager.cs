using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour {

	public GameObject panelMenu;
	public GameObject panelStarting;
	public GameObject panelPlaying;
	public GameObject panelPausing;
	public GameObject panelEnding;
	public Text countDownText;
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
		EventsManager.Instance.gameStateChanges.AddListener(OnGameStateChanges);
		OnGameStateChanges();
	}

	// Update is called once per frame
	void Update() {

	}

	//Called when the GameState changes
	private void OnGameStateChanges() {
		switch (GameStatesManager.Instance.gameState) {
			case (GameStatesManager.AvailableGameStates.Menu):
				ShowPanel("Panel Menu");
				break;
			case (GameStatesManager.AvailableGameStates.Starting):
				ShowPanel("Panel Starting");
				StartCoroutine(CountDown());
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

	private IEnumerator CountDown() {
		AudioManager.Instance.PlayClipOneShot(AudioManager.Instance.countDownClip);
		for (int i = 3; i > 0; i--) {
			countDownText.text = i.ToString();
			yield return new WaitForSeconds(0.7f);
		}
		GameStatesManager.Instance.ChangeGameStateTo(GameStatesManager.AvailableGameStates.Playing);
	}
}