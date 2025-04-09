using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHealt : MonoBehaviour
{
    [SerializeField] private int startingHealt = 1;
    [SerializeField] private GameObject deathVFXPreFab;
    [SerializeField] private float knockBackThrust = 15f;
    [SerializeField] private string enemyID; // Unique ID for each enemy

    private int currentHealt;
    private knokback knokback;
    private flash flash;

    private void Awake()
    {
        flash = GetComponent<flash>();
        knokback = GetComponent<knokback>();

        // Check if this enemy was already defeated
        if (enemyTracker.Instance != null && enemyTracker.Instance.IsEnemyDefeated(enemyID))
        {
            Destroy(gameObject); // Prevent respawning
        }
    }

    private void Start()
    {
        currentHealt = startingHealt;
    }

    public void TakeDamage(int damage)
    {
        currentHealt -= damage;
        knokback.GetKnockedBack(playerController.Instance.transform, knockBackThrust);
        StartCoroutine(flash.FlashRoutine()); 
        StartCoroutine(CheckDetectDeathRoutine());
    }

    private IEnumerator CheckDetectDeathRoutine()
    {
        yield return new WaitForSeconds(flash.GetRestoredMatTime());
        DetectDeath();
    }

    public void DetectDeath()
    {
        if (currentHealt <= 0)
        {
            Instantiate(deathVFXPreFab, transform.position, Quaternion.identity);
            
            // Store enemy death in Singleton
            if (enemyTracker.Instance != null)
            {
                enemyTracker.Instance.MarkEnemyAsDefeated(enemyID);
            }

            Destroy(gameObject);
        }
    }
}
