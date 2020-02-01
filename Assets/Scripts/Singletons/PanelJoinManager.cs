using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelJoinManager : MonoBehaviour {

	public GameObject panelPlayerJoinsGamePrefab;
	public static PanelJoinManager Instance {get; private set;}
	public Transform panelPlayerJoinTransform;

	private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else if (Instance != this) {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start () {
		EventsManager.Instance.playerJoinsGame.AddListener(OnPlayerJoinsGame);
		EventsManager.Instance.playerLeavesGame.AddListener(OnPlayerLeavesGame);
    }

    private void OnPlayerJoinsGame(PlayerId playerId, bool gameFull) {
        playerId.panelJoin = Instantiate(panelPlayerJoinsGamePrefab, panelPlayerJoinTransform);
        playerId.panelJoin.GetComponent<RectTransform>().localScale = Vector3.one;
        playerId.panelJoin.transform.Find("Text").GetComponent<Text>().text = playerId.playerName;
		Canvas.ForceUpdateCanvases();
	}

    private void OnPlayerLeavesGame(PlayerId playerId) {
        if (playerId.panelJoin != null) {
            Destroy(playerId.panelJoin);
        }
		Canvas.ForceUpdateCanvases();
	}
}
