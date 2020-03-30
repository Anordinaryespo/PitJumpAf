using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public Transform platformGenerator;
    private Vector3 platformStartPoint;

    public Movement thePlayer;
    private Vector3 playerStartPoint;

    private PlatformDestroyer[] platformList;

    private ScoreManager theScoreManager;

    public DeathMenu theDeathScreen;

    public bool powerupReset;

    public GameObject theBoss;
    private float scoreCount;

    // Start is called before the first frame update
    void Start()
    {
        platformStartPoint = platformGenerator.position;

        playerStartPoint = thePlayer.transform.position;

        theScoreManager = FindObjectOfType<ScoreManager>();

        theBoss = GameObject.Find("Boss");

        scoreCount = 0;
     
    }

    // Update is called once per frame
    void Update()
    {
        scoreCount = theScoreManager.scoreCount;

        if(theScoreManager.scoreCount >= 1000 && theScoreManager.scoreCount <= 3000)
        {
            theBoss.SetActive(true);
        } else
        {
            theBoss.SetActive(false);
        }
    }

    public void RestartGame()
    {

        theScoreManager.scoreIncreasing = false;
        thePlayer.gameObject.SetActive(false);

        theDeathScreen.gameObject.SetActive(true);

        //StartCoroutine("RestartGameCo");

    }

    public void Reset()
    {
        theDeathScreen.gameObject.SetActive(false);

        platformList = FindObjectsOfType<PlatformDestroyer>();

        for (int i = 0; i < platformList.Length; i++)
        {
            platformList[i].gameObject.SetActive(false);
        }

        thePlayer.transform.position = playerStartPoint;
        platformGenerator.position = platformStartPoint;
        thePlayer.gameObject.SetActive(true);

        theScoreManager.scoreCount = 0;
        theScoreManager.scoreIncreasing = true;

        powerupReset = true;
    }

    /*public IEnumerator RestartGameCo() 
    {
        theScoreManager.scoreIncreasing = false;
        thePlayer.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);

        platformList = FindObjectsOfType<PlatformDestroyer>();

        for(int i =0; i < platformList.Length; i++)
        {
            platformList[i].gameObject.SetActive(false);
        }

        thePlayer.transform.position = playerStartPoint;
        platformGenerator.position = platformStartPoint;
        thePlayer.gameObject.SetActive(true);

        theScoreManager.scoreCount = 0;
        theScoreManager.scoreIncreasing = true;
    }*/
}
