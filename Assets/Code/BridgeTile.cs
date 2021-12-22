using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using Random = UnityEngine.Random;

public class BridgeTile : MonoBehaviour
{
    //Bridge Obstacle
    private BridgeSpawner _bridgeSpawner;

    //Obstacle variables
    private int _obstacleRnd;
    [SerializeField] GameObject obstaclePrefab;

    [SerializeField] GameObject longObstaclePrefab;

    //shield variables
    public GameObject shieldPrefab;
    private float[] x = new[] {3.3f, 0f, -3.3f};

    // Start is called before the first frame update
    void Start()
    {
        _bridgeSpawner = GameObject.FindObjectOfType<BridgeSpawner>();
        _obstacleRnd = Random.Range(0, 5);
        SpawnObstacle();
        SpawnShield();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<VikingController>()._isStart == false)
            {
                other.GetComponent<VikingController>().InitialRotation();
                other.GetComponent<VikingController>()._isStart = true;
            }

            other.GetComponent<VikingController>().CanRotate = false;
            _bridgeSpawner.SpawnTile();
            Destroy(_bridgeSpawner.pppTile);
        }
    }

    void SpawnObstacle()
    {
        try
        {
            for (int i = 0; i < _obstacleRnd; i++)
            {
                int obstacleIndex = Random.Range(3, 9);
                Transform spawnPoint = transform.GetChild(obstacleIndex).transform;

                Instantiate(obstaclePrefab, spawnPoint.position, Quaternion.Euler(0, transform.eulerAngles.y, 0),
                    transform);
            }


            if (_obstacleRnd < 3 && GameManager.Instance.GetScore() > 5)
            {
                Transform obstaclePoint = transform.GetChild(9).transform;
                Instantiate(longObstaclePrefab, obstaclePoint.position, Quaternion.Euler(0, transform.eulerAngles.y, 0),
                    transform);
            }

            _obstacleRnd = Random.Range(0, 6);
        }
        catch (Exception)
        {
            return;
        }
    }
    
    private void SpawnShield()
    {
        try
        {
            for (int i = 0; i < 10; i++)
            {
                GameObject temp = Instantiate(shieldPrefab, transform);
                if (!transform.CompareTag("brokeBridge"))
                {
                    temp.transform.localPosition = new Vector3(x[Random.Range(0,3)],1.2f,Random.Range(11,67));
                }
                else
                {
                    temp.transform.localPosition = new Vector3(x[Random.Range(0,3)],1.2f,Random.Range(17,54));
                }
            }
        }
        catch (Exception)
        {
        }
    }
    
}