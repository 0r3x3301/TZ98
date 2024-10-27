using System;
using System.Linq;
using UnityEngine;

public class DamageTaker : MonoBehaviour
{
    [SerializeField] private float _health;
    [SerializeField] private float _damageTakeCooldown;
    [SerializeField] private string[] _damageGiversTag;
    private float _currentTimer = 0; 

    public event Action<GameObject> OnDead;
    public void TakeDamage(float damage)
    {
        if (_currentTimer >= _damageTakeCooldown)
        {
            _health -= damage;
            Debug.Log(damage);
            if (_health <= 0)
            {
                Death();
            }
            _currentTimer = 0;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_damageGiversTag.Contains(other.tag))
        {
            var damageGiver = other.GetComponent<DamageGiver>();
            if (damageGiver != null)
            {
                TakeDamage(damageGiver.GiveDamage());
            }
        }
    }

    private void Update()
    {
        if (_currentTimer < _damageTakeCooldown) _currentTimer += Time.deltaTime;
    }

    private void Death()
    {
        Debug.Log("Buuueee");
        OnDead?.Invoke(gameObject);
    }
}
