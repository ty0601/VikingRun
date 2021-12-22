using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private  VikingController _vikingController;

    // Start is called before the first frame update
    void Start()
    {
        _vikingController = GameObject.FindObjectOfType<VikingController>();
        gameObject.tag = "Obstacle";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
        if (other.CompareTag("Player"))
        {
            _vikingController.Die();
        }
        
    }
}
