using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour, IWeapon
{
    [SerializeField] private GameObject slashAnimPrefab;
    [SerializeField] private Transform slashAnimSpawnPoint;
    [SerializeField] private float swordAttackCD = .5f;


    private Transform weaponCollider;
    private Animator myAnimator;
    private GameObject slashAnim;
    private int attackCount = 0;
    private bool isAttacking = false;
    public DialogueTrigger dialogueTrigger;


    private void Awake() {
        myAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
        weaponCollider = playerController.Instance.GetWeaponCollider();
        slashAnimSpawnPoint = GameObject.Find("SlashSP").transform;
    }

    private void Update() {
        MouseFollowWithOffset();
    }

    public void Attack() {
      //  if (dialogueTrigger.panelOpen == false)
        //{
            // isAttacking = true;
            myAnimator.SetTrigger("Attack");
            weaponCollider.gameObject.SetActive(true);
            slashAnim = Instantiate(slashAnimPrefab, slashAnimSpawnPoint.position, Quaternion.identity);
            slashAnim.transform.parent = this.transform.parent;
            StartCoroutine(AttackCDRoutine());
        //}
    }

    private IEnumerator AttackCDRoutine() {
        yield return new WaitForSeconds(swordAttackCD);
        activeWeapon.Instance.ToggleIsAttacking(false);
    }

    public void DoneAttackingAnimEvent() {
        weaponCollider.gameObject.SetActive(false);
    }

    public void SwingDownFlipAnimEvent() {
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);

        if (playerController.Instance.FacingLeft)
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    private void MouseFollowWithOffset() {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(playerController.Instance.transform.position);

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        if (mousePos.x < playerScreenPoint.x) {
            activeWeapon.Instance.transform.rotation = Quaternion.Euler(0, -180, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, -180, 0);
        } else {
            activeWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
