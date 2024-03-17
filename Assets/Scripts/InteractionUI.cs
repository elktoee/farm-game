using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InteractionUI : MonoBehaviour
{
    private static GameObject instanse;
    static Image imageObj;
    static Interactable action;

    static GameObject player;
    public static Interactable Action
    {
        get => action; set
        {
            action = value;
            imageObj.sprite = value.Icon;
        }
        
    }

    private void Start(){
        player = GameObject.FindWithTag("Player");
        instanse = gameObject;
        imageObj = this.GetComponentsInChildren<Image>()[1];
        hide();
    }

    public static void hide(){
        if (instanse!=null) instanse.SetActive(false);
    }

    public static void show(){
        instanse.SetActive(true);
    }

    public static void OnClick(){
        action.Interact(player);
    }
}