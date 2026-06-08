using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Bullet"), SerializeField]
    private Transform _firePos;
    [SerializeField]
    private GameObject _bulletObj;
    [SerializeField]
    private float _maxShootDelay = 0.1f;
    private float _currentShootTimer = 0;
    [SerializeField]
    private TextMeshProUGUI _bulletTMP;
    private int _maxBullet = 30;
    private int _currentBullet = 0;

    [Header("Weapon FX"), SerializeField]
    private GameObject _muzzleFlashFX;
    [SerializeField]
    private Transform _muzzleFlashPos;
    [SerializeField]
    private GameObject _shellEjectEffectFX;
    [SerializeField]
    private Transform _shellEjectPos;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _currentShootTimer = 0;
        InitBullet();
        _bulletTMP.text = _currentBullet + " / " + _maxBullet;
    }
    private void Update()
    {
        _bulletTMP.text = _currentBullet + " / " + _maxBullet;
    }

    public void Shooting(Vector3 p_targetPos, Enemy p_enemy)
    {
        _currentShootTimer += Time.deltaTime;
        if (_currentShootTimer < _maxShootDelay || _currentBullet <= 0) return;

        _currentBullet--;
        _currentShootTimer = 0;
        Vector3 aimDir = (p_targetPos - _firePos.position).normalized;

        GameObject muzzleFX = PoolManager.Instance.ActivateObj(1);
        SetObjPosition(muzzleFX, _firePos);
        muzzleFX.transform.rotation = Quaternion.LookRotation(aimDir, Vector3.up);

        GameObject shellEjectFX = PoolManager.Instance.ActivateObj(2);
        SetObjPosition(shellEjectFX, _firePos);
        shellEjectFX.transform.rotation = Quaternion.LookRotation(aimDir, Vector3.up);

        GameObject prefabToSpawn = PoolManager.Instance.ActivateObj(0);
        SetObjPosition(prefabToSpawn, _firePos);
        prefabToSpawn.transform.rotation = Quaternion.LookRotation(aimDir, Vector3.up);

        /*if(p_enemy != null && p_enemy.EnemyCurrentHP > 0)
        {
            p_enemy.EnemyCurrentHP -= 1;
        }*/
    }

    public void ReloadClip()
    {
        
    }

    private void InitBullet()
    {
        _currentBullet = _maxBullet;
    }

    private void SetObjPosition(GameObject p_obj, Transform p_targetTr)
    {
        p_obj.transform.position = p_targetTr.position;
    }
}
