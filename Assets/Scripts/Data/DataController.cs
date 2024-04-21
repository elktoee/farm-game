using UnityEngine;

[RequireComponent(typeof(Timer))]
public class DataController: MonoBehaviour
{
    private static Timer timer;
    private static GameData gameData;

    public static GameData GameData { get => gameData; set => gameData = value; }

    private void Awake()
    {
        if (PlayerPrefs.HasKey("GameData")) {
            string serializedData = PlayerPrefs.GetString("GameData");
            gameData = JsonUtility.FromJson<GameData>(serializedData);
        } else {
            gameData = new GameData();
        }
    }

    private void Start(){
        timer = gameObject.GetComponent<Timer>();

        //TimedActionManager.instance.ExecuteActionAtInterval(()=>{SaveGameData();},7);
    }

    public static void SaveGameData()
    {
        gameData.Time = timer.Time;
        PlayerPrefs.SetString("GameData", JsonUtility.ToJson(gameData));
        PlayerPrefs.Save();
        
    }
}