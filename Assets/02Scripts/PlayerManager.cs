using UnityEngine;
using StarterAssets;
using Unity.Cinemachine;
using UnityEngine.Animations.Rigging;

public class PlayerManager : MonoBehaviour
{
    private StarterAssetsInputs _input;
    private ThirdPersonController _thirdPersonCtrl;
    private Animator _anim;

    [Header("IsAim"), SerializeField]
    private CinemachineVirtualCamera _aimCam;

    [SerializeField]
    private GameObject[] _aimImgs;
    [SerializeField]
    private float _aimRotSpeed = 50;
    [SerializeField]
    private GameObject _aimObj;
    [SerializeField]
    private float _aimObjDis = 20f;
    
    [SerializeField]
    private LayerMask _targetLayer;

    [Header(" IK "), SerializeField]
    private Rig _handRig;
    [SerializeField]
    private Rig _aimRig;

    private Enemy _enemy;
    private void Awake()
    {
        _input = GetComponent<StarterAssetsInputs>();
        _thirdPersonCtrl = GetComponent<ThirdPersonController>();
        _anim = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        AimCheck();
    }

    private void AimCheck()
    {
        if(_input.IsReload)
        {
            _input.IsReload = false;

            if (_thirdPersonCtrl.IsReload) return;

            AimControll(false);
            SetRigWeight(0);
            _anim.SetLayerWeight(1,1);
            _anim.SetTrigger("Reload");
            _thirdPersonCtrl.IsReload = true;
        }

        if(_thirdPersonCtrl.IsReload)
        {
            return;
        }

        if (_input.IsAim)
        {
            AimControll(true);

            _anim.SetLayerWeight(1,1);

            Transform camTr = Camera.main.transform;
            RaycastHit hit;
            Vector3 targetPos = Vector3.zero;

            if (Physics.Raycast(camTr.position, camTr.forward, out hit, Mathf.Infinity, _targetLayer))
            {
                targetPos = hit.point;
                _aimObj.transform.position = hit.point;

                _enemy = hit.collider.gameObject.GetComponent<Enemy>();
            }
            else
            {
                targetPos = camTr.position + camTr.forward * _aimObjDis;
                _aimObj.transform.position = targetPos;
            }

            Vector3 targetAim = targetPos;
            targetAim.y = transform.position.y;
            Vector3 aimDir = (targetAim - transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, aimDir, Time.deltaTime * _aimRotSpeed);

            SetRigWeight(1);

            if (_input.IsShoot)
            {
                _anim.SetBool("Shoot", true);
                GameManager.Instance.Shooting(targetPos, _enemy);
            }
            else
            {
                _anim.SetBool("Shoot", false);
            }

        }
        else
        {
            AimControll(false);
            SetRigWeight(0);
            _anim.SetLayerWeight(1, 0);
            _anim.SetBool("Shoot", false);
        }
    }

    private void AimControll(bool p_isCheck)
    {
        _aimCam.gameObject.SetActive(p_isCheck);
        for(int i = 0; i < _aimImgs.Length; i++)
        {
            _aimImgs[i].SetActive(p_isCheck);
        }
        _thirdPersonCtrl.IsAimMove = p_isCheck;
    }

    public void Reload()
    {
        _thirdPersonCtrl.IsReload = false;
        SetRigWeight(1);
        _anim.SetLayerWeight(1,0);
    }

    private void SetRigWeight(float p_weight)
    {
        _aimRig.weight = p_weight;
        _handRig.weight = p_weight;
    }
}
