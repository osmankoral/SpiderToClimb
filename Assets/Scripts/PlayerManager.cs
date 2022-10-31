using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    GameController gameController;



    private void Awake()
    {
        gameController = FindObjectOfType<GameController>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Platform"))
        {
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Platform"))
            GameManager.Instance.LevelState(false);
    }


}
