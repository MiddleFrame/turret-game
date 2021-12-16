using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingArrow : MonoBehaviour
{
    [SerializeField] Slider slider;
    float[] _maxValue = new float[] {500, 1000, 800 };
    float[] _minValue = new float[] { 100, -1000, 0 };
    public static SettingArrow instance;

    [SerializeField] GameObject ShootButton;
    public GameObject ShootArea;
    private void Awake()
    {
        instance = this;
        
    }
    public void CheckValueSlider()
    {
        float value;
        switch (ChangeControl.instance.CurrentControl)
        {
            case 0:
                value = GameManager.settings.firstControlValue;
                break;
            case 1:
                value = GameManager.settings.secondControlValue;
                break;
            case 2:
                value = GameManager.settings.thirdControlValue;
                break;
            default:
                value = 0;
                break;
        }
        if (value > slider.maxValue || value < slider.minValue)
        {
            slider.minValue = _minValue[ChangeControl.instance.CurrentControl];
            slider.maxValue = _maxValue[ChangeControl.instance.CurrentControl];

        }
        slider.value = value;
        slider.minValue = _minValue[ChangeControl.instance.CurrentControl];
        slider.maxValue = _maxValue[ChangeControl.instance.CurrentControl];
    }
    public void OnChanged()
    {
        switch (ChangeControl.instance.CurrentControl)
        {
            case 0:
                ShootButton.GetComponent<RectTransform>().sizeDelta = new Vector2(slider.value,slider.value);
                GameManager.settings.firstControlValue = slider.value;
                break;
            case 1:
                gameObject.transform.localPosition = new Vector3(0, slider.value, 0);
                GameManager.settings.secondControlValue = slider.value;
                break;
            case 2:
                ShootArea.GetComponent<RectTransform>().sizeDelta = new Vector2( slider.value,0);
                GameManager.settings.thirdControlValue = slider.value;
                break;
            default:
                break;
        }
        GameManager.settings.needSave = true;
    }
}
