using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _lifeTime, _speed;
    private float _currentLifeTime = 0;
    void Update()
    {
        var direction = new Vector3(0f, 0f, 1f);
        transform.Translate(direction * _speed * Time.deltaTime);
        CheckLife();
    }

    private void CheckLife()
    {
        if (_currentLifeTime >= _lifeTime)
        {
            Destroy(gameObject);
        }
        else
        {
            _currentLifeTime += Time.deltaTime;
        }
    }
}
