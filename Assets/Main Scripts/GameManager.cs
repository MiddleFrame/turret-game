using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static bool Pause { get; set; } = false;
    public static Settings settings;
    public static PlayerStats playerStats;
    public static Upgrades upgrades;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        GameStats.NewGame();
    }
    public void NewGame()
   {
        GameStats.NewGame();
   }

    public void Paused(bool menu = false)
    {
        
        Pause = !Pause;
        Blur.EnableOrDisableBlur(Pause);
        Shoot.instance.Pause(Pause);
        GameStats.MenuController = menu;
        ChangeControl.instance.ChangeController(ChangeControl.instance.CurrentControl);
    }

    private void OnApplicationQuit()
    {
        OnApplicationPause(true);
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            SaveToPlayerPrefs<PlayerStats>(playerStats, "PlayerStats");
            SaveToPlayerPrefs<Upgrades>(upgrades, "Upgrades");
            Debug.Log(JsonUtility.ToJson(settings));
            if (settings.needSave)
            {
                SaveToPlayerPrefs<Settings>(settings, "ControlSetting");
                GameManager.settings.needSave = false;
               
            }
        }
        else
        {
            new LoadGame().Load();
        }
    }

   /*private void OnApplicationFocus(bool focus)
   {
        
   }*/
   public void GoToUrl(string url)
    {
        Application.OpenURL(url);
    }
    public static void SaveToPlayerPrefs<T>(T data, string key) 
    {
        PlayerPrefs.SetString(key, JsonUtility.ToJson(data));
    }
    public static T LoadFromPlayerPrefs<T>(string key) 
    {
        if (PlayerPrefs.HasKey(key))
            return JsonUtility.FromJson<T>(PlayerPrefs.GetString(key));
        else
            return default(T);
    }
}
