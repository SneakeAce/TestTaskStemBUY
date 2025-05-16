using UnityEngine;

[CreateAssetMenu(menuName = "Configs/FigureConfig")]
public class FigureConfig : ScriptableObject
{
    [field: SerializeField] public GameObject Prefab { get; private set; }
    [field: SerializeField] public AnimalType AnimalType { get; private set; }
    [field: SerializeField] public ShapeType ShapeType { get; private set; }
    [field: SerializeField] public Color Color { get; private set; }
} 
