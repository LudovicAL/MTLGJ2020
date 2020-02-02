using UnityEngine;

public class Fire : MonoBehaviour
{
    public float _hitPoints = 10.0f;
    public float _hitPointsVariation = 10.0f;

    void awake() {
        this._hitPoints = Random.Range(
            _hitPoints - _hitPointsVariation,
            -_hitPoints + _hitPointsVariation
        );
    }

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

            RaycastHit2D hit = Physics2D.Raycast(source, direction, 1.0f, 1 << 13);
            if (hit.collider != null){
                FlammableObject hitFlammableObject = hit.transform.gameObject.GetComponent<FlammableObject>();
                if (hitFlammableObject != null)
                {
                    if (hit.transform == collider.transform || !hitFlammableObject._flammableObjectData.isBlockingPropagation)
                    {
                        flammableObject._isOnFire = true;
                    }
                }

            } else {
                flammableObject._isOnFire = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collider) {
        if (collider.tag == "Flammable") {
            FlammableObject flammableObject = collider.GetComponent<FlammableObject>();
            flammableObject._isOnFire = false;
        }
    }

    void OnParticleCollision(GameObject collision) {
        Debug.Log(collision);
        if (collision.tag == "FireStarter") {
            this._hitPoints -= 10.0f;
        }
        if (this._hitPoints <= 0.0f){
            Destroy(gameObject);
        }
    }
}
