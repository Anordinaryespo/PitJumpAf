using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    private bool attacking = false;
    private float timeBtwAttack;
    public float startTimeBtwAttack;

    public Transform attackPos;
    public LayerMask whatIsEnemies;

    public Collider2D mace;


    // Start is called before the first frame update
    void Start()
    {
        mace.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Attack") && !attacking)
        {
            mace.enabled = true;
            //timeBtwAttack = startTimeBtwAttack;

            attacking = true;
        }

        if(Input.GetButtonUp("Attack") && attacking)
        {
            /*timeBtwAttack -= Time.deltaTime;
        }
        else
        {*/
            attacking = false;
            mace.enabled = false;
        }

    }
}
