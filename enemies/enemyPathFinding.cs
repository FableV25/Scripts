using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyPathfinding : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;

    private Rigidbody2D rb;
    private Vector2 moveDir;
    private knokback knokback;

    private void Awake() {
        knokback = GetComponent<knokback>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        if (knokback.GettingKnockedBack)
        {
            return;
        }

        rb.MovePosition(rb.position + moveDir * (moveSpeed * Time.fixedDeltaTime));
    }

    public void MoveTo(Vector2 targetPosition) {
        moveDir = targetPosition;
    }
}
