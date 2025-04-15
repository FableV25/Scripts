using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour, IWeapon
{
    public void Attack()
    {
        Debug.Log("Lamp - lampara");
        activeWeapon.Instance.ToggleIsAttacking(false);
    }
}