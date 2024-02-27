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

    [Header("Set Dynamically")]
    public int dirHeld = -1;
    public float stamina;
    public bool running = false;

    private Rigidbody2D rigid;

    private Coroutine recharge;
   
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

        if (Input.GetKeyDown("left shift")) {
            running = true;
        } else if (Input.GetKeyUp("left shift")) {
            running = false;
        }

        if (running && (stamina != 0)) {
            rigid.velocity = vel * speed * runSpeedX;

            stamina -= runCost * Time.deltaTime;
            if (stamina < 0) stamina = 0;
            StaminaBar.fillAmount = stamina / maxStamina;

            if (recharge != null) StopCoroutine(recharge);
            recharge = StartCoroutine(RechargeStamina());

        } else {
            rigid.velocity = vel * speed;
        }
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
