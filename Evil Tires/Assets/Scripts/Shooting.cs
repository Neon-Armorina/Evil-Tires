using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [Header("Set in Inspector")]
    public Transform firePoint;
    public GameObject projectilePrefab;
    public float projectileSpeed = 8f;
    public AudioSource audioShoot;
    public GameObject fireFlash;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
            audioShoot.Play();
        }
    }

    void Shoot()
    {
        fireFlash.SetActive(true);
        Invoke("light_Disabler", 0.1f);
        GameObject projGO = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rigid = projGO.GetComponent<Rigidbody2D>();
        rigid.AddForce(firePoint.up * projectileSpeed, ForceMode2D.Impulse);
    }

    void light_Disabler()
    {
        fireFlash.SetActive(false);
    }
}
