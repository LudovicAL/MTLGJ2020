using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Player))]
public class PlayerHealth : MonoBehaviour {

	[HideInInspector] public Player player;
	public PlayerTakingDamage playerTakingDamage = new PlayerTakingDamage();
	public PlayerDying playerDying = new PlayerDying();

	[System.Serializable]
	public class PlayerTakingDamage : UnityEvent<PlayerId, float> {}

	[System.Serializable]
	public class PlayerDying : UnityEvent<PlayerId> {}

	// Use this for initialization
	void Start () {
		player = GetComponent<Player>();
		player.playerId.currentHealth = player.playerId.maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//Call this function when dealing damage to this player
	public void TakeDamage(int amount) {
		if (player.playerId.currentHealth > 0) {
			player.playerId.currentHealth -= amount;
			playerTakingDamage.Invoke(player.playerId, (float)player.playerId.currentHealth / (float)player.playerId.maxHealth);
			if (player.playerId.currentHealth <= 0) {
				player.playerId.currentHealth = 0;
				playerDying.Invoke(player.playerId);
			}
		}
	}
}
