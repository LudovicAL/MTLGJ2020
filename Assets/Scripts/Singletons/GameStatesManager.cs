//This script keeps track of the current game-state and warns other scripts when the game-state happens to change.
//Place a unique instance of it somewhere in your game, on a dummy GameObject.

using UnityEngine;
using UnityEngine.Events;

public class GameStatesManager : MonoBehaviour {

	public enum AvailableGameStates {
		Menu,   //Consulting the menu
		Starting,   //Game is starting
		Playing,    //Game is playing
		Pausing, //Game is paused
		Ending  //Game is ending
	};

	public AvailableGameStates gameStateAtLaunch = AvailableGameStates.Menu;

    //The emitter
    public UnityEvent GameStateChanged;
	
    //The following variable contains the current GameState.
	public AvailableGameStates gameState {get; private set;}

    public static GameStatesManager Instance {get; private set;}

    void Awake() {
        //Makes this script a singleton

        if (Instance == null) {
            Instance = this;
        } else if (Instance != this) {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        //Sets the GameState at launch
        ChangeGameStateTo(gameStateAtLaunch);

        //Registers the events
        GameStateChanged = new UnityEvent();       
    }

	//Call this function from anywhere to request a game state change
	public void ChangeGameStateTo(AvailableGameStates desiredState) {
		gameState = desiredState;
		GameStateChanged.Invoke ();
	}
}
