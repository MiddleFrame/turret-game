using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public static class GameStats
{
    public static int PointInGame =0;
    public static bool MenuController = false;
    

    public static void NewGame()
    {
        SpinGun.EnemyKilling = 0;
        PointInGame = 0;
        ChangeTextValue.instance.UpdateScore();
        
        var enemy = Object.FindObjectsOfType<Enemy>();
        for(int i =0;i<enemy.Length;  i++)
        Object.Destroy(enemy[i].gameObject);
        Shoot.instance.DeletePoolFromScene();

        GameManager.instance.Paused();


        Analytic.instance.StartNewGame();
    }

    public static void LostGame(GameObject BackGroundBlur)
    {
        GameManager.instance.Paused(true);
        BackGroundBlur.SetActive(true);
        if (PointInGame > GameManager.playerStats.hightScore)
        {
            GameManager.playerStats.hightScore = PointInGame;
            ChangeTextValue.instance.UpdateScore(true);
        }
    }

    public static void ContinueGame()
    {
        var enemy = Object.FindObjectsOfType<Enemy>();
        for (int i = 0; i < enemy.Length; i++)
            Object.Destroy(enemy[i].gameObject);
        Shoot.instance.DeletePoolFromScene();
        GameManager.instance.Paused();
        Analytic.instance.GameContinue(PointInGame);
    } 
}


public class PlayerStats
{
    public int hightScore = 0;
}