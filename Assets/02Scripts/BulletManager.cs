using UnityEngine;

public class BulletManager : MonoBehaviour
{
    private Rigidbody _bulletRigd;

    [SerializeField]
    private float _moveSpeed = 10f;
    private void Awake()
    {
        _bulletRigd = GetComponent<Rigidbody>();
    }

    void Update()
    {
        BulletMove();
    }

    private void BulletMove()
    {
        _bulletRigd.linearVelocity = transform.forward * _moveSpeed;
    }
}
