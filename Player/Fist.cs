
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fist : MonoBehaviour, IWeapon
{

    private void Update()
    {
        mouseFollowWithOffset();
    }

    public void Attack()
    {
        Debug.Log("Fist - puño");
        activeWeapon.Instance.ToggleIsAttacking(false);
    }

    private void mouseFollowWithOffset()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(playerController.Instance.transform.position);

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        if (mousePos.x < playerScreenPoint.x) {
            activeWeapon.Instance.transform.rotation = Quaternion.Euler(0, -180, angle);
        } else {
            activeWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}