
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fist : MonoBehaviour, IWeapon
{
    public void Attack()
    {
        Debug.Log("Fist - puño");
        activeWeapon.Instance.ToggleIsAttacking(false);
    }
}
