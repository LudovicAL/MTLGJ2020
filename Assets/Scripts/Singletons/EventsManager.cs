using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class PlayerCollidesWithDoor : UnityEvent<PlayerControler, DoorData> {
	//arg1: The player that collides with a door
	//arg2: The door that is being collided with
}
[System.Serializable]
public class PlayerUncollidesWithDoor : UnityEvent<PlayerControler, DoorData, DoorData> {
	//arg1: The player that stopped colliding with a door
	//arg2: The door that stopped being collided with
	//arg3: Any door that the player is still colliding with
}

[System.Serializable]
public class PlayerJoinsGame : UnityEvent<PlayerId, bool> {
	//arg1: The PlayerId of the player that joined the game
	//arg2: True if the game is full
}

[System.Serializable]
public class PlayerLeavesGame : UnityEvent<PlayerId> {
	//arg1: The PlayerId of the player that left the game
}

[System.Serializable]
public class GameStateChanges : UnityEvent {
}

public class HouseIntegrityChanges : UnityEvent {

}

public class VictimSaved : UnityEvent {
	public int numVictims;
	public int savedVictims;
}

public class HouseAppears : UnityEvent {

}

public class EventsManager : MonoBehaviour {

	public PlayerCollidesWithDoor playerCollidesWithDoor;
	public PlayerUncollidesWithDoor playerUncollidesWithDoor;
	public PlayerJoinsGame playerJoinsGame;
	public PlayerLeavesGame playerLeavesGame;
	public GameStateChanges gameStateChanges;
	public HouseIntegrityChanges houseIntegrityChanges;
	public VictimSaved victimSaved;
	public HouseAppears houseAppears;

	public static EventsManager Instance {get; private set;}

	private void Awake() {
		if (Instance == null) {
			Instance = this;
		} else if (Instance != this) {
			Destroy(gameObject);
		}
		DontDestroyOnLoad(gameObject);

		if (playerCollidesWithDoor == null) {
			playerCollidesWithDoor = new PlayerCollidesWithDoor();
		}
		if (playerUncollidesWithDoor == null) {
			playerUncollidesWithDoor = new PlayerUncollidesWithDoor();
		}
		if (playerJoinsGame == null) {
			playerJoinsGame = new PlayerJoinsGame();
		}
		if (playerLeavesGame == null) {
			playerLeavesGame = new PlayerLeavesGame();
		}
		if (gameStateChanges == null) {
			gameStateChanges = new GameStateChanges();
		}
		if (houseIntegrityChanges == null) {
			houseIntegrityChanges = new HouseIntegrityChanges();
		}
		if (victimSaved == null) {
			victimSaved = new VictimSaved();
		}
		if (houseAppears == null) {
			houseAppears = new HouseAppears();
		}
	}
}
