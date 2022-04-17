using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ChangeTextValue : MonoBehaviour
{
    TextMeshProUGUI _score;
    [SerializeField] TextMeshProUGUI _menuScore;
    public TextMeshProUGUI hightScore;
    public TextMeshProUGUI money;
    public TextMeshProUGUI MenuMoney;
    public GameObject hightSroreStar;
    public static ChangeTextValue instance;

    private void Awake()
    {
        instance = this;
        _score = gameObject.GetComponent<TextMeshProUGUI>();
    }
    private void Start()
    {
        UpdateMoney();
    }
    public void UpdateScore(bool newHight = false)
    {
        _score.text = GameStats.PointInGame + "";
        _menuScore.text = GameStats.PointInGame + "";
        
        if (newHight)
        {
            hightScore.text = GameStats.PointInGame + "";
            hightSroreStar.SetActive(true);
        }
    }

    public void UpdateMoney()
    {
        money.text = GameManager.playerStats.Money + "";
        MenuMoney.text = money.text;
    }
}
