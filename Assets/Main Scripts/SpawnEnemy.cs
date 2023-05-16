using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
     float _spawnSpeed = 3.5f;
    [SerializeField] GameObject _shootgun;
    [SerializeField] GameObject[] _enemys;
    [SerializeField] GameObject[] _spawnPoints;
    int _lastColor=0;
    int[][] _chanses = new int[][] { new int[]{ 100, 0, 0, 0 }, new int[]{ 70,30,0,0 }, new int[] {50, 30,20,0 }, new int[] {30,30,30,10 }, new int[] {20,30,30,20 } };
    int[] _killingEnemy = new int[] {0,20,50,100,150 };
    Color[] _colors = new Color[] { new Color32(0x64, 0xC8, 0xF9, 0xFF),
        new Color32(0xF9, 0xBA, 0x64, 0xFF), 
        new Color32(0xF9, 0x70, 0x64, 0xFF), 
        new Color32(0xD1, 0x4A, 0xC2, 0xFF),
        new Color32(0x9A, 0x71, 0xD7, 0xFF), };
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnInSec());
    }

    IEnumerator SpawnInSec()
    {
        while (true) {
            if(!GameManager.Pause)
            Spawn();
            yield return new WaitForSeconds(_spawnSpeed);
        }
    }


    void Spawn()
    {
        
        int j = Random.Range(1, 101);
        int step = 0;
        for(int f =0; f < _killingEnemy.Length; f++)
        {
            if (step<f )
            {
                if (SpinGun.EnemyKilling >= _killingEnemy[f])
                {
                    step = f;
                    _spawnSpeed -= 0.75f;
                }
                else break;
            }
            
        }
        for(int f=0; f < _chanses[step].Length; f++)
        {
            if (j > _chanses[step][f])
            {
                j -= _chanses[step][f];
            }
            else
            {
                j = f;
                break;
            }
        }
        var enemy = Instantiate(_enemys[j]);
        int g = 1;
        while (g == _lastColor)
        {
            g = Random.Range(0, _colors.Length);
        }
        _lastColor = g;
        var Enemy = enemy.GetComponent<Enemy>();
        enemy.GetComponent<SpriteRenderer>().color = _colors[g];
        Enemy.particleSystem.startColor = _colors[g];
        int i = Random.Range(0,360);
        
        if(Enemy.type.type == Enemy.typeEnemy.Triangle || enemy.GetComponent<Enemy>().type.type == Enemy.typeEnemy.SquareWithShield)
        {
            enemy.transform.Rotate(0,0, (i-2)*90);
        }
        else if(Enemy.type.type == Enemy.typeEnemy.Square)
        {
            int angle = Random.Range(0, 90);
            enemy.transform.Rotate(0, 0, angle);
        }
        Enemy.Init(i,_spawnPoints, _shootgun);
    }
}
