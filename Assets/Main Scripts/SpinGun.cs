using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SpinGun : MonoBehaviour
{
    public static GameObject Player;
    public static int EnemyKilling = 0;
    [SerializeField] GameObject _centerSpin;
    [SerializeField] GameObject _backGroundBlur;
    Coroutine anim;
    int _side;
    int side { get { return _side; } set {
            _side = (value % 4) + (value>=0?0:4);
        } 
    }
    // Start is called before the first frame update
    void Start()
    {
        side = 0;
        Player = this.gameObject;
    }

    public void Spin(bool right)
    {
        if (right)
        {

            side--;
            if(anim!=null)
            StopCoroutine(anim);
            //anim =  StartCoroutine(SpinAnim(-1));
            Player.transform.RotateAround(_centerSpin.transform.position, new Vector3(0, 0, -1),  90);
        }
        else
        {
            side++;
            if (anim != null)
                StopCoroutine(anim);
            // anim = StartCoroutine(SpinAnim(1));
            Player.transform.RotateAround(_centerSpin.transform.position, new Vector3(0, 0, 1), 90);
        }
        
    }

    IEnumerator SpinAnim(int around)
    {
        while (Mathf.Abs(Mathf.Abs(Player.transform.rotation.eulerAngles.z) - side * 90 )> 0.1f)
        {
           
            Player.transform.RotateAround(_centerSpin.transform.position, new Vector3(0, 0, around), 3);
            yield return new WaitForEndOfFrame();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Shield")
        {
            Analytic.instance.EndGame(GameStats.PointInGame);
            ChangeTextValue.instance.hightSroreStar.SetActive(false);
            GameStats.LostGame(_backGroundBlur);
        }
    }
}
