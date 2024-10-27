using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CloneSkill : MonoBehaviour
{
    [SerializeField] private float _cooldown;
    [SerializeField] private TargetDetector _detector;
    [SerializeField]private AttackTarget _attackTarget;
    [SerializeField] private GameObject _origin;
    private List<GameObject> _clones = new List<GameObject>();
    private Transform _targetTransform = null;
    private float _currentCooldownTime;

    private bool _isClone = false;

    private void OnValidate()
    {
        _detector = GetComponent<TargetDetector>();
        _attackTarget = GetComponent<AttackTarget>();
    }

    private void OnEnable()
    {
        _currentCooldownTime = _cooldown - 2;
        _detector.OnTargetEnter += SetTarget;
        
    }

    private void Start()
    {
        if (gameObject.name == "Clone")
        {
            Debug.Log("I Clone");
            _isClone = true;
            if (_attackTarget != null) _attackTarget.IsClone = true;
        }
    }
    private void OnDisable()
    {
        _detector.OnTargetEnter -= SetTarget;
    }

    private void SetTarget(Transform targetTransform)
    {
        _targetTransform = targetTransform;
        _detector.OnTargetEnter -= SetTarget;
    }

    private void Update()
    {
        if (!_isClone && _clones.Count == 0 && _targetTransform != null)
        {
            WaitClone();
        }
    }

    private void WaitClone()
    {
        if (_currentCooldownTime >= _cooldown)
        {
            Clone();
            _currentCooldownTime = 0;
        }
        else
        {
            _currentCooldownTime += Time.deltaTime;
        }
    }

    private void OnCloneDead(GameObject clone)
    {
        _clones.Remove(clone);
        clone.GetComponent<DamageTaker>().OnDead -= OnCloneDead;
        Destroy(clone.gameObject);
    }

    private void Clone()
    {
        int[] deviations = { -1, 1 };
        for (int i = 0; i < 2; i++)
        {
            var position = new Vector3(transform.position.x + deviations[i], transform.position.y, transform.position.z);
            _clones.Add(Instantiate(_origin, position, Quaternion.identity));
            _clones.Last().name = "Clone";
            _clones.Last().GetComponent<DamageTaker>().OnDead += OnCloneDead;
        }
    }
}
