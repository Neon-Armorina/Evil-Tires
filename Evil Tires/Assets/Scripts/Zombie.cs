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
        speed = BaseSpeed * (1 + Random.Range(-0.1f, 0.1f));
        policeman = FindObjectOfType<Policeman>();
    }

    //void Update()
    //{
    //    transform.up = 
    //}
}
