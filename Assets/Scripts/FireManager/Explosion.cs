using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float _fireDamage = 10.0f;
    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Flammable") {
            FlammableObject flammableObject = collision.gameObject.GetComponent<FlammableObject>();
            
            Vector3 source = gameObject.transform.position;
            Vector3 destination = collision.transform.position;
            Vector3 direction = destination - source;

            RaycastHit2D hit = Physics2D.Raycast(source, direction);
            if (hit.collider != null){
                FlammableObject hitFlammableObject = hit.transform.gameObject.GetComponent<FlammableObject>();
                if (hitFlammableObject != null)
                {
                    if (hit.transform == collision.transform || !hitFlammableObject._flammableObjectData.isBlockingPropagation)
                    {
                        flammableObject._fireDamage = _fireDamage;
                        flammableObject._isOnFire = true;
                    }
                }

            }
        }
    }
}
