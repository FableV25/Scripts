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

    [SerializeField] private TMP_Text quantityText;
    [SerializeField] private Image itemImage;
    public GameObject selectedShader;
    public bool thisItemSelected;
    private inventoryManager inventoryManager;

    private void Start()
    {
        inventoryManager = GameObject.Find("IUCanvas").GetComponent<inventoryManager>(); 
    }

    public void addItem(string itemName, int quantity, Sprite sprite)
    {
        this.itemName = itemName;
        this.quantity = quantity;
        this.sprite = sprite;
        isFull = true;

        quantityText.text = quantity.ToString();
        quantityText.enabled = true;
        itemImage.sprite = sprite;
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
    }

    public void OnRigthClick()
    {
        Debug.Log("Rigth Click");
    }
}
