using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EvilTires : MonoBehaviour
{

    public TextMeshProUGUI objText;
    public static int tiresCount = 4;

    void Update()
    {
        if (tiresCount == 0)
        {
            objText.transform.parent.gameObject.SetActive(false);
            Destroy(this.gameObject);
            enabled = false;
        }
    }
}
