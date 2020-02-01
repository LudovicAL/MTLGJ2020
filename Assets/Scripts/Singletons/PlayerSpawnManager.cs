using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnManager : MonoBehaviour {

	public GameObject avatarPrefab;
	public static PlayerSpawnManager Instance {get; private set;}

	private void Awake() {
		if (Instance == null) {
			Instance = this;
		} else if (Instance != this) {
			Destroy(gameObject);
		}
		DontDestroyOnLoad(gameObject);
	}

	// Use this for initialization
	void Start () {
		EventsManager.Instance.playerJoinsGame.AddListener(OnPlayerJoinsGame);
		EventsManager.Instance.playerLeavesGame.AddListener(OnPlayerLeavesGame);
	}

	private void OnPlayerJoinsGame(PlayerId playerId, bool gameFull) {
		playerId.avatar = Instantiate(avatarPrefab, playerId.spawnPosition, Quaternion.identity);
		playerId.avatar.GetComponent<Player>().playerId = playerId;
	}

	private void OnPlayerLeavesGame(PlayerId playerId) {
		if (playerId.avatar != null) {
			Destroy(playerId.avatar);
		}
	}
}
