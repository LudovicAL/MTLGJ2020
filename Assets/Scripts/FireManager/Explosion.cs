using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float _fireDamage = 10.0f;
    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.tag == "Flammable") {
            FlammableObject flammableObject = collider.gameObject.GetComponent<FlammableObject>();
            
            Vector3 source = gameObject.transform.position;
            Vector3 destination = collider.transform.position;
            Vector3 direction = destination - source;

            RaycastHit2D hit = Physics2D.Raycast(source, direction);
            if (hit.collider != null){
                FlammableObject hitFlammableObject = hit.transform.gameObject.GetComponent<FlammableObject>();
                if (hitFlammableObject != null)
                {
                    if (hit.transform == collider.transform || !hitFlammableObject._flammableObjectData.isBlockingPropagation)
                    {
                        flammableObject._currentHitPoints -= _fireDamage;
                    }
                }

            }
        }
    }
}
