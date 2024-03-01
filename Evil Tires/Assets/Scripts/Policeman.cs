using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
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
    public Image HealthBar;
    public float regenRate;
    public float maxHealth;
    public Camera cam;
    public GameObject BossCar;
    public GameObject Car;

    [Header("Set Dynamically")]
    public float stamina;
    public float health;
    public bool running = false;
    public bool tireCarrying = false;

    private Rigidbody2D rigid;
    private Coroutine recharge;
    private Coroutine regen;
    private Vector2 moveDirection;
    private int tireNumber = 0;

    Vector2 mousePos;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //dirHeld = -1;
        //for (int i = 0; i < 4; i++) {
        //    if (Input.GetKey(keys[i])) dirHeld = i;
        //}

        if (Input.GetKeyDown("left shift"))
        {
            running = true;
        }
        else if (Input.GetKeyUp("left shift"))
        {
            running = false;
        }

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    void FixedUpdate()
    {
        if (running && (stamina != 0) && (moveDirection != Vector2.zero))
        {
            rigid.velocity = new Vector2(moveDirection.x * speed * runSpeedX, moveDirection.y * speed * runSpeedX);

            stamina -= runCost * Time.deltaTime;
            if (stamina < 0) stamina = 0;
            StaminaBar.fillAmount = stamina / maxStamina;

            if (recharge != null) StopCoroutine(recharge);
            recharge = StartCoroutine(RechargeStamina());
        }
        else
        {
            rigid.velocity = new Vector2(moveDirection.x * speed, moveDirection.y * speed);
        }
        //Vector2 vel = Vector2.zero;
        //if (dirHeld > -1) vel = directions[dirHeld];

        //if (running && (stamina != 0) && (dirHeld > -1))
        //{
        //    rigid.velocity = vel * speed * runSpeedX;

        //    stamina -= runCost * Time.deltaTime;
        //    if (stamina < 0) stamina = 0;
        //    StaminaBar.fillAmount = stamina / maxStamina;

        //    if (recharge != null) StopCoroutine(recharge);
        //    recharge = StartCoroutine(RechargeStamina());

        //}
        //else
        //{
        //    rigid.velocity = vel * speed;
        //}

        Vector2 lookDir = mousePos - rigid.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rigid.rotation = angle;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject go = collision.gameObject;
        if (go.tag.Equals("Tire") && !tireCarrying)
        {
            Destroy(go);
            tireCarrying = true;
        }
        else if (go.tag.Equals("PlacedTire") && tireCarrying)
        {
            tireCarrying = false;
            tireNumber++;
            Destroy(go);
            Debug.Log(tireNumber);
            if (tireNumber == 4)
            {
                Destroy(Car);
                Invoke("SpawnDelay", 3);
            }
        }
        if (go.tag.Equals("Zombie") || go.tag.Equals("Bosscar"))
        {
            if (regen != null) StopCoroutine(regen);
            regen = StartCoroutine(RegenHealth());
        }
    }

    private IEnumerator RechargeStamina()
    {
        yield return new WaitForSeconds(3f);

        while (stamina < maxStamina)
        {
            stamina += chargeRate / 10f;
            if (stamina > maxStamina) stamina = maxStamina;
            StaminaBar.fillAmount = stamina / maxStamina;
            yield return new WaitForSeconds(.1f);
        }
    }

    private IEnumerator RegenHealth()
    {
        yield return new WaitForSeconds(3f);

        while (health < maxHealth)
        {
            health += regenRate / 10f;
            if (health > maxHealth) health = maxHealth;
            HealthBar.fillAmount = health / maxHealth;
            yield return new WaitForSeconds(.1f);
        }
    }

    void SpawnDelay()
    {
        BossCar.SetActive(true);
    }
}
