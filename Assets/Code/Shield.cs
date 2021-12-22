using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private float _turnSpeed = 90f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Obstacle>() != null)
        {
            Destroy(gameObject);
            return;
        }

        if (other.gameObject.CompareTag("shield"))
        {
            Destroy(gameObject);
        }
        if (other.gameObject.name != "viking")
        {
            return;
        }

        GameManager.Instance.AddScore();
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0,0,_turnSpeed * Time.deltaTime);
    }
}
