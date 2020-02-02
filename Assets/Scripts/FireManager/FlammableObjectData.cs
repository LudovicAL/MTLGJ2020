using UnityEngine;
using UnityEditor;
using System;

[CreateAssetMenu(fileName = "Flammable Object Data", menuName = "Data")][Serializable]
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

