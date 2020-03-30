using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpPointE : MonoBehaviour
{

    public int scoreToGive;

    private ScoreManager theScoreManager;


    // Start is called before the first frame update
    void Start()
    {
        theScoreManager = FindObjectOfType<ScoreManager>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Mace")
        {
            theScoreManager.AddScore(scoreToGive);
        }
    }

}
