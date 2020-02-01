using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarsManager : MonoBehaviour {

	public GameObject panelHealthBarPrefab;
	public Transform panelHealthBarsTransform;
	public static HealthBarsManager Instance {get; private set;}
	

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
		PlayerListManager.Instance.playerJoining.AddListener(OnPlayerJoining);
		PlayerListManager.Instance.playerLeaving.AddListener(OnPlayerLeaving);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnPlayerJoining(PlayerId playerId, bool gameFull) {
		playerId.panelHealthBar = Instantiate(panelHealthBarPrefab, panelHealthBarsTransform);
		playerId.panelHealthBar.GetComponent<RectTransform>().localScale = Vector3.one;
		playerId.greenHealthBar = playerId.panelHealthBar.transform.Find("Panel Green").gameObject.GetComponent<Image>();
		Canvas.ForceUpdateCanvases();
	}

	private void OnPlayerLeaving(PlayerId playerId) {
		if (playerId.panelHealthBar != null) {
			Destroy(playerId.panelHealthBar);
			Canvas.ForceUpdateCanvases();
		}
	}
}
