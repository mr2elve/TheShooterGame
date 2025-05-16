using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int currentHealth;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DamageEnemy(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            GameManager.instance.killedEnemies++;
            /*GameManager.instance.PlayerDeath();*/
            UI.instance.killedEnemiesText.text = "Killed Enemies: " + GameManager.instance.killedEnemies;
        }
    }
}