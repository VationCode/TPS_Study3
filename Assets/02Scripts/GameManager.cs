using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;

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
    private ParticleSystem _muzzleFlashFX;
    [SerializeField]
    private ParticleSystem _shellEjectEffectFX;

    [Header("Enemy")]
    [SerializeField]
    private Transform[] _spawnPoint;

    [Header("BGM")]
    [SerializeField]
    private AudioClip _bgmSoundClip;
    private AudioSource _bgmAudio;

    private PlayableDirector _cut;
    public bool IsReady = true;
    private void Awake()
    {
        Instance = this;
        _bgmAudio = GetComponent<AudioSource>();
        _cut = GetComponent<PlayableDirector>();
        
    }

    private void Start()
    {
        _currentShootTimer = 0;
        InitBullet();

        _bulletTMP.text = _currentBullet + " / " + _maxBullet;
        _cut.Play();
    }
    private void Update()
    {
        _bulletTMP.text = _currentBullet + " / " + _maxBullet;
    }

    public void Shooting(Vector3 p_targetPos, Enemy p_enemy, AudioSource p_audio, AudioClip p_clip)
    {
        _currentShootTimer += Time.deltaTime;
        if (_currentShootTimer < _maxShootDelay || _currentBullet <= 0) return;

        _currentBullet--;
        _currentShootTimer = 0;
        Vector3 aimDir = (p_targetPos - _firePos.position).normalized;

        /*GameObject muzzleFX = PoolManager.Instance.ActivateObj(1);
        SetObjPosition(muzzleFX, _firePos);
        muzzleFX.transform.rotation = Quaternion.LookRotation(aimDir, Vector3.up);

        GameObject shellEjectFX = PoolManager.Instance.ActivateObj(2);
        SetObjPosition(shellEjectFX, _firePos);
        shellEjectFX.transform.rotation = Quaternion.LookRotation(aimDir, Vector3.up);*/
        _muzzleFlashFX.Play();
        _shellEjectEffectFX.Play();


        GameObject prefabToSpawn = PoolManager.Instance.ActivateObj(0);
        SetObjPosition(prefabToSpawn, _firePos);
        prefabToSpawn.transform.rotation = Quaternion.LookRotation(aimDir, Vector3.up);

        p_audio.PlayOneShot(p_clip);
        /*if(p_enemy != null && p_enemy.EnemyCurrentHP > 0)
        {
            p_enemy.EnemyCurrentHP -= 1;
        }*/
    }

    public void ReloadClip()
    {
        InitBullet();
    }

    private void InitBullet()
    {
        _currentBullet = _maxBullet;
    }

    private void SetObjPosition(GameObject p_obj, Transform p_targetTr)
    {
        p_obj.transform.position = p_targetTr.position;
        p_obj.transform.rotation = p_targetTr.rotation;
    }

    IEnumerator EnemySpawn()
    {
        //Instantiate(_enemyObj, _spawnPoint[Random.Range(0,_spawnPoint.Length)].transform.position, Quaternion.identity);
        GameObject enemyObj = PoolManager.Instance.ActivateObj(1);
        SetObjPosition(enemyObj, _spawnPoint[Random.Range(0, _spawnPoint.Length)]);

        yield return new WaitForSeconds(2f);

        StartCoroutine(EnemySpawn());
    }

    private void PlayBGMSound()
    {
        _bgmAudio.clip = _bgmSoundClip;
        _bgmAudio.loop = true;
        _bgmAudio.Play();
    }

    public void StartGame()
    {
        IsReady = false;
        PlayBGMSound();
        StartCoroutine(EnemySpawn());
    }
}
