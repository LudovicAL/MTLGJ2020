using UnityEngine;

public class FlammableObject : MonoBehaviour
{
    public FlammableObjectData _flammableObjectData;
    public bool _isOnFire;
    public float _fireDamage = 0.01f;
    public float _currentHitPoints;
    public float _maxHitPoints;

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
            if (gameObject.layer == 13)
                EventsManager.Instance.houseIntegrityChanges.Invoke();
        } else if (_currentHitPoints < _maxHitPoints) {
            float burntRatio = Mathf.Max(_currentHitPoints / _maxHitPoints, 0.55f);
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, burntRatio, burntRatio);
        }
    }
}
