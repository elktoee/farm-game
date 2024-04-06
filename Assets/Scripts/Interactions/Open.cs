using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Open : Interactable
{
    [SerializeField] private Sprite image1;
    [SerializeField] private Sprite image2;
    private SpriteRenderer spriteRenderer;

    private Collider2D elCollider2D;

    [SerializeField] private bool isOpen;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        elCollider2D = GetComponent<Collider2D>();
    }
    public override void Interact(GameObject player)
    {
        if(isOpen){
            spriteRenderer.sprite = image1;
            elCollider2D.enabled = true;
            isOpen = false;
        }
        else{
            spriteRenderer.sprite = image2;
            elCollider2D.enabled = false;
            isOpen = true;
        }
    }
}
