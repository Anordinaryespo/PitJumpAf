using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    private bool doublePoints;
    private bool safeMode;

    private bool powerupActive;

    private float powerupLengthCounter;

    private ScoreManager theScoreManager;
    private PlatformGenerator thePlatformGenerator;
    private GameManager theGameManager;

    private float normalPointsPerSecond;
    private float enemyRate;

    private PlatformDestroyer[] enemyList;

    // Start is called before the first frame update
    void Start()
    {
        theScoreManager = FindObjectOfType<ScoreManager>();
        thePlatformGenerator = FindObjectOfType<PlatformGenerator>();
        theGameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(powerupActive == true)
        {

            powerupLengthCounter -= Time.deltaTime;

            if(theGameManager.powerupReset == true)
            {
                powerupLengthCounter = 0;
                theGameManager.powerupReset = false;
            }

            if(doublePoints == true)
            {
                
                theScoreManager.shouldDouble = true;
            }

            if(safeMode == true)
            {
                thePlatformGenerator.randomEnemyTreshold = 0f;
            }

            if(powerupLengthCounter <= 0)
            {
                
                theScoreManager.shouldDouble = false;
                thePlatformGenerator.randomEnemyTreshold = enemyRate;

                powerupActive = false;
            }

        }
    }

    public void ActivatePowerup(bool points, bool safe, float time)
    {
        doublePoints = points;
        safeMode = safe;
        powerupLengthCounter = time;

        normalPointsPerSecond = theScoreManager.pointsPerSecond;
        enemyRate = thePlatformGenerator.randomEnemyTreshold;

        if (safeMode == true)
        {
            enemyList = FindObjectsOfType<PlatformDestroyer>();

            for (int i = 0; i < enemyList.Length; i++)
            {
                if (enemyList[i].gameObject.name == "Enemy2(Clone)")
                { 
                    enemyList[i].gameObject.SetActive(false);
                }
            }
        }

        powerupActive = true;

    }

    public bool getPowerupActive()
    {
        return powerupActive;
    }
    public void setPowerupActive(bool _powerupActive)
    {
        powerupActive = _powerupActive;
    }

    public bool getDoublePoints()
    {
        return doublePoints;
    }
    public void setDoublePoints(bool _doublePoints)
    {
        doublePoints = _doublePoints;
    }

    public bool getSafeMode()
    {
        return safeMode;
    }
    public void setSafeMode(bool _safeMode)
    {
        safeMode = _safeMode;
    }

}
