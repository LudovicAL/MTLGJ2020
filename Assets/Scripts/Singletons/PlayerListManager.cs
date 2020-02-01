using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerListManager : MonoBehaviour {
	public int maxNumPlayers;
	public List<PlayerId> listOfPlayers {get; private set;}
    public List<PlayerId> listOfAvailablePlayers;
	public int currentPlayerCount;

	public static PlayerListManager Instance {get; private set;}

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else if (Instance != this) {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start () {
		currentPlayerCount = 0;
		maxNumPlayers = Mathf.Min(maxNumPlayers, listOfAvailablePlayers.Count);
        listOfPlayers = new List<PlayerId> ();
    }

	void Update () {
		if (GameStatesManager.Instance.gameState == GameStatesManager.AvailableGameStates.Menu) {
			for (int i = listOfPlayers.Count - 1; i >= 0; i--) {
				if (listOfPlayers[i].controls.GetButtonBDown()) {
					RemovePlayer(listOfPlayers[i]);
				}
			}
			for (int i = listOfAvailablePlayers.Count - 1; i >= 0; i--) {
				if (listOfAvailablePlayers[i].controls.GetButtonADown()) {
					AddPlayer(listOfAvailablePlayers[i]);
				}
			}
			for (int i = 0, max = listOfPlayers.Count; i < max; i++) {
				if (listOfPlayers[i].controls.GetButtonStartDown()) {
					GameStatesManager.Instance.ChangeGameStateTo(GameStatesManager.AvailableGameStates.Starting);
				}
			}
		}
	}

	//Adds a player to the game
	private void AddPlayer(PlayerId playerId) {
		if (listOfPlayers.Count < maxNumPlayers) {
            listOfPlayers.Add(playerId);
            listOfAvailablePlayers.Remove(playerId);
			currentPlayerCount = listOfPlayers.Count;
			bool gameFull = (currentPlayerCount < maxNumPlayers) ? false : true;
			EventsManager.Instance.playerJoinsGame.Invoke(playerId, gameFull);
		}
	}

	//Removes a player from the game
	private void RemovePlayer(PlayerId playerId) {
        listOfAvailablePlayers.Add(playerId);
        listOfPlayers.Remove(playerId);
		currentPlayerCount = listOfPlayers.Count;
		EventsManager.Instance.playerLeavesGame.Invoke(playerId);
	}
}
