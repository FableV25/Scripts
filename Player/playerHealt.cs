using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHealt : MonoBehaviour
{
    [SerializeField] private int maxHP = 10;
    [SerializeField] private float knockBackTrhustAmount = 10f;
    [SerializeField] private float damageRecoberyTime = 1f;

    private int currentHP;
    private bool canTakeDamage = true;
    private knokback knokback;
    private flash flash;

    private void Awake()
    {
        flash = GetComponent<flash>();
        knokback = GetComponent<knokback>();
    }

    void Start()
    {
        currentHP = maxHP;
    }


    private void OnCollisionStay2D(Collision2D other)
    {
        enemyAI enemy = other.gameObject.GetComponent<enemyAI>();

        if(enemy && canTakeDamage)
        {
            TakeDamage(1);
            knokback.GetKnockedBack(other.gameObject.transform, knockBackTrhustAmount);
            StartCoroutine(flash.FlashRoutine());
        }
    }
    private void TakeDamage(int damageAmount)
    {
        canTakeDamage = false;
        currentHP -=    damageAmount;
        StartCoroutine(damageRecoberyRoutine());
    }

    private IEnumerator damageRecoberyRoutine()
    {
        yield return new WaitForSeconds(damageRecoberyTime);
        canTakeDamage = true;
    }
}
