using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class SpinGun : MonoBehaviour
{
    public static SpinGun Gun { get; private set; }
    public static GameObject Player;
    public static int EnemyKilling = 0;
    private bool target = false;

    [SerializeField]
    GameObject _centerSpin;

    [SerializeField]
    GameObject _backGroundBlur;

    Coroutine anim;
    int _side;

    [SerializeField]
    GameObject[] _lifes;

    [SerializeField]
    Color _shootgunColor;

    [SerializeField]
    private List<GameObject> _enemies = new List<GameObject>();

    public int side
    {
        get { return _side; }
        set { _side = (value % 4) + (value >= 0 ? 0 : 4); }
    }

    // Start is called before the first frame update
    void Awake()
    {
        Player = this.gameObject;
        Gun = this;
    }

    void Start()
    {
        side = 1;
        StartCoroutine(SpinAnim());
    }


    IEnumerator SpinAnim()
    {
        int direction = 1;
        Quaternion angle = new Quaternion();
        while (true)
        {
            if (GameManager.upgrades.rotateTime > 0 && _enemies.Count > 0 && !GameManager.Pause)
            {
                if (!target)
                {
                    var rotation = transform.rotation;
                    angle = Quaternion.Euler(rotation.eulerAngles.x,
                        rotation.eulerAngles.y,
                        Mathf.Atan2(_enemies[0].transform.position.y, _enemies[0].transform.position.x) *
                        Mathf.Rad2Deg);
                    if (rotation.eulerAngles.z >= 180 && angle.eulerAngles.z >= 180)
                    {
                        direction = rotation.eulerAngles.z > angle.eulerAngles.z ? -1 : 1;
                    }
                    else if (rotation.eulerAngles.z < 180 && angle.eulerAngles.z < 180)
                    {
                        direction = rotation.eulerAngles.z > angle.eulerAngles.z ? -1 : 1;
                    }
                    else if (rotation.eulerAngles.z >= 180 && angle.eulerAngles.z < 180)
                    {
                        direction = 360-rotation.eulerAngles.z > angle.eulerAngles.z ? 1 : -1;
                    }
                    else if (rotation.eulerAngles.z < 180 && angle.eulerAngles.z >= 180)
                    {
                        direction = rotation.eulerAngles.z > 360-angle.eulerAngles.z ? -1 : 1;
                    }

                    target = true;
                    Debug.Log("New target:\n " + "direction " + direction + "\n angle " + angle.eulerAngles.z +"\n gun angle "+rotation.eulerAngles.z + "\n enemy " +
                              _enemies[0]);
                }


                if (Mathf.Abs(angle.eulerAngles.z - transform.rotation.eulerAngles.z) > 1.1f)
                {
                    Player.transform.RotateAround(_centerSpin.transform.position,
                        new Vector3(0, 0, direction), 1);
                }
                else
                {
                    Shoot.instance.Shooting();
                }

                yield return new WaitForSeconds(GameManager.upgrades.rotateTime / 90);
            }
            else if (GameManager.upgrades.rotateTime <= 0)
            {
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x,
                    transform.rotation.eulerAngles.y,
                    Mathf.Atan2(_enemies[0].transform.position.y, _enemies[0].transform.position.x) * Mathf.Rad2Deg);
            }

            yield return null;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Shield"))
        {
            GameManager.playerStats.life--;
            if (GameManager.playerStats.life > 1)
                _lifes[GameManager.playerStats.life].GetComponent<Image>().color =
                    ThemeManager.instance.CurrentTheme.MiddlePrey;
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
                if (GameManager.playerStats.life >= i)
                    _lifes[i].GetComponent<Image>().color = _shootgunColor;
                else
                    _lifes[i].GetComponent<Image>().color = ThemeManager.instance.CurrentTheme.MiddlePrey;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            _enemies.Add(col.gameObject);
        }
    }

    public void RemoveEnemy(GameObject enemy)
    {
        target = false;
        _enemies.Remove(enemy);
    }

    public void RemoveEnemies()
    {
        target = false;
        _enemies.Clear();
    }
}