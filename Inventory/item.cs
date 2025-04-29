using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item : MonoBehaviour
{

    [SerializeField] private string itemName;
    [SerializeField] private int quantity;
    [TextArea][SerializeField] private string description;
    [SerializeField] private Sprite sprite;
    
    private inventoryManager inventoryManager;

    void Start()
    {
        inventoryManager = GameObject.Find("IUCanvas").GetComponent<inventoryManager>();   
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            int leftOverItems = inventoryManager.addItem(itemName, quantity, sprite, description);
            if (leftOverItems <= 0)
            {
                Destroy(gameObject);
            }
            else 
            {
                quantity = leftOverItems;
            }
        }

    }
}
