using System.Collections;
using System.Collections.Generic;
using Inventory.UI;
using UnityEngine;

public class MouseFollower : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas;


    [SerializeField]
    private Item item;

    private void Awake()
    {
        canvas = transform.root.GetComponent<Canvas>();
        item = GetComponentInChildren<Item>();
    }

    public void SetData(Sprite sprite, int quantity){
        item.SetData(sprite, quantity);
    }

    void Update()
    {
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)canvas.transform, 
        Input.mousePosition,
        canvas.worldCamera,
        out position );
        transform.position =canvas.transform.TransformPoint(position);
    }

    public void Toggle(bool val){
        Debug.Log($"Item toggled {val}");
        gameObject.SetActive(val);
    }
}
