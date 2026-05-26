using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whip : Weapon
{
    List<GameObject> hitEnemies = new List<GameObject>();

    void OnEnable()
    {
        hitEnemies.Clear();
        StartCoroutine(StartDestroy());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
      
        if (collision.gameObject.layer == 6)
        {
            GameObject enemyObj = collision.gameObject;

            if (!hitEnemies.Contains(enemyObj))
            {
                hitEnemies.Add(enemyObj);

               
                int damage = RandomDamage(attackPower);
                enemyObj.GetComponent<Enemy>().ReduceHealthPoint(damage);

           
                Player.GetInstance().Heal(100);
            }
        }
    }
}