using UnityEngine;
public abstract class Interactable: MonoBehaviour{
    public abstract void Interact(GameObject player);
    protected Sprite icon;
}