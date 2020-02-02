using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartManager : MonoBehaviour {

	public List<GameObject> availableHouses;
	public static GameStartManager Instance { get; private set;}

	private void Awake() {
		if (Instance == null) {
			Instance = this;
		} else if (Instance != this) {
			Destroy(gameObject);
		}
		DontDestroyOnLoad(gameObject);
	}

	void Start() {
		EventsManager.Instance.gameStateChanges.AddListener(OnGameStateChanges);
	}

    void Update() {
        
    }

	//Called when the GameState changes
	private void OnGameStateChanges() {
		switch (GameStatesManager.Instance.gameState) {
			case (GameStatesManager.AvailableGameStates.Menu):
				break;
			case (GameStatesManager.AvailableGameStates.Starting):
				SpawnHouse();
				break;
			case (GameStatesManager.AvailableGameStates.Playing):
				break;
			case (GameStatesManager.AvailableGameStates.Pausing):
				break;
			case (GameStatesManager.AvailableGameStates.Ending):
				break;
		}
	}

	private void SpawnHouse() {
		GameObject house = Instantiate(availableHouses[Random.Range(0, availableHouses.Count)], Vector3.zero, Quaternion.identity);
		GameObject[] spawnList = GameObject.FindGameObjectsWithTag("SpawnPoint");
		for (int i = 0, max = PlayerListManager.Instance.listOfPlayers.Count; i < max; i++) {
			PlayerListManager.Instance.listOfPlayers[i].avatar.transform.position = spawnList[i].transform.position;
		}
	}
}
