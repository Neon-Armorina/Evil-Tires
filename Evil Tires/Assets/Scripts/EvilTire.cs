using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilTire : EvilTires
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject go = collision.gameObject;
        if (go.tag.Equals("Projectile"))
        {
            Destroy(go);
            tiresCount--;
        }
    }
}
