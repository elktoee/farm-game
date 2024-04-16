using UnityEngine;
using System.Collections.Generic;
using System;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private Material materialOutline;
    private Material originalMaterial;
    private List<InteractableObject> interactableObjects = new List<InteractableObject>(); 

    private class InteractableObject{
        GameObject gameObject;
        Material originalMaterial;
        Renderer rend;
        Interactable interactable ;

        public InteractableObject(GameObject gameObject){
            this.GameObject = gameObject;
            Rend = gameObject.GetComponent<Renderer>();
            if (Rend != null)
            {
                OriginalMaterial = Rend.material;
            }

            Interactable = gameObject.GetComponent<Interactable>();
        }

        public GameObject GameObject { get => gameObject; set => gameObject = value; }
        public Material OriginalMaterial { get => originalMaterial; set => originalMaterial = value; }
        public Renderer Rend { get => rend; set => rend = value; }
        public Interactable Interactable { get => interactable; set => interactable = value; }
    }
    
    private InteractableObject closestObject;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Action"))
        {
            interactableObjects.Add(new InteractableObject(other.gameObject));
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Action"))
        {
            InteractableObject interactableObjectToRemove = interactableObjects.Find(obj => obj.GameObject == other.gameObject);
            if (interactableObjectToRemove != null)
            {
                interactableObjects.Remove(interactableObjectToRemove);
                if (interactableObjectToRemove == closestObject)
                {
                    OnClosestObjectChanged(closestObject);
                    closestObject = null;
                }
            }
        }
    }

    private void OnClosestObjectChanged(InteractableObject oldClosestObject)
    {
        oldClosestObject.Rend.material=oldClosestObject.OriginalMaterial;
    }

    private void Update()
    {
        FindClosestObject();
        UpdateInteractionUI();
    }

    private void FindClosestObject()
    {
        InteractableObject closestObject = null;
        float closestDistanceSqr = float.MaxValue;
        foreach (InteractableObject obj in interactableObjects)
        {
            float distanceSqr = (transform.position - obj.GameObject.transform.position).sqrMagnitude;
            if (distanceSqr < closestDistanceSqr)
            {
                closestDistanceSqr = distanceSqr;
                closestObject = obj;
            }
        }
        if (this.closestObject != closestObject){
            if (this.closestObject!=null) OnClosestObjectChanged(this.closestObject);
            this.closestObject = closestObject;
        }
    }

    private void UpdateInteractionUI()
    {
        if (closestObject != null)
        {
            SetAction(closestObject);
        }
        else
        {
            InteractionUI.Hide();
        }
    }

    private void SetAction(InteractableObject obj)
    {
        
        if (obj.Interactable != null)
        {
            InteractionUI.Action = obj.Interactable;
            InteractionUI.Show();
            obj.Rend.material = materialOutline;
            
        }
    }
}
