using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{ 
    /*void OnCollision2D(Collider2D other)
    {
        if (other.gameObject.tag == "MAZZA")
        {
            gameObject.SetActive(false);
            print("pitculo");
        }
    }*/

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "MAZZA")
        {
            gameObject.SetActive(false);
            print("pitculo");
        }

    }

    /* void Destroy()
     {
         gameObject.SetActive(false);
     }*/
}