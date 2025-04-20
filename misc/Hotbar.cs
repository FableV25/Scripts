using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Hotbar : MonoBehaviour
{
    private int activeSlotIndexNum = 0;
    private PlayerControls playerControls;

    private void Awake() 
    {
        playerControls = new PlayerControls();
    }

    private void Start() 
    {
        playerControls.Inventory.Keyboard.performed += ctx => ToggleActiveSlot((int)ctx.ReadValue<float>());

        ToggleActiveHighlight(0);
    }

    private void OnEnable() 
    {
        playerControls.Enable();
    }

    private void ToggleActiveSlot(int numValue) 
{
    ToggleActiveHighlight(numValue - 1);
    GetComponent<HotbarFade>()?.ResetFadeTimer(); // Notify the fader of activity
}

    private void ToggleActiveHighlight(int indexNum) 
    {

        activeSlotIndexNum = indexNum;

        foreach (Transform inventorySlot in this.transform)
        {
            inventorySlot.GetChild(0).gameObject.SetActive(false);
        }

        this.transform.GetChild(indexNum).GetChild(0).gameObject.SetActive(true);

        ChangeActiveWeapon();
    }

    private void ChangeActiveWeapon()
    {
        //Debug.Log(transform.GetChild(activeSlotIndexNum).GetComponent<inventorySlot>().GetWeaponInfo().weaponPrefab.name);
        if (activeWeapon.Instance.CurrentActiveWeapon != null)
        {
            Destroy(activeWeapon.Instance.CurrentActiveWeapon.gameObject);
        }

        if (!transform.GetChild(activeSlotIndexNum).GetComponentInChildren<inventorySlot>())
        {
            activeWeapon.Instance.WeaponNull();
            return;
        }

        GameObject weaponToSpawn = transform.GetChild(activeSlotIndexNum).GetComponentInChildren<inventorySlot>().GetWeaponInfo().weaponPrefab;

        GameObject newWeapon = Instantiate(weaponToSpawn, activeWeapon.Instance.transform.position, Quaternion.identity);

        activeWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, 0);
        newWeapon.transform.parent = activeWeapon.Instance.transform;

        activeWeapon.Instance.NewWeapon(newWeapon.GetComponent<MonoBehaviour>());
    }
    
}
