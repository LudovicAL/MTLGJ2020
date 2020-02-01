using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerActions))]
[RequireComponent(typeof(PlayerHealth))]
[RequireComponent(typeof(PlayerHealthBar))]
public class Player : MonoBehaviour {
	public PlayerId playerId;
	[HideInInspector] public PlayerActions playerActions;
	[HideInInspector] public PlayerHealth playerHealth;
	[HideInInspector] public PlayerHealthBar playerHealthBar;

    // Use this for initialization
    void Start () {
		playerActions = GetComponent<PlayerActions>();
		playerHealth = GetComponent<PlayerHealth>();
		playerHealthBar = GetComponent<PlayerHealthBar>();
	}
	
	// Update is called once per frame
	void Update () {
	}
}
