using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    [SerializeField] private GameObject platformPrefab;
    [SerializeField] private float speed;

    private bool isLeft;
    private float spawmTime;
    // Start is called before the first frame update
    void Start()
    {
        spawmTime = Random.Range(10f, 15f);
        InvokeRepeating(nameof(PlatformSpawn), 1f, spawmTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void PlatformSpawn()
    {
        Vector3 temp = transform.GetChild(0).transform.position; temp.y -= 0.51f;
        Instantiate(platformPrefab, temp, Quaternion.identity);
        spawmTime = Random.Range(10f, 15f);
    }

    private void FixedUpdate()
    {
        if (transform.position.x < -2.70) isLeft = false;
        if (transform.position.x > 2.70) isLeft = true;

        Vector3 tempVel = transform.position;
        if (isLeft)
            tempVel.x -=Time.deltaTime * speed;
        else
            tempVel.x +=  Time.deltaTime * speed;

        transform.position = tempVel;
    }


}
