using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shoot : MonoBehaviour
{
    public static Shoot instance;
    public static int damage;
    [SerializeField] Animation _shootAnim;
    [SerializeField] GameObject[] _bulletsPool;
    [SerializeField] GameObject _shootPosition;
    [SerializeField] AudioSource _shootSound;
    [SerializeField] AudioClip _shoot;
    [SerializeField] Image _fillImage;
    //[SerializeField] ParticleSystem particleSystemShoot;
    Vector2[] _velocites;
    bool _isCooldown = true;
    Coroutine _cooldown;
    private void Awake()
    {
        instance = this;
    }
    public void Shooting()
    {

        if (_isCooldown)
        {
            for (int i = 0; i < GameManager.upgrades.ballCount; i++)
            {
                if (!_bulletsPool[i].activeSelf)
                {
                    _bulletsPool[i].GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                    _bulletsPool[i].GetComponent<Rigidbody2D>().angularVelocity = 0f;
                    _bulletsPool[i].transform.position = _shootPosition.transform.position;
                    _bulletsPool[i].SetActive(true);
                    _bulletsPool[i].GetComponent<Rigidbody2D>().AddForce(new Vector2(Mathf.Cos(gameObject.transform.rotation.eulerAngles.z * Mathf.Deg2Rad) * GameManager.upgrades.bulletSpeed,
                        Mathf.Sin(gameObject.transform.eulerAngles.z * Mathf.Deg2Rad) * GameManager.upgrades.bulletSpeed), ForceMode2D.Impulse);
                    if (_shootAnim.isPlaying)
                        _shootAnim.Stop();
                    _shootAnim.Play();
                    _shootSound.PlayOneShot(_shoot);
                    //particleSystemShoot.Play();
                    break;
                }
            }
            _cooldown = StartCoroutine(StartCooldown());
        }
    }

    IEnumerator StartCooldown()
    {
        _isCooldown = false;
        float cooldown = 0f;
        while (cooldown < GameManager.upgrades.shootCooldown)
        {
            yield return new WaitForSeconds(0.1f);
            if (!GameManager.Pause)
            {
                _fillImage.fillAmount += 0.1f/ GameManager.upgrades.shootCooldown;
                cooldown += 0.1f;
            }
        }
        _isCooldown = true;
        _fillImage.fillAmount = 0;
    }

    public void InstantRecharge()
    {
        if (_cooldown != null)
            StopCoroutine(_cooldown);
        _isCooldown = true;
        _fillImage.fillAmount = 0;
    }

    public void DeletePoolFromScene()
    {
        for (int i = 0; i < _bulletsPool.Length; i++)
        {
            if (_bulletsPool[i].activeSelf)
            {
                _bulletsPool[i].SetActive(false);
            }
        }

    }

    public void Pause(bool pause)
    {
        if (pause)
        {
            _velocites = new Vector2[_bulletsPool.Length];
            for (int i = 0; i < _bulletsPool.Length; i++)
            {
                _velocites[i] = _bulletsPool[i].GetComponent<Rigidbody2D>().velocity;
                _bulletsPool[i].GetComponent<Rigidbody2D>().velocity = Vector3.zero;

            }

        }
        else
        {
            for (int i = 0; i < _bulletsPool.Length; i++)
                _bulletsPool[i].GetComponent<Rigidbody2D>().velocity = _velocites[i];
        }
    }
}
