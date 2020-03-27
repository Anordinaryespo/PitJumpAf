using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigCoinGenerator : MonoBehaviour
{

    public ObjectPooler bigCoinPool;

    public void SpawnBigCoin(Vector3 startPosition)
    {
        GameObject bigCoin = bigCoinPool.GetPooledObject();
        bigCoin.transform.position = startPosition;
        bigCoin.SetActive(true);
    }
}

