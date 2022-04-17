using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
public class SpinGun : MonoBehaviour
{
    public static GameObject Player;
    public static int EnemyKilling = 0;
    [SerializeField] GameObject _centerSpin;
    [SerializeField] GameObject _backGroundBlur;
    Coroutine anim;
    int _side;
    [SerializeField] GameObject[] _lifes;
    [SerializeField] Color _shootgunColor;
    public int side { get { return _side; } set {
            _side = (value % 4) + (value>=0?0:4);
        } 
    }
    // Start is called before the first frame update
    void Awake()
    {
        Player = this.gameObject;
    }
    void Start()
    {
        side = 1;
        
    }

    public void Spin(bool right)
    {
        
        if (right)
        {

            side--;
            if(anim!=null)
            StopCoroutine(anim);
            if(GameManager.upgrades.rotateTime>0)
            anim =  StartCoroutine(SpinAnim(-1));
            else
            Player.transform.RotateAround(_centerSpin.transform.position, new Vector3(0, 0, -1),  90);
        }
        else
        {
            side++;
            if (anim != null)
                StopCoroutine(anim);
            if (GameManager.upgrades.rotateTime > 0)
                anim = StartCoroutine(SpinAnim(1));
            else
                Player.transform.RotateAround(_centerSpin.transform.position, new Vector3(0, 0, 1), 90);
        }
        
    }

    IEnumerator SpinAnim(int around)
    {
        
        while (Mathf.Abs(Mathf.Abs(Player.transform.rotation.eulerAngles.z) - side * 90 )> 0.1f)
        {
            if(!GameManager.Pause)
            Player.transform.RotateAround(_centerSpin.transform.position, new Vector3(0, 0, around), 3);
            yield return new WaitForSeconds((float)GameManager.upgrades.rotateTime/30);
        }
       
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Shield")
        {
          
            GameManager.playerStats.life--;
            if(GameManager.playerStats.life>1)
            _lifes[GameManager.playerStats.life].GetComponent<Image>().color = ThemeManager.instance.CurrentTheme.MiddlePrey;
            if (GameManager.playerStats.life <= 0)
            {
                
                ChangeTextValue.instance.MenuMoney.text = ChangeTextValue.instance.money.text;
                Analytic.instance.EndGame(GameStats.PointInGame);
                ChangeTextValue.instance.hightSroreStar.SetActive(false);
                GameStats.LostGame(_backGroundBlur);
            }
        }
    }

    public void UpdateLife()
    {
        if (GameManager.upgrades.lifes > 1)
        {
            for (int i = 0; i < GameManager.upgrades.lifes; i++)
            {
                _lifes[i].SetActive(true);
                if(GameManager.playerStats.life>=i)
                _lifes[i].GetComponent<Image>().color = _shootgunColor;
                else
                    _lifes[i].GetComponent<Image>().color = ThemeManager.instance.CurrentTheme.MiddlePrey; 
            }
        }
    }
}
