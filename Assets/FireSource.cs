using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSource : MonoBehaviour
{
    private Vector3 initialScale;
    private float initialHitPoints;
    void Start() {
        initialScale = transform.parent.gameObject.transform.localScale;
        initialHitPoints = gameObject.GetComponentInParent<Fire>()._hitPoints;
    }
    void OnParticleCollision(GameObject collision) {
        if (collision.tag == "Water") {
            gameObject.GetComponentInParent<Fire>()._hitPoints -= Random.Range(0.035f, 0.1f);
        }
        if (gameObject.GetComponentInParent<Fire>()._hitPoints <= 0.0f){
            Destroy(gameObject);
        } else {
            float extinguishedRatio = gameObject.GetComponentInParent<Fire>()._hitPoints / initialHitPoints;
            transform.parent.gameObject.transform.localScale = new Vector3(
                Mathf.Max(initialScale.x * extinguishedRatio, initialScale.x / 2),
                Mathf.Max(initialScale.y * extinguishedRatio, initialScale.y / 2),
                initialScale.z
            );
        }
    }

    void OnDestroy() {
        Destroy(transform.parent.gameObject);
    }
}
