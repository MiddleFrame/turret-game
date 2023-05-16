using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(SpriteRenderer))]
public class Enemy : MonoBehaviour
{
    public enum typeEnemy { Square, Triangle, Hexagon, SquareWithShield }
    GameObject _shootGun;
    public EnemyType type;
    GameObject _moneyText;
    [SerializeField] GameObject _money;
    private Vector3 _startPosition;
    int _hp;
    float selfspeed = 0.5f;
    //Number of spawn point
    int _i;
   GameObject[] _spawnPoints;
    float _dist;
    public new ParticleSystem particleSystem;
    float _way=0;
    // Start is called before the first frame update
    public void Init(int i, GameObject[] spawnPoints, GameObject shootGun)
    {
        this._i = i;
        this._hp = type.hp;
        _shootGun = shootGun;
        _spawnPoints = spawnPoints;
        _moneyText = GameManager.instance.mainMoney;
        transform.position = new Vector3(4*Mathf.Sin(i),6*Mathf.Cos(i),0);
        _startPosition = transform.position;
        _dist = (transform.position - _shootGun.transform.position).magnitude;
        if (type.type != Enemy.typeEnemy.SquareWithShield) return;
        if (Random.Range(0, 2) == 1) return;
        var shields =  gameObject.GetComponentsInChildren<Transform>();
        shields[shields.Length - 1].gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Pause)
        {
            _way += Time.deltaTime * type.speed * selfspeed;
            transform.position = Vector3.Lerp(_startPosition, _shootGun.transform.position, _way / _dist);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet")) return;
        _hp--;
        if (_hp == 0)
        {
            giveMoney = true;
            if (type.type== typeEnemy.Hexagon)
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
    bool giveMoney = true;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;
        giveMoney = false;
        gameObject.GetComponent<Collider2D>().enabled = false;
        GetComponent<Animation>().Play("Death");

    }
    public void Death()
    {
        SpinGun.Gun.RemoveEnemy(gameObject);
        particleSystem.Play();
        if (giveMoney)
        {
            var variant = Random.Range(0, 101);
            if (variant < type.chanseMoney)
            {

                StartCoroutine(GiveMoney());
            }
            else
            {
                StartCoroutine(UseParticle());

            }
        }
    }
    IEnumerator GiveMoney()
    {
        var money = Instantiate(_money);
        money.transform.position = this.transform.position;
        float totalMovementTime = 2f; //the amount of time you want the movement to take
        float currentMovementTime = 0f;
        while (Vector3.SqrMagnitude(_moneyText.transform.position - money.transform.position) > .1f)
        {
            currentMovementTime += Time.deltaTime;
            money.transform.position = Vector3.Lerp(money.transform.position, _moneyText.transform.position, currentMovementTime / totalMovementTime);
            yield return new WaitForSeconds(.01f);
        }
        Destroy(money);
        yield return new WaitForSeconds(particleSystem.main.duration);
        ChangeMoney();
    }
    IEnumerator UseParticle()
    {
        yield return new WaitForSeconds(particleSystem.main.duration*2);
        Destroy(gameObject);
    }

    public void ChangeMoney()
    {
        GameManager.playerStats.Money += type.moneyCount;
        ChangeTextValue.instance.money.text = GameManager.playerStats.Money+"";
        Destroy(gameObject);
    }
}
