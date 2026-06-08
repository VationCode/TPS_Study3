using UnityEngine;

public class BulletManager : MonoBehaviour
{
    private Rigidbody _bulletRigd;

    [SerializeField]
    private float _moveSpeed = 50f;
    private float _destoryTime = 3f;
    private float _timer = 0;
    private void Awake()
    {
        _bulletRigd = GetComponent<Rigidbody>();
    }

    void Update()
    {
        BulletMove();
        DestoryBulet();
    }

    private void BulletMove()
    {
        _bulletRigd.linearVelocity = transform.forward * _moveSpeed;
    }

    private void DestoryBulet()
    {
        _timer += Time.deltaTime;
        if (_timer < _destoryTime) return;
        _timer = 0;

        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
    }
}
