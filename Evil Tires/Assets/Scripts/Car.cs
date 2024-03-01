using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    [Header("Set in Inspector: BossCar")]
    public float carSpeed = 0.2f;
    public float smoothRotate = 10.0f;
    public float damage;

    private Rigidbody2D carRb;
    private Policeman policeman;
    private int moveDir;
    private int curDir;
    private bool canChange;
    private Vector2 moveDirection = Vector2.up;


    void Start()
    {
        carRb = GetComponent<Rigidbody2D>();
        //moveDir = Random.Range(1, 5); // 1-4  -- 1=down 2=left 3=right 4=up
        moveDir = 4;
        //if (moveDir < 1 || moveDir > 4)
        //{
        //    while (moveDir < 1 || moveDir > 4) { moveDir = Random.Range(1, 5); }
        //}
        canChange = true;
        policeman = FindObjectOfType<Policeman>();
    }

    void Update()
    {
        if (policeman != null)
            MovementHandler();
    }

    void FixedUpdate()
    {
        carRb.MovePosition(carRb.position + (moveDirection * carSpeed) * Time.fixedDeltaTime);
    }

    void ChangeDirection()
    {
        moveDir = Random.Range(1, 5); // 1-4  -- 1=down 2=left 3=right 4=up
        if (moveDir < 1 || moveDir > 4)
        {
            while (moveDir < 1 || moveDir > 4)
            {
                moveDir = Random.Range(1, 5);
                if (moveDir == curDir)
                {
                    moveDir = Random.Range(1, 5);
                }
            }// End of While loop
        }// End of IF moveDir statement
    }

    void MoveLeft()
    {
        curDir = 2;
        carRb.transform.GetChild(0).GetChild(0).eulerAngles = new Vector3(0, 0, -90);
        Quaternion target = Quaternion.Euler(0, 0, 90);
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smoothRotate);
        moveDirection = Vector2.left;
    }

    void MoveRight()
    {
        curDir = 3;
        carRb.transform.GetChild(0).GetChild(0).eulerAngles = new Vector3(0, 0, 90);
        Quaternion target = Quaternion.Euler(0, 0, 270);
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smoothRotate);
        moveDirection = Vector2.right;
    }

    void MoveUp()
    {
        curDir = 4;
        carRb.transform.GetChild(0).GetChild(0).eulerAngles = new Vector3(0, 0, 180);
        Quaternion target = Quaternion.Euler(0, 0, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smoothRotate);
        moveDirection = Vector2.up;
    }

    void MoveDown()
    {
        curDir = 1;
        carRb.transform.GetChild(0).GetChild(0).eulerAngles = new Vector3(0, 0, 0);
        Quaternion target = Quaternion.Euler(0, 0, 180);
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smoothRotate);
        moveDirection = Vector2.down;
    }

    void StopMoving()
    {
        moveDirection = Vector2.zero;
    }

    void MovementHandler()
    {
        if (moveDir == 1)
        {
            MoveDown();
        }
        else if (moveDir == 2)
        {
            MoveLeft();
        }
        else if (moveDir == 3)
        {
            MoveRight();
        }
        else if (moveDir == 4)
        {
            MoveUp();
        }

        if (canChange == true)
        {
            StartCoroutine(RandomMovement());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject go = collision.gameObject;
        if (go.tag == "Wall" || go.tag == "Obstacles")
        {
            StopMoving();
            ChangeDirection();
        }
        if (go.tag == "Policeman")
        {
            policeman.health -= damage;
            policeman.HealthBar.fillAmount = policeman.health / policeman.maxHealth;
            if (policeman.health <= 0) Destroy(go);
        }
    }

    IEnumerator RandomMovement()
    {
        canChange = false;
        var timer = Random.Range(3f, 5f);
        yield return new WaitForSeconds(timer);
        ChangeDirection();
        yield return new WaitForSeconds(0.5f);
        canChange = true;
    }
}
