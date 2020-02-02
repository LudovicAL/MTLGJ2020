using Unity.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireManager : MonoBehaviour
{

    public static FireManager Instance { get; private set; }
    public GameObject _fire;
    public GameObject _explosion;
    public GameObject[] _replacementFloorTile;
    public List<GameObject> _flammableObjects;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
	void Start() {

		EventsManager.Instance.houseAppears.AddListener(SetFlammables);
	}

    void Update()
    {
        PropagateFire();
    }

    public void SpawnDestroyedTile(GameObject source, GameObject[] target) {
        if (target != null) {
            int index = Random.Range (0, target.Length);
            Instantiate(target[index], source.transform.position, Quaternion.identity);
        }
    }

    public void SpawnFire(GameObject source, float radius)
    {
        var shape = _fire.GetComponent<ParticleSystem>().shape;
        shape.scale = new Vector3(radius, radius, 1.0f);
        Vector3 position = source.transform.position;
        position.z = Random.Range(-5.0f, -0.0f);
        Instantiate(_fire, position, Quaternion.Euler(new Vector3(0, 180, 0)));
    }
    
    public void SpawnExplosion(GameObject source, float explosionRadius)
    {
        _explosion.GetComponent<CircleCollider2D>().radius = explosionRadius;
        _explosion.GetComponent<Explosion>()._fireDamage = source.GetComponent<FlammableObject>()._flammableObjectData.damageOnDeath;
        _explosion.transform.localScale = new Vector3(explosionRadius, explosionRadius, 1);
        Instantiate(_explosion, source.transform.position, Quaternion.identity);
    }

    public void PropagateFire()
    {
        foreach (GameObject flammableObject in _flammableObjects)
        {
            FlammableObject flammableEntity = flammableObject.GetComponent<FlammableObject>();
            if (flammableEntity._isOnFire)
            {
                flammableEntity._currentHitPoints -= flammableEntity._fireDamage;
            } else if (flammableEntity._currentHitPoints < flammableEntity._maxHitPoints) {
                flammableEntity._currentHitPoints += flammableEntity._flammableObjectData.regeneration;
            }
            
        }
    }

    public void SetFlammables() {
        _flammableObjects = new List<GameObject>();
        _flammableObjects.AddRange(GameObject.FindGameObjectsWithTag("Flammable"));
    }
}
