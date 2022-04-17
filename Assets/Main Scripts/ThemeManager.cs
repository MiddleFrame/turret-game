using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ThemeManager : MonoBehaviour
{
    public List<Graphic> MiddlePreyElements;
    public List<Graphic> lightPreyElements;
    public List<Graphic> DarkPreyElements;
    public List<Graphic> DarkElements;
    public Theme[] themes;
    int _theme =0;
    public Theme CurrentTheme { get; set; } 

    public GameObject ButtonTheme;
    public static ThemeManager instance;

    private void Awake()
    {
        instance = this;
        CurrentTheme = themes[0];
    }
    private void UpdateTheme()
    {
    foreach (var item in MiddlePreyElements)
    {
        if (item != null)
            item.color = CurrentTheme.MiddlePrey;
    }
    foreach (var item in lightPreyElements)
    {
        if (item != null)
            item.color = CurrentTheme.lightPrey;
    }
    foreach (var item in DarkPreyElements)
    {
        if (item != null)
            item.color = CurrentTheme.DarkPrey;
    }
    foreach (var item in DarkElements)
    {
        if (item != null)
            item.color = CurrentTheme.Dark;
    }
        Camera.main.backgroundColor = CurrentTheme.lightPrey;
    }


    public void ChangeTheme()
    {
        Blur.EnableOrDisableBlur(false);
        _theme++;
        Debug.Log("ChangeTheme " + _theme);
        if (_theme >= themes.Length)
        {
            GameManager.settings.ThemeIsDark = false;
            _theme = 0;
        }
        else
        {
            GameManager.settings.ThemeIsDark = true;
        }
        CurrentTheme = themes[_theme];
        UpdateTheme();
        SlideButton();
        ChangeControl.instance.ChangeController(ChangeControl.instance.CurrentControl);
        GameManager.settings.needSave = true;
      /*  if (GameManager.Pause)
        {
            Blur.EnableOrDisableBlur(true);
        }*/
    }

    private void SlideButton()
    {
        ButtonTheme.transform.localScale = -ButtonTheme.transform.localScale;
    }
}



[System.Serializable]
public class Theme
{
    [Space(10)]
    public Color MiddlePrey;
    public Color lightPrey;

    public Color DarkPrey;
    public Color Dark;

}