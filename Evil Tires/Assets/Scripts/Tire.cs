using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tire : MonoBehaviour
{
    //[SerializeField]
    //private Text pickUpText;

    //private bool pickUpAllowed;

    //void Start()
    //{
    //    pickUpText.gameObject.SetActive(false);
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    if (pickUpAllowed && Input.GetKeyDown(KeyCode.E))
    //        PickUp();
    //}

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.name.Equals("Policeman"))
    //    {
    //        pickUpText.gameObject.SetActive(true);
    //        pickUpAllowed = true;
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.gameObject.name.Equals("Policeman"))
    //    {
    //        pickUpText.gameObject.SetActive(false);
    //        pickUpAllowed = false;
    //    }
    //}

    private void PickUp()
    {
        Destroy(gameObject);
    }
}
