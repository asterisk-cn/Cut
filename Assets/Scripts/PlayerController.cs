using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField]
    private float _speed = 10.0f;

    private PlayerInputs _inputs;

    public TargetController target;

    private float _currentSpeed;

    void Awake()
    {
        _inputs = GetComponent<PlayerInputs>();
    }

    void Update()
    {
        if (_currentSpeed == 0)
        {
            return;
        }

        if (_inputs.fire)
        {
            _currentSpeed = 0;
            target.CheckAnswer(transform.position.x);
            AudioManager.Instance.PlaySE("Fire");
        }

        var newX = transform.position.x + _currentSpeed * Time.deltaTime;

        if (newX > target.transform.localScale.x / 2)
        {
            transform.position = new Vector3(-target.transform.localScale.x / 2, transform.position.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);
        }
    }

    public void Reset()
    {
        transform.position = new Vector3(-target.transform.localScale.x / 2, transform.position.y, transform.position.z);
        _currentSpeed = _speed;
    }
}
