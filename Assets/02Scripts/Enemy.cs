using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private Slider _hpBar;

    private float _enemyMaxHP = 10;
    public float EnemyCurrentHP = 0;

    void Start()
    {
        InitEnmyHP();
    }

    private void InitEnmyHP()
    {
        EnemyCurrentHP = _enemyMaxHP;
    }

    private void Update()
    {
        _hpBar.value = EnemyCurrentHP / _enemyMaxHP;
    }
}
