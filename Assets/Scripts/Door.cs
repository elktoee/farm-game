using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : Interactable
{
    [SerializeField]
    private int scene;

    public override void Interact(GameObject player){
        DataController.SaveGameData();
        SceneManager.LoadScene(scene);
    }
}