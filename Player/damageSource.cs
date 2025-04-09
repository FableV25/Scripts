using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damageSource : MonoBehaviour
{   
    [SerializeField] private int damageAmount;

    private void OnTriggerEnter2D(Collider2D other)
    {
        enemyHealt enemyHealt = other.gameObject.GetComponent<enemyHealt>();
        enemyHealt?.TakeDamage(damageAmount);
    }

}
