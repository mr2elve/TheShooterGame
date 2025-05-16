using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed, LifeTime;
    public Rigidbody therigidbody;
    public int damage;
    public bool damageEnemy, damagePlayer;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        therigidbody.linearVelocity = transform.forward * bulletSpeed;

        LifeTime -= Time.deltaTime;
        if (LifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && damageEnemy)
        {
            other.gameObject.GetComponent<EnemyHealth>().DamageEnemy(damage);
        }
        if (other.gameObject.tag == "Head" && damageEnemy)
        {
            other.transform.parent.gameObject.GetComponent<EnemyHealth>().DamageEnemy(damage * 2);
        }
        if (other.gameObject.tag == "Player" && damagePlayer)
        {
            PlayerHealth.instance.DamagePlayer(damage);
        }
        Destroy(gameObject);
    }
}