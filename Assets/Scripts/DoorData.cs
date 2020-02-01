using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DoorData : MonoBehaviour {

	public bool isLocked;
	private bool isOpen;
	public GameObject openDoorParent;
	public GameObject closedDoorParent;	

    void Start() {
        
    }

    void Update() {
        
    }

	public void Interract() {
		if (!isLocked) {
			isOpen = !isOpen;
			openDoorParent.SetActive(isOpen);
			closedDoorParent.SetActive(!isOpen);
			if (isOpen) {
				AudioManager.Instance.PlayClipOneShot(AudioManager.Instance.doorClosingClip);
			} else {
				AudioManager.Instance.PlayClipOneShot(AudioManager.Instance.doorOpeningClip);
			}
		}
	}
}
