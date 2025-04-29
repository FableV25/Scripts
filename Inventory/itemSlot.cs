using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class itemSlot : MonoBehaviour, IPointerClickHandler
{
    public string itemName;
    public int quantity;
    public Sprite sprite;
    public bool isFull;
    public string description;
    public Sprite empty;
    [SerializeField] private int maxItemNum;

    [SerializeField] private TMP_Text quantityText;
    [SerializeField] private Image itemImage;
    public GameObject selectedShader;
    public bool thisItemSelected;
    private inventoryManager inventoryManager;

    //----
    public Image itemDescriptionImage;
    public TMP_Text itemDescriptionName;
    public TMP_Text itemDescription;

    private void Start()
    {
        inventoryManager = GameObject.Find("IUCanvas").GetComponent<inventoryManager>(); 
    }

    public int addItem(string itemName, int quantity, Sprite sprite, string description)
    {

        if(isFull)
        {
            return quantity;
        }

        this.itemName = itemName;
        this.sprite = sprite;
        itemImage.sprite = sprite;
        this.description = description;

        this.quantity += quantity;
        if(this.quantity >= maxItemNum)
        {
            quantityText.text = maxItemNum.ToString();
            quantityText.enabled = true;
            isFull = true;
        
            int extraItems = this.quantity - maxItemNum;
            this.quantity = maxItemNum;
            return extraItems;
        }
        
        quantityText.text = this.quantity.ToString();
        quantityText.enabled = true;

        return 0;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();
        }
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            OnRigthClick();
        }
    }

    public void OnLeftClick()
    {
        inventoryManager.deSelect();
        selectedShader.SetActive(true);
        thisItemSelected = true;

        itemDescriptionName.text = itemName;
        itemDescription.text = description;
        itemDescriptionImage.sprite = sprite; 

        if(itemDescriptionImage.sprite == null)
        {
            itemDescriptionImage.sprite = empty;
        }
    }

    public void OnRigthClick()
    {
        Debug.Log("Rigth Click");
    }
}
