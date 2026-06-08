using UnityEngine;
using StarterAssets;
using Unity.Cinemachine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    private StarterAssetsInputs _input;
    private ThirdPersonController _thirdPersonCtrl;


    [Header("Aim"), SerializeField]
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
    private void Awake()
    {
        _input = GetComponent<StarterAssetsInputs>();
        _thirdPersonCtrl = GetComponent<ThirdPersonController>();
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
        if (_input.aim)
        {
            AimControll(true);

            Transform camTr = Camera.main.transform;
            RaycastHit hit;
            Vector3 targetPos = Vector3.zero;

            if (Physics.Raycast(camTr.position, camTr.forward, out hit, Mathf.Infinity, _targetLayer))
            {
                targetPos = hit.point;
                _aimObj.transform.position = hit.point;
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

        }
        else
        {
            AimControll(false);
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
}
