using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputsMap : MonoBehaviour {

	public string interractWithDoor = "Fire2";
	public string useBow = "Fire1";

	public static InputsMap Instance { get; private set; }

	private void Awake() {
		if (Instance == null) {
			Instance = this;
		} else if (Instance != this) {
			Destroy(gameObject);
		}
		DontDestroyOnLoad(gameObject);
	}

	void Start() {
        
    }

    void Update() {
        
    }
}
