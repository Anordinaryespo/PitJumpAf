using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{

    public GameObject thePlatform;
    public Transform generationPoint;
    public float distanceBetween;

    private float platformWidth;

    public float distanceBetweenMin;
    public float distanceBetweenMax;

    //public GameObject[] thePlatforms;

    private int platformSelector;

    private float[] platformWidths;

    public ObjectPooler[] theObjectPools;

    private float minHeight;
    public Transform maxHeightPoint;
    private float maxHeight;
    public float maxHeightChange;
    private float heightChange;

    private CoinGenerator theCoinGenerator;
    private BigCoinGenerator theBigCoinGenerator;

    public float randomCoinsTreshold;
    public float randomBigCoinTreshold;

    public float randomEnemyTreshold;

    public ObjectPooler theEnemyPool;

    public float powerupHeight;
    public ObjectPooler powerupPool;
    public float powerupTreshold;

    // Start is called before the first frame update
    void Start()
    {

        //platformWidth = thePlatform.GetComponent<BoxCollider2D>().size.x;

        platformWidths = new float[theObjectPools.Length];

        for (int i = 0; i < theObjectPools.Length; i++)
        {

            platformWidths[i] = theObjectPools[i].pooledObject.GetComponent<BoxCollider2D>().size.x;

        }

        minHeight = transform.position.y;
        maxHeight = maxHeightPoint.position.y;

        theCoinGenerator = FindObjectOfType<CoinGenerator>();
        theBigCoinGenerator = FindObjectOfType<BigCoinGenerator>();

}

    // Update is called once per frame
    void Update()
    {
        float randomNumb = Random.Range(0f, 100f);
        float randomNumbEnemy = Random.Range(0f, 100f);

        if (transform.position.x < generationPoint.position.x)
        {

            distanceBetween = Random.Range(distanceBetweenMin, distanceBetweenMax);

            platformSelector = Random.Range(0, theObjectPools.Length);

            heightChange = transform.position.y + Random.Range(maxHeightChange, -maxHeightChange);

            if(heightChange > maxHeight)
            {
                heightChange = maxHeight;
            } else if (heightChange < minHeight)
            {
                heightChange = minHeight;
            }


            if(Random.Range(0f, 100f) < powerupTreshold)
            {
                GameObject newPowerup = powerupPool.GetPooledObject();

                newPowerup.transform.position = transform.position + new Vector3(distanceBetween, powerupHeight, 0f);

                newPowerup.SetActive(true);
            }

            transform.position = new Vector3(transform.position.x + ((platformWidths[platformSelector])/2) + distanceBetween, heightChange, transform.position.z);


            if (randomNumb > randomCoinsTreshold)
            {
                theCoinGenerator.SpawnCoins(new Vector3(transform.position.x, heightChange + (Random.Range(3f, 6f)), transform.position.z));
            }
            else if (randomNumb > randomBigCoinTreshold && randomNumb < randomCoinsTreshold)
            {
                theBigCoinGenerator.SpawnBigCoin(new Vector3(transform.position.x, heightChange + (Random.Range(10f, 12f)), transform.position.z));
            }

            //Instantiate(/*thePlatform*/ theObjectPools[platformSelector], transform.position, transform.rotation);

            GameObject newPlatform = theObjectPools[platformSelector].GetPooledObject();

            newPlatform.transform.position = transform.position;
            newPlatform.transform.rotation = transform.rotation;
            newPlatform.SetActive(true);

            if (randomNumbEnemy > randomEnemyTreshold)
            {
                GameObject newEnemy = theEnemyPool.GetPooledObject();

                Vector3 enemyPosition = new Vector3(Random.Range((-platformWidths[platformSelector]/4), (platformWidths[platformSelector]/4)), 3.5f, 0f);

                newEnemy.transform.position = transform.position + enemyPosition;
                newEnemy.transform.rotation = transform.rotation;
                newEnemy.SetActive(true);
            }

                transform.position = new Vector3(transform.position.x + ((platformWidths[platformSelector])/2) + distanceBetween, transform.position.y, transform.position.z);


        }
        
    }
}
