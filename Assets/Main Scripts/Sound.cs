using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sound : MonoBehaviour
{
    public Sprite SoundSprite;
    public Sprite SoundOffSprite;
    public Image sound;
    GameObject _soundSlider;
    public static Sound instance;


    private void Awake()
    {

        instance = this;
        _soundSlider = gameObject;
    }


    public void ChangeSound(){
        SwapSlider();
        if (Camera.main.GetComponent<AudioSource>().volume > 0)
        {
            Camera.main.GetComponent<AudioSource>().volume = 0;
            GameManager.settings.enableSound = false;
        }
        else
        {
            GameManager.settings.enableSound = true ;
            Camera.main.GetComponent<AudioSource>().volume = 1;
        }
        GameManager.settings.needSave = true;
    }

    private void SwapSlider()
    {
        _soundSlider.transform.localScale = -_soundSlider.transform.localScale;
        if (_soundSlider.transform.localScale.x < 0)
        {
            sound.sprite = SoundOffSprite;
        }
        else
        {
            sound.sprite = SoundSprite;
        }
    }
}
