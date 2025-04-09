using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knokback : MonoBehaviour
{
    public bool GettingKnockedBack
    {
        get; 
        private set;
    }

    [SerializeField] private float knokBackTime = .2f;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void GetKnockedBack(Transform damageSource, float knokBackThrust)
    {
        GettingKnockedBack = true;
        Vector2 difference = (transform.position - damageSource.position).normalized * knokBackThrust * rb.mass;
        rb.AddForce(difference, ForceMode2D.Impulse);
        StartCoroutine(KnockRoutine()); 
    }

    private IEnumerator KnockRoutine()
    {
        yield return new WaitForSeconds(knokBackTime);
        rb.velocity = Vector2.zero;
        GettingKnockedBack = false;
    }


}
