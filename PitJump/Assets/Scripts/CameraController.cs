using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Movement thePlayer;

    private Vector3 lastPlayerPosition;
    private float distanceToMoveX;
    //private float distanceToMoveY;

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<Movement>();
        lastPlayerPosition = thePlayer.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        //distanceToMoveY = thePlayer.transform.position.y - lastPlayerPosition.y;

        distanceToMoveX = thePlayer.transform.position.x - lastPlayerPosition.x;

        transform.position = new Vector3(transform.position.x + distanceToMoveX, transform.position.y/* + distanceToMoveY*/, transform.position.z);

        lastPlayerPosition = thePlayer.transform.position;

    }
}
