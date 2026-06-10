using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private Slider _hpBar;

    private float _enemyMaxHP = 10;
    public float EnemyCurrentHP = 0;

    private NavMeshAgent _agent;
    private Animator _anim;

    private GameObject _targetObj;
    private float _targetDelay;

    private CapsuleCollider _collider;
    private void Awake()
    {
        _anim = GetComponentInChildren<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        _targetObj = GameObject.FindWithTag("Player").gameObject;
        _collider = GetComponent<CapsuleCollider>();
    }

    void Start()
    {
        _agent.speed = 1;
        _targetDelay = 0f;
        InitEnmyHP();
    }

    private void Update()
    {
        _hpBar.value = EnemyCurrentHP / _enemyMaxHP;

        if(EnemyCurrentHP <= 0)
        {
            StartCoroutine(EnemyDie());
            return;
        }

        if(_targetObj != null)
        {
            float maxDelay = 1f;
            _targetDelay += Time.deltaTime;

            if (_targetDelay < maxDelay) return;
            _agent.destination = _targetObj.transform.position;
            transform.LookAt(_targetObj.transform.position);

            bool isRange = Vector3.Distance(transform.position, _targetObj.transform.position) <=
                _agent.stoppingDistance;

            if (isRange)
            {
                _agent.speed = 0;
                _anim.SetTrigger("Attack");
            }
            else
            {
                _agent.speed = 1;
                _anim.SetFloat("MoveSpeed", _agent.velocity.magnitude);
            }

            _targetDelay = 0f;
        }
    }
    private void InitEnmyHP()
    {
        EnemyCurrentHP = _enemyMaxHP;
    }

    private IEnumerator EnemyDie()
    {
        _agent.speed = 0;
        _anim.SetTrigger("Dead");
        _collider.enabled = false;

        yield return new WaitForSeconds(3f);
        // Destroy(gameObject);
        gameObject.SetActive(false);
        _collider.enabled = true;
        InitEnmyHP();
        _agent.speed = 1;

    }
}
