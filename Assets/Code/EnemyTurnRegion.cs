using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurnRegion : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyController>()._shouldRotate = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyController>()._shouldRotate = false;
        }
    }
}
