using UnityEngine;
[RequireComponent(typeof(TargetDetector))]
public class AttackTarget : MonoBehaviour
{
    [SerializeField] private float _cooldown;
    [SerializeField] private GameObject _attackPrefab;
    [SerializeField] private Transform _attackPointTransform;
    [SerializeField] private TargetDetector _detector;
    private Transform _targetTransform = null;
    private float _currentCooldownTime = 0;

    public bool IsClone = false;

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
        _detector.OnTargetEnter -= SetTarget;
    }

    private void Update()
    {
        if (_targetTransform != null)
        {
            WaitAttack();
        }
    }

    private void WaitAttack()
    {
        if (_currentCooldownTime >= _cooldown)
        {
            Attack();
            _currentCooldownTime = 0;
        }
        else
        {
            _currentCooldownTime += Time.deltaTime;
        }
    }

    public void Attack()
    {
        var newAttack = Instantiate(_attackPrefab, _attackPointTransform.position, _attackPointTransform.rotation);
        newAttack.transform.LookAt(_targetTransform);
        newAttack.transform.rotation = new Quaternion(0f, newAttack.transform.rotation.y, newAttack.transform.rotation.z, newAttack.transform.rotation.w);
        if (IsClone)
        {
            var damageGiver = newAttack.GetComponent<DamageGiver>();
            damageGiver.SetDamage(damageGiver.Damage / 2);
        }
    }
}
