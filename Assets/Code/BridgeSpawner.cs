using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BridgeSpawner : MonoBehaviour
{
    [SerializeField] GameObject bridgeTile;
    [SerializeField] GameObject turingBridgeTile;
    [SerializeField] GameObject brokeBridgeTile;

    private GameObject _temp;
    private GameObject _cTile = null;
    private GameObject _pTile = null;
    private GameObject _ppTile = null;
    public GameObject pppTile = null;

    private Vector3 _nextSpawnPoint;
    private int _directionRnd;
    private int _wholeRnd;
    public void SpawnTile()
    {
        pppTile = _ppTile;
        _ppTile = _pTile;
        _pTile = _cTile;
        if (_directionRnd < 2)
        {
            if (_cTile == null) _temp = Instantiate(bridgeTile, _nextSpawnPoint, Quaternion.identity);
            else
            {
                if(_wholeRnd == 0) _temp = Instantiate(brokeBridgeTile, _nextSpawnPoint, _cTile.transform.rotation);
                else _temp = Instantiate(bridgeTile, _nextSpawnPoint, _cTile.transform.rotation);
            }
        }
        else if (_directionRnd == 2)
        {
            _temp = Instantiate(turingBridgeTile, _nextSpawnPoint,
                Quaternion.Euler(0, _cTile.transform.eulerAngles.y + 90, 0));
        }
        else if (_directionRnd == 3)
        {
            _temp = Instantiate(turingBridgeTile, _nextSpawnPoint,
                Quaternion.Euler(0, _cTile.transform.eulerAngles.y - 90, 0));
        }

        _nextSpawnPoint = _temp.transform.GetChild(0).transform.position;

        _cTile = _temp;
        _directionRnd = Random.Range(0, 4);
        _wholeRnd = Random.Range(0, 2);
    }

    // Start is called before the first frame update
    void Start()
    {
        _directionRnd = 0;
        SpawnTile();
        SpawnTile();
    }
}