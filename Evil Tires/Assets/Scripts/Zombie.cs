using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    private Rigidbody2D rigid;
    private Policeman policeman;
    private float speed;

    public float BaseSpeed;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        speed = BaseSpeed * (1 + Random.Range(-0.1f, 0.1f));
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
    }
}
