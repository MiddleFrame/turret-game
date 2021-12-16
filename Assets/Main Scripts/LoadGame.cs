using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadGame {
    public void Load()
    {
        GameManager.settings = GameManager.LoadFromPlayerPrefs<Settings>("ControlSetting");
        if (GameManager.settings == null)
            GameManager.settings = new Settings();
        Debug.Log(JsonUtility.ToJson(GameManager.settings));
        
        if (!GameManager.settings.enableSound)
            Sound.instance.ChangeSound();

        if (GameManager.settings.ThemeIsDark)
            ThemeManager.instance.ChangeTheme();
        GameStats.MenuController = true;
        ChangeControl.instance.ChangeController(GameManager.settings.Control);
       SettingArrow.instance.CheckValueSlider();
        GameManager.settings.needSave = false;


        GameManager.playerStats = GameManager.LoadFromPlayerPrefs<PlayerStats>("PlayerStats");
        if (GameManager.playerStats == null)
            GameManager.playerStats = new PlayerStats();
        ChangeTextValue.instance.hightScore.text = GameManager.playerStats.hightScore+"";
    }
}
