using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class TurningRegion : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<VikingController>().CanRotate = true;
            other.GetComponent<VikingController>()._entryAngle = other.GetComponent<VikingController>()._targetAngle;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<VikingController>().CanRotate = false;
        }
    }
}
