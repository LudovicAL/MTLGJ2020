using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanelJoinManager : MonoBehaviour {

	public GameObject panelPlayerJoinedPrefab;
	public static PlanelJoinManager Instance {get; private set;}
	public GameObject panelJoinInstruction;
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
		EventsManager.Instance.playerJoinsGame.AddListener(OnPlayerJoining);
		EventsManager.Instance.playerLeavesGame.AddListener(OnPlayerLeaving);
    }

    private void OnPlayerJoining(PlayerId playerId, bool gameFull) {
        playerId.panelJoin = Instantiate(panelPlayerJoinedPrefab, panelPlayerJoinTransform);
        playerId.panelJoin.GetComponent<RectTransform>().localScale = Vector3.one;
        playerId.panelJoin.transform.Find("Text").GetComponent<Text>().text = playerId.playerName + " joined the game!";
        if (gameFull) {
            panelJoinInstruction.SetActive(false);
        }
		Canvas.ForceUpdateCanvases();
	}

    private void OnPlayerLeaving(PlayerId playerId) {
        if (playerId.panelJoin != null) {
            Destroy(playerId.panelJoin);
        }
        panelJoinInstruction.SetActive(true);
		Canvas.ForceUpdateCanvases();
	}
}
