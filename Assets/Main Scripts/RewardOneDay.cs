using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardOneDay : MonoBehaviour
{
   public void GiveReward()
    {
        GameManager.playerStats.Money += 10;
        ChangeTextValue.instance.money.text = GameManager.playerStats.Money + "";

    }
}
