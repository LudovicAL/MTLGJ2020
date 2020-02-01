//Here is an example of how to implement game state management on an object that inherit MonoBehaviour.
using UnityEngine;

public class ExampleOfAScriptUsingGameStateManagement : MonoBehaviour {

	//A listener is placed on Start()
	void Start () {
        EventsManager.Instance.gameStateChanges.AddListener(OnGameStateChanges);
		OnGameStateChanges();
	}
		
	//Here is an example of how to execute stuff only when a specified GameState is on
	void Update () {
        if (GameStatesManager.Instance.gameState == GameStatesManager.AvailableGameStates.Playing) {
            //Do stuff...
		}
	}

	//Called when the GameState changes
	private void OnGameStateChanges() {
		switch (GameStatesManager.Instance.gameState) {
            case (GameStatesManager.AvailableGameStates.Menu):
                break;
            case (GameStatesManager.AvailableGameStates.Starting):
				break;
            case (GameStatesManager.AvailableGameStates.Playing):
				break;
            case (GameStatesManager.AvailableGameStates.Pausing):
				break;
            case (GameStatesManager.AvailableGameStates.Ending):
				break;
        }
    }
}
