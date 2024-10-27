using System.Collections.Generic;
using UnityEngine;

public class DevilSpawner : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private GameObject _prefab;

    private List<GameObject> _spawnedDevils = new List<GameObject>();
    public void Spawn()
    {
        _spawnedDevils.Add(Instantiate(_prefab));
    }
}
