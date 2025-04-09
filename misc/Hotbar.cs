using System.Collections;
using System.Collections.Generic;
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
    }

    
}
