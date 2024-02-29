using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Set in Inspector")]
    public GameObject BossCar;

    private Camera cam;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        DestroyWhenOffScreen();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject go = other.gameObject;
        Destroy(gameObject);
        if (go.tag.Equals("BossTire"))
        {
            Destroy(go);
        }
        if (go.tag.Equals("Zombie"))
        {
            Destroy(go);
        }
    }

    private void DestroyWhenOffScreen()
    {
        Vector2 screenPosition = cam.WorldToScreenPoint(transform.position);

        if (screenPosition.x < 0 ||
            screenPosition.x > cam.pixelWidth ||
            screenPosition.y < 0 ||
            screenPosition.y > cam.pixelHeight)
        {
            Destroy(gameObject, 1);
        }
    }
}
