using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerHealthBar : MonoBehaviour {

	[HideInInspector] public Player player;

	// Use this for initialization
	void Start () {
		player = GetComponent<Player>();
		player.playerHealth.playerTakingDamage.AddListener(OnTakingDamage);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnTakingDamage(PlayerId playerId, float healthRatio) {
		playerId.greenHealthBar.fillAmount = Mathf.Clamp(healthRatio, 0.0f, 1.0f);
		Canvas.ForceUpdateCanvases();
	}
}
