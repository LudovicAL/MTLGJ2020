using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControler : MonoBehaviour {
	public Vector3 cameraOffset = new Vector3(0.0f, 0.0f, -20.0f);
	public float smoothTime = 0.5f;
	public float minZoom = 5.0f;
	public float maxZoom = 20.0f;
	public float zoomLimiter = 40.0f;

	private Vector3 velocity;
	private Camera cam;
	private Bounds bounds;

    void Start() {
		cam = GetComponentInChildren<Camera>();
    }

	void LateUpdate() {
		if (PlayerListManager.Instance.currentPlayerCount > 0) {
			ComputeBounds();
			MoveCamera(bounds.center);
			ZoomCamera(Mathf.Max(bounds.size.x, bounds.size.y));
		} else {
#if (UNITY_EDITOR)
			if (Input.anyKey) {
				Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y, 0.0f);
				if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) {
					targetPosition.y = targetPosition.y + 1.0f;
				}
				if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) {
					targetPosition.y = targetPosition.y - 1.0f;
				}
				if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {
					targetPosition.x = targetPosition.x + 1.0f;
				}
				if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {
					targetPosition.x = targetPosition.x - 1.0f;
				}
				MoveCamera(targetPosition);
			}
#endif
		}
	}

	// Returns bounds encapsulating all player avatar gameobjects
	private void ComputeBounds() {
		if (PlayerListManager.Instance.currentPlayerCount > 0) {
			bounds = new Bounds(PlayerListManager.Instance.listOfPlayers[0].avatar.transform.position, Vector3.zero);
			for (int i = 1; i < PlayerListManager.Instance.currentPlayerCount; i++) {
				bounds.Encapsulate(PlayerListManager.Instance.listOfPlayers[i].avatar.transform.position);
			}
		}
	}

	//Moves the camera
	private void MoveCamera(Vector3 targetPosition) {
		transform.position = Vector3.SmoothDamp(transform.position, targetPosition + cameraOffset, ref velocity, smoothTime);
	}

	//Zooms the camera
	private void ZoomCamera(float width) {
		float newZoom = Mathf.Lerp(minZoom, maxZoom, width / zoomLimiter);
		cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, newZoom, Time.deltaTime);
	}
}
