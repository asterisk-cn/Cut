using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField]
    private float _speed = 5.0f;

    [SerializeField]
    private GameObject _gameObject;

    private PlayerInputs _inputs;

    void Awake()
    {
        _inputs = GetComponent<PlayerInputs>();
    }

    void Start()
    {

    }

    void Update()
    {

    }
}
