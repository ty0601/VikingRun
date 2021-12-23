using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    private Animator _animator;
    private CharacterController _character;
    private VikingController _vikingController;
    public bool _shouldRotate;
    private float _speed = 0f;


    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _character = gameObject.GetComponent<CharacterController>();
        _vikingController = GameObject.FindObjectOfType<VikingController>();
    }

    private void Restart()
    {
        SceneManager.LoadScene(3);
    }

    // Update is called once per frame
    void Update()
    {
        if (!_vikingController._isStart)
        {
            _speed = 0;
        }
        else if (Vector3.Distance(_vikingController.transform.position, transform.position) <= 5 ||
                 _vikingController._isDrop)
        {
            _vikingController.Die();
            _animator.SetBool("isRun", false);
            _speed = 0;
            if (!_vikingController._isDrop)
            {
                transform.LookAt(_vikingController.transform);
                _animator.SetBool("isPlayerDie", true);
            }

            Invoke("Restart", 2.8f);
        }
        else
        {
            // transform.Rotate(transform.rotation.x * (-1) * 10f * Time.deltaTime,0,0);
            _animator.SetBool("isRun", true);
            if (_shouldRotate)
            {
                _shouldRotate = false;
                transform.LookAt(_vikingController.transform);
                transform.rotation = Quaternion.Euler(0,transform.eulerAngles.y,transform.eulerAngles.z);
            }

            if (Vector3.Distance(_vikingController.transform.position, transform.position) >= 25)
            {
                _speed = 23f;
            }
            else
            {
                _speed = 17f;
            }
        }

        
        _character.Move(transform.forward * Time.deltaTime * _speed);
    }
}