using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    private float speed = 2f;

    private void FixedUpdate()
    {
        Vector3 tempVel = transform.position;
        tempVel.y -= Time.deltaTime * speed;
        transform.position = tempVel;
    }
}
