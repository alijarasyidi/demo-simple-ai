using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _rigidbody;
    [SerializeField]
    private float _speed = 2f;

    private Vector2 _movement = Vector2.zero;

    void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");

        _movement = new Vector2(horizontal, vertical);
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector3(_movement.y, 0f, -_movement.x) * _speed;
    }
}
