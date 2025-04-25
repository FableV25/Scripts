using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventoryManager : MonoBehaviour
{
    public GameObject inventoryMenu;
    public GameObject hotBar;
    private bool menuActive; 

    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I) && menuActive)
        {
            Time.timeScale = 1;
            inventoryMenu.SetActive(false);
            hotBar.SetActive(true);
            menuActive = false;
        }
        else if (Input.GetKeyDown(KeyCode.I) && !menuActive)
        {
            Time.timeScale = 0;
            inventoryMenu.SetActive(true);
            hotBar.SetActive(false);
            menuActive = true;
        }
    }

    public void addItem(string itemName, int quantity, Sprite sprite)
    {
        Debug.Log("item: " + itemName + " quantity: " + quantity + " sprite: " + sprite);
    }
}
