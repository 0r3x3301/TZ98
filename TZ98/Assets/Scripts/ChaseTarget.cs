using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(TargetDetector))]
public class ChaseTarget : MonoBehaviour
{
    [SerializeField] private float _speed, _maxDistance, _mindistance;
    [SerializeField] private TargetDetector _detector;
    private Transform _targetTransform = null;

    private void OnValidate()
    {
        _detector = GetComponent<TargetDetector>();
    }

    private void OnEnable()
    {
        _detector.OnTargetEnter += SetTarget;
    }
    private void OnDisable()
    {
        _detector.OnTargetEnter -= SetTarget;
    }

    private void SetTarget(Transform targetTransform)
    {
        _targetTransform = targetTransform;
    }

    private void Update()
    {
        if (_targetTransform != null)
        {
            MoveTo(_targetTransform);
        }
    }

    private void MoveTo(Transform targetTransform)
    {
        var direction = new Vector3(targetTransform.position.x, 0f, targetTransform.position.z);
        direction.Normalize();
        var distance = Vector3.Distance(targetTransform.position, transform.position);
        if (distance < _mindistance - 0.5f)
        {
            transform.Translate(direction * -_speed * Time.deltaTime);
        }
        else if (distance > _maxDistance + 0.5f)
        {
            transform.Translate(direction * _speed * Time.deltaTime);
        }
        transform.LookAt(new Vector3(targetTransform.position.x, transform.position.y, targetTransform.position.z));
    }
}
