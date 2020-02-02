using UnityEngine;

public class FlammableObject : MonoBehaviour
{
    public FlammableObjectData _flammableObjectData;
    public bool _isOnFire;
    public float _fireDamage = 0.01f;
    public float _currentHitPoints;
    public float _maxHitPoints = 0;

    void Start()
    {
        gameObject.tag = "Flammable";
        _maxHitPoints = _flammableObjectData.hitPoints + UnityEngine.Random.Range(
            _maxHitPoints - _flammableObjectData.hitPointsVariation,
            _maxHitPoints + _flammableObjectData.hitPointsVariation
        );
        _currentHitPoints = _maxHitPoints;
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentHitPoints <= 0)
        {
            if (_flammableObjectData.isExplodingOnDeath){
                FireManager.Instance.SpawnExplosion(gameObject, _flammableObjectData.explosionRadius);
            } else if (_isOnFire) {
                FireManager.Instance.SpawnFire(gameObject, _flammableObjectData.fireRadius);
            }
            if (gameObject.layer == 13) //Magic number 10 is wall
            {
                EventsManager.Instance.houseIntegrityChanges.Invoke();
            }
            SpawnReplacement(gameObject);
            FireManager.Instance._flammableObjects.Remove(gameObject);
            Destroy(gameObject);
        } else if (_currentHitPoints < _maxHitPoints) {
            float burntRatio = Mathf.Max(_currentHitPoints / _maxHitPoints, 0.55f);
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, burntRatio, burntRatio);
        }
    }

    void SpawnReplacement(GameObject source)
    {
        GameObject[] target;
        Debug.Log(source.layer);
        switch (source.layer)
        {
            case 9: // Magic number for layer ground
                target =  FireManager.Instance._replacementFloorTile;
                break;
            default:
                target = null;
                break;
        }
        FireManager.Instance.SpawnDestroyedTile(source, target);
    }
}
