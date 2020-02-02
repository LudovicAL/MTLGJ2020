using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Flammable Object Data", menuName = "Data")]
public class FlammableObjectData : ScriptableObject
{
    public float hitPoints;
    public float hitPointsVariation;
    public float regeneration;
    public float explosionRadius;
    public bool isBreakable;
    public bool isExplodingOnDeath;
    public bool isBlockingPropagation;
    public float damageOnDeath;
    public float fireRadius;
}

[CustomEditor(typeof(FlammableObjectData))]
public class FlammableObjectDataEditor: Editor
{
    public override void OnInspectorGUI()
    {
        var flammableObjectData = target as FlammableObjectData;
        flammableObjectData.hitPoints = EditorGUILayout.FloatField("Object hit points", flammableObjectData.hitPoints);
        flammableObjectData.hitPointsVariation = EditorGUILayout.FloatField("Hit points variation", flammableObjectData.hitPointsVariation);
        flammableObjectData.regeneration = EditorGUILayout.FloatField("Object regeneration (if any)", flammableObjectData.regeneration);
        flammableObjectData.fireRadius = EditorGUILayout.FloatField("Fire radius", flammableObjectData.fireRadius);
        flammableObjectData.isBreakable = EditorGUILayout.Toggle("Is breakable?", flammableObjectData.isBreakable);
        flammableObjectData.isBlockingPropagation = EditorGUILayout.Toggle("Block propagation?", flammableObjectData.isBlockingPropagation);
        flammableObjectData.isExplodingOnDeath = EditorGUILayout.Toggle("Explode on death?", flammableObjectData.isExplodingOnDeath);

        if (flammableObjectData.isExplodingOnDeath) {
            flammableObjectData.damageOnDeath = EditorGUILayout.FloatField("Explosion damage", flammableObjectData.damageOnDeath);
            flammableObjectData.explosionRadius = EditorGUILayout.FloatField("Explosion radius", flammableObjectData.explosionRadius);
        }
            

    }
}
