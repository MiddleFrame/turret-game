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
    public GameObject hightSroreStar;
    public static ChangeTextValue instance;

    private void Awake()
    {
        instance = this;
        _score = gameObject.GetComponent<TextMeshProUGUI>();
    }
    private void Start()
    {
        
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
}
