using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Upgrade : MonoBehaviour
{
    public static Upgrade instance;
    [SerializeField] TextMeshProUGUI[] _ballCountText;
    [SerializeField] TextMeshProUGUI[] _rotateTimeText;
    [SerializeField] TextMeshProUGUI[] _shootCooldownText;
    [SerializeField] TextMeshProUGUI[] _bulletSpeedText;
    [SerializeField] TextMeshProUGUI[] _lifesText;
    [SerializeField] GameObject[] Money;

    int[] _cost = new int[] { 30, 10, 10, 100, 100 };
    int[] _costMultiply = new int[] { 30, 10, 10, 100, 100 };
    float[] _upgradeValue = new float[] {1, -0.1f, -0.1f, 0.5f, 1 };
    int[] _costValue = new int[] { 30, 10, 10, 100, 100 };

    public int[] MaxUpgrade = new int[] { 17, 20, 25, 4, 5 };
    private void Awake()
    {
        instance = this;
       
        
    }
    public void UpdateText()
    {
        _ballCountText[1].text = GameManager.upgrades.ballCount+"";
        _rotateTimeText[1].text = GameManager.upgrades.rotateTime+"";
        _shootCooldownText[1].text = GameManager.upgrades.shootCooldown+"";
        _bulletSpeedText[1].text = GameManager.upgrades.bulletSpeed + "";
        _lifesText[1].text = GameManager.upgrades.lifes + "";
        for(int i =0; i < _upgradeValue.Length; i++)
        {
            _costValue[i] = _cost[i] + _costMultiply[i] * GameManager.upgrades.upgradeCount[i];
        }
        _ballCountText[3].text = _costValue[0] + "";
        _rotateTimeText[3].text = _costValue[1] + "";
        _shootCooldownText[3].text = _costValue[2] + "";
        _bulletSpeedText[3].text = _costValue[3] + "";
        _lifesText[3].text = _costValue[4] + "";
        for (int i = 0; i < GameManager.upgrades.isMaxUpgrade.Length; i++)
        {
            if (GameManager.upgrades.isMaxUpgrade[i])
            {
                Money[i].SetActive(false);
                switch (i)
                {
                    case 0:
                        _ballCountText[2].text = "";
                        _ballCountText[3].text = "MAX";
                        break;
                    case 1:
                        _rotateTimeText[2].text = "";
                        _rotateTimeText[3].text = "MAX";
                        break;
                    case 2:
                        _shootCooldownText[2].text = "";
                        _shootCooldownText[3].text = "MAX";
                        break;
                    case 3:
                        _bulletSpeedText[2].text = "";
                        _bulletSpeedText[3].text = "MAX";
                        break;
                    case 4:
                        _lifesText[2].text = "";
                        _lifesText[3].text = "MAX";
                        break;
                }
                
            }
        }
    }

    public void BuyUpgrade(int i)
    {
        if (GameManager.playerStats.Money >= _costValue[i])
        {
            var isBuy = !GameManager.upgrades.isMaxUpgrade[i];
           
            if (isBuy)
            {
                switch (i)
                {
                case 0:
                    BallCount();
                    break;
                case 1:
                    RotateTime();
                    break;
                case 2:
                    ShootCooldown();
                    break;
                case 3:
                        BulletSpeed();
                        
                    break;
                case 4:
                        Lifes();
                        break;
                default:
                    Debug.LogError("Try to buy not exist upgrade");
                    break;
                }
                GameManager.upgrades.upgradeCount[i]++;
                GameManager.playerStats.Money -= _costValue[i];
                ChangeTextValue.instance.money.text = GameManager.playerStats.Money + "";
                ChangeTextValue.instance.MenuMoney.text = ChangeTextValue.instance.money.text;
                if (GameManager.upgrades.upgradeCount[i] == MaxUpgrade[i])
                    GameManager.upgrades.isMaxUpgrade[i] = true;
                UpdateText();
            }
        }
    }

    private void BallCount()
    {
        GameManager.upgrades.ballCount += (int)_upgradeValue[0];
        Analytic.instance.BuyShopUpgrade("Ball_count");
    }


    private void RotateTime()
    {
        GameManager.upgrades.rotateTime += _upgradeValue[1];
        GameManager.upgrades.rotateTime= (float)Math.Round(((double)GameManager.upgrades.rotateTime), 1);
        Analytic.instance.BuyShopUpgrade("Rotate_time");
    }


    private void ShootCooldown()
    {
        GameManager.upgrades.shootCooldown += _upgradeValue[2];
        GameManager.upgrades.shootCooldown = (float)Math.Round(((double)GameManager.upgrades.shootCooldown), 1);
        Analytic.instance.BuyShopUpgrade("Shoot_cooldown");
    }

    private void BulletSpeed()
    {
        GameManager.upgrades.bulletSpeed += _upgradeValue[3];
        GameManager.upgrades.bulletSpeed = (float)Math.Round(((double)GameManager.upgrades.bulletSpeed), 1);
        Analytic.instance.BuyShopUpgrade("Bullet_speed");
    }
    private void Lifes()
    {
        GameManager.upgrades.lifes += (int)_upgradeValue[4];
        Analytic.instance.BuyShopUpgrade("Lifes");
    }


  
}


[SerializeField]
public class Upgrades
{
    public int ballCount =3 ;
    public float rotateTime = 2f;
    public float shootCooldown = 3f;
    public int lifes = 1;
    public float bulletSpeed = 0.5f; 
    public int[] upgradeCount = new int[] { 0, 0, 0, 0, 0 };
    public bool[] isMaxUpgrade = new bool[] { false, false,false,false,false, };
}
