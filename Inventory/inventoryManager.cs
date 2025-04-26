using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventoryManager : MonoBehaviour
{
    public GameObject inventoryMenu;
    public GameObject hotBar;
    private bool menuActive; 
    public itemSlot[] itemSlot;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab) && menuActive)
        {
            Time.timeScale = 1;
            inventoryMenu.SetActive(false);
            hotBar.SetActive(true);
            menuActive = false;
        }
        else if (Input.GetKeyDown(KeyCode.Tab) && !menuActive)
        {
            Time.timeScale = 0;
            inventoryMenu.SetActive(true);
            hotBar.SetActive(false);
            menuActive = true;
        }
    }

    public void addItem(string itemName, int quantity, Sprite sprite)
    {
        for (int i = 0;  i < itemSlot.Length; i++)
        {
            if(itemSlot[i].isFull == false)
            {
                itemSlot[i].addItem(itemName, quantity, sprite);
                return;
            }    
        }
    }

    public void deSelect()
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            itemSlot[i].selectedShader.SetActive(false);
            itemSlot[i].thisItemSelected = false;
        }
    }
}
