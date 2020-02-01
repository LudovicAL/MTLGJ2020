using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerControler))]
[RequireComponent(typeof(Player))]
public class Player : MonoBehaviour {
	public PlayerId playerId;
	[HideInInspector] public PlayerControler playerControler;

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	}
}
