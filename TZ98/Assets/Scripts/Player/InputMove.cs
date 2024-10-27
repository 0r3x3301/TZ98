using UnityEngine;

public class InputMove : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;

    private void Update()
    {
        Move();
    }

    public void Move()
    {
        var direction = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        transform.Translate(direction * _speed * Time.deltaTime);
    }
}
