using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventorySlot : MonoBehaviour
{
    [SerializeField] private weaponInfo weaponInfo;
    
    public weaponInfo GetWeaponInfo()
    {
        return weaponInfo;
    }
}
