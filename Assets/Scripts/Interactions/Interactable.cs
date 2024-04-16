using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public abstract void Interact(GameObject player);
    [SerializeField]
    private Sprite icon;

    public Sprite Icon { get => icon; set => icon = value; }
}
