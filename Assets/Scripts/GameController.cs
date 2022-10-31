using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    private GameObject cameraFollower;
    private GameObject player;

    private float maxY;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cameraFollower = GameObject.FindGameObjectWithTag("Target");
    }

    private void Update()
    {
        if (player.transform.position.y > maxY) CameraFollowerUpdate();
    }

    private void CameraFollowerUpdate()
    {
        Vector3 tempVec = cameraFollower.transform.position;
        maxY = player.transform.position.y;
        tempVec.y = maxY;
        cameraFollower.transform.position = tempVec;
    }

    public void LevelStart()
    {

    }

    public void LevelEnd(int _reqItem, int _successItem)
    {
       
    }


    private void LevelStateFunc(bool _state)
    {
        GameManager.Instance.LevelState(_state);

    }

}
