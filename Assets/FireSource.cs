using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSource : MonoBehaviour
{

    void OnParticleCollision(GameObject collision) {
        if (collision.tag == "Water") {
            gameObject.GetComponentInParent<Fire>()._hitPoints -= 0.035f;
        }
        if (gameObject.GetComponentInParent<Fire>()._hitPoints <= 0.0f){
            Destroy(gameObject);
        }
    }

    void OnDestroy() {
        Destroy(transform.parent.gameObject);
    }
}
