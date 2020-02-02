using UnityEngine;

public class Fire : MonoBehaviour
{
    void OnBecameInvisible(){
        GetComponent<ParticleSystem>().Stop();
    }

    void OnBecameVisible(){
        GetComponent<ParticleSystem>().Play();
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag == "Flammable") {
            FlammableObject flammableObject = collider.GetComponent<FlammableObject>();
            
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
                        flammableObject._isOnFire = true;
                    }
                }

            }
        }
    }

    void OnTriggerExit2D(Collider2D collider) {
        if (collider.tag == "Flammable") {
            FlammableObject flammableObject = collider.GetComponent<FlammableObject>();
            flammableObject._isOnFire = false;
        }
    }
}
