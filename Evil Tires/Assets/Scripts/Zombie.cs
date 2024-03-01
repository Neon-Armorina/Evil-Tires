using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    private Rigidbody2D rigid;
    private Policeman policeman;
    private float speed;
    private float damage;

    public float baseSpeed;
    public AudioClip biteClip;
    public float baseDamage;
    public float volumeBite;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        speed = baseSpeed * (1 + Random.Range(-0.1f, 0.1f));
        damage = baseDamage * (1 + Random.Range(-0.1f, 0.1f));
        policeman = FindObjectOfType<Policeman>();
    }

    void Update()
    {
        if (policeman != null)
        {
            transform.up = (policeman.transform.position - transform.position).normalized;
            rigid.velocity = transform.up * speed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject go = collision.gameObject;
        if (go.tag.Equals("BossCar"))
        {
            Destroy(gameObject);
        }

        if (go.tag.Equals("Policeman"))
        {
            AudioSource.PlayClipAtPoint(biteClip, transform.position, volumeBite);
            policeman.health -= damage;
            policeman.HealthBar.fillAmount = policeman.health / policeman.maxHealth;
            if (policeman.health <= 0) Destroy(go);
        }

    }
}
