using System;
using System.Collections;
using System.Collections.Generic;
using Inventory.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectedItemInfoPanel : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI itemNameText;

    [SerializeField]
    private TextMeshProUGUI itemDescriptionText;

    [SerializeField]
    private Image itemImage;

    [SerializeField]
    private TextMeshProUGUI amountText;

    [SerializeField]
    private TextMeshProUGUI priceText;

    [SerializeField] 
    private GameObject gameObjectBuy;

    [SerializeField]
    private GameObject sellButton;

    [SerializeField]
    private GameObject buyButton;

    [SerializeField]
    private ShopInventory shopInventory;

    public int amount = 1;

    private int price = 1;
    

    public void Awake(){
        ResetDescription();
    }

    public void ResetDescription()
    {
        this.itemImage.gameObject.SetActive(false);
        this.itemNameText.text = "";
        this.itemDescriptionText.text = "";
        this.priceText.text = "";
        this.gameObjectBuy.SetActive(false);
        }

    public void SetDescription(Sprite sprite, string itemName, string itemDescription, int price)
    {
        amount = 1;
        this.price = price;
        this.itemImage.gameObject.SetActive(true);
        this.itemImage.sprite = sprite;
        this.itemNameText.text = itemName;
        this.itemDescriptionText.text = itemDescription;
        this.priceText.text = price + "Gold";
        this.gameObjectBuy.SetActive(true);
        this.amountText.text = amount + "";
        if(shopInventory.shopPage){
            sellButton.SetActive(false);
            buyButton.SetActive(true);
        }
        else{
            sellButton.SetActive(true);
            buyButton.SetActive(false);
        }
    }

    public void IncreaseAmount(){
        if(amount < 9){
            
            if(shopInventory.shopPage){
            amount++;
            this.amountText.text = amount + "";
            this.priceText.text = price*amount + "Gold";
            }
            else if(amount < shopInventory.GetItemCount(shopInventory.selectedItem)){
            amount++;
            this.amountText.text = amount + "";
            this.priceText.text = price*amount + "Gold";
            }
        }
    }

    public void DecreaseAmount(){
        if(amount > 1){
            amount--;
            this.amountText.text = amount + "";
        if(shopInventory.shopPage){
            this.priceText.text = price*amount + "Gold";
            }
            else {
            this.priceText.text = price*amount + "Gold";
            }
        }
    }
}
