using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public static Shoot instance;
    public static int damage;
    [SerializeField] Animation _shootAnim;
    [SerializeField] GameObject[] _bulletsPool;
    [SerializeField] GameObject _shootPosition;
    [SerializeField] float _speedBullet;
    Vector2[] _velocites;

    private void Start()
    {
        instance = this;
    }
    public void Shooting()
    {
        if (_shootAnim.isPlaying)
            _shootAnim.Stop();
        _shootAnim.Play();
        Debug.Log(gameObject.transform.rotation.eulerAngles.z);
        for(int i=0; i < _bulletsPool.Length; i++)
        {
            if (!_bulletsPool[i].activeSelf)
            {
                _bulletsPool[i].GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                _bulletsPool[i].GetComponent<Rigidbody2D>().angularVelocity =0f;
                _bulletsPool[i].transform.position = _shootPosition.transform.position;
                _bulletsPool[i].SetActive(true);
                _bulletsPool[i].GetComponent<Rigidbody2D>().AddForce(new Vector2(Mathf.Cos(gameObject.transform.rotation.eulerAngles.z*Mathf.Deg2Rad) *_speedBullet, Mathf.Sin(gameObject.transform.eulerAngles.z* Mathf.Deg2Rad) *_speedBullet), ForceMode2D.Impulse);
                break;
            }
        }
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
