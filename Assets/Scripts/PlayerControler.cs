using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Player))]
public class PlayerControler : MonoBehaviour {

	public GameObject crosshair;
	public float crosshairDistance;
	public float arrowSpeed;
	public float movementSpeedMultiplicator;
	public float aimingPenaltyToMovementSpeed;
	public float minFootstepsVolume = 1;
	public float maxFootstepsVolume = 1;
	public float minFootstepsPitch = 1;
	public float maxFootstepsPitch = 1;
	public GameObject arrowPrefab;
	public AudioClip footStepDefaultAudioClip;
	public AudioClip footStepWoodAudioClip;
	public AudioClip bowAudioClip;

	private Player player;
	private AudioSource audioSource;
	private Rigidbody2D rb;
	private PlayerCollisionsManager playerCollisionsManager;
	private Vector2 movementDirection;
	private Vector2 aimDirection = Vector2.down;
	private float movementSpeed;
	private bool isAiming;
	private bool endOfAiming;
	private bool isInterractingWithDoors;
	private bool isGrabbing;
	private bool audioSourceIsPaused;
	private AudioClip currentFootStepAudioClip;
	
	private Grabbable grabbedObject;

	void Awake() {
		player = GetComponent<Player>();
		rb = GetComponent<Rigidbody2D>();
		playerCollisionsManager = GetComponent<PlayerCollisionsManager>();
		currentFootStepAudioClip = footStepDefaultAudioClip;
	}

	void Start() {

	}

	void Update() {
		if (GameStatesManager.Instance.gameState == GameStatesManager.AvailableGameStates.Playing) {
			ProcessInputs();
			Move();
			PlayFootsteps();
			Aim();
			ShootArrow();
			InterractWithDoor();
			TryGrab();
		}
	}

	/// <summary>
	/// Collects inputs given by the player
	/// </summary>
	private void ProcessInputs() {
		movementDirection = new Vector2(player.playerId.controls.GetLHorizontal(), player.playerId.controls.GetLVertical());
		movementSpeed = Mathf.Clamp(movementDirection.magnitude, 0.0f, 1.0f);
		movementDirection.Normalize();
		isAiming = player.playerId.controls.GetButtonA();
		endOfAiming = player.playerId.controls.GetButtonAUp();
		isGrabbing = player.playerId.controls.GetButtonXDown();

		if (isAiming) {
			movementSpeed *= aimingPenaltyToMovementSpeed;
		}
		isInterractingWithDoors = player.playerId.controls.GetButtonBDown();
	}

	/// <summary>
	/// Moves the character around
	/// </summary>
	private void Move() {
		rb.velocity = movementDirection * movementSpeed * movementSpeedMultiplicator;
	}

	/// <summary>
	/// Plays footsteps sounds
	/// </summary>
	private void PlayFootsteps() {
		if (rb.velocity != Vector2.zero) {	//If the character is moving
			if (!audioSource || !audioSource.isPlaying) { //If footsteps sounds are not playing already
				if (isAiming) {
					audioSource = AudioManager.Instance.PlayClip(currentFootStepAudioClip, false, 0.0f, Random.Range(minFootstepsVolume * aimingPenaltyToMovementSpeed, maxFootstepsVolume * aimingPenaltyToMovementSpeed), Random.Range(minFootstepsPitch * aimingPenaltyToMovementSpeed, maxFootstepsPitch * aimingPenaltyToMovementSpeed));
				} else {
					audioSource = AudioManager.Instance.PlayClip(currentFootStepAudioClip, false, 0.0f, Random.Range(minFootstepsVolume, maxFootstepsVolume), Random.Range(minFootstepsPitch, maxFootstepsPitch));
				}
			}
		} else {
			if (audioSource) {	//If the character is not moving
				AudioManager.Instance.StopAudioSource(audioSource);
			}
		}
	}

	/// <summary>
	/// Finds out the right footsteps audioclip to play
	/// </summary>
	private void SetFootstepsAudioClip() {
		//Changer le currentFootStepAudioClip en fonction du type de sol sur lequel le joueur se déplace
	}

	/// <summary>
	/// Aims the bow
	/// </summary>
	private void Aim() {
		if (crosshair.activeSelf != isAiming) {
			crosshair.SetActive(isAiming);
		}
		if (movementDirection != Vector2.zero) {
			crosshair.transform.localPosition = movementDirection * crosshairDistance;
			aimDirection = crosshair.transform.localPosition;
			aimDirection.Normalize();
		}
	}

	/// <summary>
	/// Shoots an arrow
	/// </summary>
	private void ShootArrow() {	
		if (endOfAiming) {
			//AudioManager.Instance.PlayClipOneShot(bowAudioClip);
			//GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
		}
	}

	/// <summary>
	/// Interracts with every colliding doors
	/// </summary>
	private void InterractWithDoor() {
		if (isInterractingWithDoors) {
			//for (int i = (playerCollisionsManager.currentCollidingDoors.Count - 1); i >= 0; i--) {
			//	playerCollisionsManager.currentCollidingDoors[i].Interract();
			//}
		}
	}

	private void TryGrab() {
		if (isGrabbing)
		{
			// pickup
			if (grabbedObject == null)
			{
				Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 1f, 1 << 12); // 12 = Grabbable
				foreach (Collider2D hit in hits)
				{
					Grabbable grabComponent = hit.gameObject.GetComponent<Grabbable>();
					if (grabComponent.owner == null)
					{
						grabComponent.owner = this;
						grabbedObject = grabComponent;
						break;
					}
				}
			}
			else // drop
			{
				grabbedObject.owner = null;
				grabbedObject = null;
			}
		}
	}

	/// <summary>
	/// Sets the character on fire
	/// </summary>
	public void SetOnFire(bool active) {
		
	}
}
