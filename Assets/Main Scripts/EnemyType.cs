using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Enemy")]
public class EnemyType: ScriptableObject
{
    public Enemy.typeEnemy type;
    public float speed;
    public int hp;
}
