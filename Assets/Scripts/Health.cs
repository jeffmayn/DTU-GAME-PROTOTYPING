using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float health = 100f;

    public void TakeDamage(float dmg)
    {
        health = Mathf.Max(health - dmg, 0);
        if(health <= 0)
        {
            GetComponent<EnemyController>().Die();
            //Destroy(gameObject);
        }
    }
}
