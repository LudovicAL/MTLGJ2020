using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DoorData : MonoBehaviour {

	public AudioClip doorOpeningAudioClip;
	public AudioClip doorClosingAudioClip;
	private bool isOpen;
	public GameObject openDoorParent;
	public GameObject closedDoorParent;

    void Start() {
        
    }

    void Update() {
        
    }

	public void Interract() {
		isOpen = !isOpen;
		openDoorParent.SetActive(isOpen);
		closedDoorParent.SetActive(!isOpen);
		if (isOpen) {
			AudioManager.Instance.PlayClipOneShot(doorOpeningAudioClip);
		} else {
			AudioManager.Instance.PlayClipOneShot(doorClosingAudioClip);
		}
	}
}
