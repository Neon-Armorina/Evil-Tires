using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EvilTires : MonoBehaviour
{
    public static int tiresCount = 4;

    void Update()
    {
        if (tiresCount == 0)
        {
            Destroy(this.gameObject);
            enabled = false;
        }
    }
}
