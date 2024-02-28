using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [Header("Set in Inspector")]
    public Transform firePoint;
    public GameObject projectilePrefab;
    public float projectileSpeed = 8f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();        }
    }

    void Shoot()
    {
        GameObject projGO = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rigid = projGO.GetComponent<Rigidbody2D>();
        rigid.AddForce(firePoint.up * projectileSpeed, ForceMode2D.Impulse);
    }
}
