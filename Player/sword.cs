using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] private GameObject slashAnimPrefab;
    [SerializeField] private Transform slashAnimSP;
    [SerializeField] private Transform weaponCollider;

    [SerializeField] private float attackCooldown = 1f; // Cooldown duration in seconds

    private PlayerControls playerControls;
    private Animator myAnimator;
    private playerController playerController;
    private activeWeapon activeWeapon;

    private GameObject slashAnim;
    private bool canAttack = true; // Flag to control attack availability
    int hit = 0;

    private void Awake() 
    {
        playerController = GetComponentInParent<playerController>();
        activeWeapon = GetComponentInParent<activeWeapon>();
        myAnimator = GetComponent<Animator>();
        playerControls = new PlayerControls();
    }

    private void OnEnable() 
    {
        playerControls.Enable();
    }

    void Start()
    {
        playerControls.Combat.Attack.started += _ => TryAttack();
    }

    private void Update() 
    {
        MouseFollowWithOffset();
    }

    private void TryAttack()
    {
        if (canAttack)
        {
            Attack();
        }
    }

    private void Attack() 
    {
        myAnimator.SetTrigger("Attack");
        weaponCollider.gameObject.SetActive(true);
        hit++;

        if (hit == 1)
        {
        slashAnim = Instantiate(slashAnimPrefab, slashAnimSP.position, Quaternion.identity);
        slashAnim.transform.parent = this.transform.parent;
        }

        if (hit >= 2)
        {
            canAttack = false; // Disable attack
            StartCoroutine(ResetAttackCooldown()); // Start cooldown timer
            hit = 0;
        }
    }

    private IEnumerator ResetAttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true; // Enable attack after cooldown
    }

    public void DoneAttackingAnimEvent()
    {
        weaponCollider.gameObject.SetActive(false);
    }

/*  ------ animacion del destello de golpe (va de abajo a arriba) --------
    public void SwingUpFlipAnimEvent()
    {
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);
    
        if(playerController.FacingLeft)
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }
*/
    public void SwingDownFlipAnimEvent()
    {
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
    
        if(playerController.FacingLeft)
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    private void MouseFollowWithOffset() 
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(playerController.transform.position);

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        if (mousePos.x < playerScreenPoint.x) {
            activeWeapon.transform.rotation = Quaternion.Euler(0, -180, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, -180, 0);
        } else {
            activeWeapon.transform.rotation = Quaternion.Euler(0, 0, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
