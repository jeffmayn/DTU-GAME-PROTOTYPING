using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class takePotion : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        FindObjectOfType<PlayerController>().maxHealth = 100;
        FindObjectOfType<GameManager>().restoreHP();
        Destroy(gameObject);
    }
}
