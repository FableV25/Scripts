using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Scripting.APIUpdating;

public class playerController : singleton<playerController>
{
    public bool FacingLeft 
    { 
        get { return facingLeft; }
    }

    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float dashSpeed = 2f;
    [SerializeField] private float dashCD = 3f;
    [SerializeField] private TrailRenderer MyTrailRenderer;
    [SerializeField] private Transform weaponCollider;

    

    private PlayerControls playerControls;
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator myAnimator;
    private SpriteRenderer mySpriteRenderer;
    private knokback knokback;

    private bool facingLeft = false;
    private bool isDashing = false;
    private bool isMoving = false; // New variable to track movement
    private float startingMoveSpeed;

    protected override void Awake()
    {
        base.Awake();

        playerControls = new PlayerControls(); 
        rb = GetComponent<Rigidbody2D>();

        myAnimator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        knokback = GetComponent<knokback>();
    }

    private void Start()
    {
        playerControls.Combat.Dash.performed += _ => Dash();
        startingMoveSpeed = moveSpeed;
    }   

    private void OnEnable()
    {
        playerControls.Enable();        
    }

    private void PlayerInput()  
    {
        movement = playerControls.Movement.Move.ReadValue<Vector2>();

        isMoving = movement.sqrMagnitude > 0.01f; // Checks if movement vector has a value

        myAnimator.SetFloat("moveX", movement.x);
        myAnimator.SetFloat("moveY", movement.y);
    } 

    private void Update()
    {
        PlayerInput();
    }

    private void Move()
    {
        if (knokback.GettingKnockedBack)
        {
            return;
        }

        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime ));
    }

    private void FixedUpdate()
    {
        AdjustPlayerFacingDirection();
        Move();
    }

    public Transform GetWeaponCollider()
    {
        return weaponCollider;
    }

    private void AdjustPlayerFacingDirection()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

        if (mousePos.x < playerScreenPoint.x)
        {
            mySpriteRenderer.flipX = true;
            facingLeft = true;
        } 
        else 
        {
            mySpriteRenderer.flipX = false;
            facingLeft = false;
        }
    }

    private void Dash()
    {
        if (!isDashing && isMoving) // Prevent dashing if not moving
        {
            isDashing = true;
            moveSpeed *= dashSpeed;
            MyTrailRenderer.emitting = true;
            StartCoroutine(EndDashRoutine());
        }
    }

    private IEnumerator EndDashRoutine()
    {
        float dashTime = .2f;
        yield return new WaitForSeconds(dashTime);
        moveSpeed = startingMoveSpeed;
        MyTrailRenderer.emitting = false;
        yield return new WaitForSeconds(dashCD);
        isDashing = false;
    }
    
    
}
