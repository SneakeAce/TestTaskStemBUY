using UnityEngine;

public interface IFactory<T, TConfig> 
    where T : MonoBehaviour
    where TConfig : ScriptableObject
{
    T Create(TConfig config, Vector3 spawnPosition);
}
