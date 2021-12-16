using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
public class Enemy : MonoBehaviour
{
    public enum typeEnemy { Square, Triangle, Hexagon, SquareWithShield }
    GameObject _shootGun;
    public EnemyType type;
    int _hp;
    //Number of spawn point
    int _i;
   GameObject[] _spawnPoints;
    float _dist;
    float _way=0;
    // Start is called before the first frame update
    public void Init(int i, GameObject[] spawnPoints, GameObject shootGun)
    {
        this._i = i;
        this._hp = type.hp;
        _shootGun = shootGun;
        _spawnPoints = spawnPoints;
        transform.position = _spawnPoints[i].transform.position;
        _dist = (transform.position - _shootGun.transform.position).magnitude;
        if(type.type == Enemy.typeEnemy.SquareWithShield)
        {
            if(Random.Range(0, 2) == 0)
            {
              var shields =  gameObject.GetComponentsInChildren<Transform>();
                shields[shields.Length - 1].gameObject.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Pause)
        {
            _way += Time.deltaTime * type.speed;
            transform.position = Vector3.Lerp(_spawnPoints[_i].transform.position, _shootGun.transform.position, _way / _dist);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Bullet")
        {
            _hp--;
            if (_hp == 0)
            {
                if(type.type== typeEnemy.Hexagon)
                {
                    GameStats.PointInGame+=2;
                }
                else
                {
                    GameStats.PointInGame++;
                }
                ChangeTextValue.instance.UpdateScore();
               SpinGun.EnemyKilling++;
                gameObject.GetComponent<Collider2D>().enabled = false;
                GetComponent<Animation>().Play("Death");
            }
            else
            {
                GetComponent<Animation>().Play();
            }
        }
    }

    public void Death()
    {
        Destroy(gameObject);
    }
}
