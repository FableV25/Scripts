using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Lamp : MonoBehaviour, IWeapon
{

    [SerializeField] private weaponInfo weaponInfo;
    [SerializeField] private Light2D lampLight; // Or Light2D if using URP




    private void Start()
    {
        lampLight = GetComponentInChildren<Light2D>(); // Or Light2D
    }

    public void Attack()
    {
        if (lampLight != null)
        {
            Debug.Log("Lamp on - lamapra prendido");
            lampLight.enabled = !lampLight.enabled;
            Debug.Log("Lamp off - lamapra apagado");
        }
    }

    private void Update()
    {
        mouseFollowWithOffset();
    }



    public weaponInfo GetWeaponInfo()
    {
        return weaponInfo;
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