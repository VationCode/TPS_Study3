using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Bullet"), SerializeField]
    private Transform _firePos;
    [SerializeField]
    private GameObject _bulletObj;

    [Header("Weapon FX"), SerializeField]
    private GameObject _muzzleFlashFX;
    [SerializeField]
    private Transform _muzzleFlashPos;

    private void Awake()
    {
        Instance = this;
    }

    public void Shooting(Vector3 p_targetPos)
    {
        Instantiate(_muzzleFlashFX, _muzzleFlashPos);

        Vector3 aimDir = (p_targetPos - _firePos.position).normalized;
        Instantiate(_bulletObj, _firePos.position, Quaternion.LookRotation(aimDir));
    }
}
