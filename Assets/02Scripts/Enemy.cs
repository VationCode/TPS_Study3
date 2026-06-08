using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int _enemyMaxHP = 100;
    public int EnemyCurrentHP = 0;

    void Start()
    {
        
    }

    private void InitEnmyHP()
    {
        EnemyCurrentHP = _enemyMaxHP;
    }
}
