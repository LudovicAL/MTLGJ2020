using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class BackgroundScroller : MonoBehaviour {
	public float horizontalScrollSpeed;
	public float verticalScrollSpeed;

	private MeshRenderer meshRenderer;

	// Start is called before the first frame update
	void Start() {
		if (Camera.main.orthographic) {
			meshRenderer = GetComponent<MeshRenderer>();
			transform.localScale = Vector3.zero;
			while (Camera.main.WorldToScreenPoint(new Vector3(0f, transform.position.y + meshRenderer.bounds.extents.y)).y < Screen.height) {
				transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y + 0.1f, transform.localScale.z);
			}
			transform.localScale = new Vector3(Camera.main.aspect * transform.localScale.y, transform.localScale.y, transform.localScale.z);
		}
	}

    // Update is called once per frame
    void Update() {
		meshRenderer.material.mainTextureOffset = new Vector2(Time.time * horizontalScrollSpeed, Time.time * verticalScrollSpeed);
    }
}
