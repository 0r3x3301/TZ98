using System;
using UnityEngine;

public class TargetDetector : MonoBehaviour
{
    [SerializeField] private string _tag = "";
    public event Action<Transform> OnTargetEnter;

    public void Init(string tag)
    {
        _tag = tag;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_tag))
        {
            OnTargetEnter?.Invoke(other.transform);
        }
    }
}
