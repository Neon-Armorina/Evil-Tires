using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Policeman : MonoBehaviour
{
    [Header("Set in Inspector")]
    public float speed = 5;
    public float runSpeedX = 2;
    public float runCost;
    public float maxStamina;
    public Image StaminaBar;
    public float chargeRate;
    public Camera cam;

    [Header("Set Dynamically")]
    public int dirHeld = -1;
    public float stamina;
    public bool running = false;

    private Rigidbody2D rigid;
    private Coroutine recharge;

    Vector2 movement, mousePos;

    private Vector2[] directions = new Vector2[] {
        Vector2.right, Vector2.up, Vector2.left, Vector2.down };

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

        if (Input.GetKeyDown("left shift")) {
            running = true;
        } else if (Input.GetKeyUp("left shift")) {
            running = false;
        }

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    void FixedUpdate()
    {
        Vector2 vel = Vector2.zero;
        if (dirHeld > -1) vel = directions[dirHeld];

        if (running && (stamina != 0) && (dirHeld > -1))
        {
            rigid.velocity = vel * speed * runSpeedX;

            stamina -= runCost * Time.deltaTime;
            if (stamina < 0) stamina = 0;
            StaminaBar.fillAmount = stamina / maxStamina;

            if (recharge != null) StopCoroutine(recharge);
            recharge = StartCoroutine(RechargeStamina());

        }
        else
        {
            rigid.velocity = vel * speed;
        }
        Vector2 lookDir = mousePos - rigid.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rigid.rotation = angle;
    }

    private IEnumerator RechargeStamina()
    {
        yield return new WaitForSeconds(3f);

        while(stamina < maxStamina) {
            stamina += chargeRate / 10f;
            if (stamina > maxStamina) stamina = maxStamina;
            StaminaBar.fillAmount = stamina / maxStamina;
            yield return new WaitForSeconds(.1f);
        }
    }
}
