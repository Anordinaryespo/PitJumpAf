using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackTrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.isTrigger && other.CompareTag("Killbox"))
        {

            other.SendMessageUpwards("Destroy");
            print("aleculo");

        }
    }

}
