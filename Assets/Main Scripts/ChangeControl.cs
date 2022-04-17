using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeControl : MonoBehaviour
{
    [SerializeField] Image[] _rims;
    [SerializeField] GameObject[] _controls;
    //[SerializeField] Slider _slider;
    public static ChangeControl instance;
    [HideInInspector]
    public int CurrentControl;
    private void Awake()
    {
        instance = this;
    }

   
    public void ChangeController(int i)
    {
        Debug.Log("Change Control "+i);
        Blur.EnableOrDisableBlur(false);
        CurrentControl = i;
        GameManager.settings.Control = i;
        for (int j=0; j < _controls.Length; j++) 
        {
            if (j != i)
            {
                _rims[j].color = new Color(_rims[i].color.r, _rims[i].color.g, _rims[i].color.b, 0);
                _controls[j].GetComponent<CanvasGroup>().alpha = 0;
                _controls[j].GetComponent<CanvasGroup>().blocksRaycasts = false;
            }
            else
            {
                _rims[j].color = new Color(_rims[i].color.r, _rims[i].color.g, _rims[i].color.b, 256);
                _controls[j].GetComponent<CanvasGroup>().alpha = 1;
                _controls[j].GetComponent<CanvasGroup>().blocksRaycasts = true;
            }
        }
        SettingArrow.instance.CheckValueSlider();
        if(i == 2 && !GameStats.MenuController)
        {
            SettingArrow.instance.ShootArea.GetComponent<Image>().color = new Color(_rims[i].color.r, _rims[i].color.g, _rims[i].color.b, GameManager.Pause?1:0);
        }
        GameManager.settings.needSave = true;
        GameStats.MenuController = false;
        if (GameManager.Pause)
        {
            Blur.EnableOrDisableBlur(true);
        }
    }
}

[SerializeField]
public class Settings
{
    public bool needSave = false;
    public float firstControlValue;
    public float secondControlValue;
    public float thirdControlValue;
    public bool ThemeIsDark=true;
    public bool enableSound = true;
    public int Control;

    public Settings(float firstControl =260f, float secondControl= -900f, float thirdControl=0f)
    {
        firstControlValue = firstControl;
        secondControlValue = secondControl;
       thirdControlValue= thirdControl;
        Control = ChangeControl.instance.CurrentControl;
    }
}