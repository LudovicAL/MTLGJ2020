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
            gameObject.GetComponentInParent<Fire>()._hitPoints -= 0.035f;
        }
        if (gameObject.GetComponentInParent<Fire>()._hitPoints <= 0.0f){
            Destroy(gameObject);
        } else {
            float extinguishedRatio = gameObject.GetComponentInParent<Fire>()._hitPoints / initialHitPoints;
            transform.parent.gameObject.transform.localScale = new Vector3(
                initialScale.x * extinguishedRatio,
                initialScale.y * extinguishedRatio,
                initialScale.z
            );
        }
    }

    void OnDestroy() {
        Destroy(transform.parent.gameObject);
    }
}
