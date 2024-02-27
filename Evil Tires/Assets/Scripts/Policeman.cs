using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Policeman : MonoBehaviour
{
    [Header("Set in Inspector")]
    public float speed = 5;

    [Header("Set Dynamically")]
    public int dirHeld = -1;

    private Rigidbody2D rigid;
   
    private Vector3[] directions = new Vector3[] {
        Vector3.right, Vector3.up, Vector3.left, Vector3.down };

    private KeyCode[] keys = new KeyCode[] { KeyCode.D,
        KeyCode.W, KeyCode.A, KeyCode.S };

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        dirHeld = -1;
        for (int i = 0; i < 4; i++) {
            if (Input.GetKey(keys[i])) dirHeld = i;
        }

        Vector3 vel = Vector3.zero;
        if (dirHeld > -1) vel = directions[dirHeld];

        rigid.velocity = vel * speed;
    }
}
