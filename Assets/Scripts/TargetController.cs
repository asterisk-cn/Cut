using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TargetController : MonoBehaviour
{
    [Header("Settings")]
    private float _hitPoint = 19.2f;

    [SerializeField]
    private PlayerController _player;
    [SerializeField]
    private TextMeshProUGUI _fractionText;
    [SerializeField]
    private GameObject _correctAnswer;

    private int _numerator;
    private int _denominator;
    private int _score;
    private float _currentHitPoint;

    private bool IsAlive
    {
        get
        {
            return _currentHitPoint > 0;
        }
    }

    public float Ratio
    {
        get
        {
            return (float)_denominator / _numerator;
        }
    }

    void Awake()
    {
        _currentHitPoint = _hitPoint;
        _player.target = this;
    }

    void Start()
    {
        Reset();
    }

    void Reset()
    {
        _numerator = Random.Range(2, 8);
        _denominator = Random.Range(1, _numerator);
        SimplifyFraction();

        _fractionText.text = _denominator + "/" + _numerator;

        ApplyScale();

        _player.Reset();
    }

    public void CheckAnswer(float positionX)
    {
        var answer = PositionX2Ratio(positionX);

        if (answer >= Ratio)
        {
            var extra = answer - Ratio;
            _score += CalculateScore(extra);
            Damage(extra);
        }
        else
        {
            _currentHitPoint = 0;

            _correctAnswer.transform.position = new Vector3(Ratio2PositionX(Ratio), _correctAnswer.transform.position.y, _correctAnswer.transform.position.z);
            _correctAnswer.SetActive(true);
        }

        if (!IsAlive)
        {
            GameController.Instance.GameOver(_score);
            return;
        }

        Reset();
    }

    void Damage(float extra)
    {
        _currentHitPoint -= _hitPoint * extra;

        if (_currentHitPoint < 0)
        {
            _currentHitPoint = 0;
        }

        ApplyScale();
    }

    int CalculateScore(float extra)
    {
        return (int)((1 - extra) * 100);
    }

    void ApplyScale()
    {
        transform.localScale = new Vector3(_currentHitPoint, transform.localScale.y, transform.localScale.z);
    }

    int Gcd(int a, int b)
    {
        if (b == 0)
        {
            return a;
        }

        return Gcd(b, a % b);
    }

    void SimplifyFraction()
    {
        int gcd = Gcd(_numerator, _denominator);
        _numerator /= gcd;
        _denominator /= gcd;
    }

    float Ratio2PositionX(float ratio)
    {
        return (ratio - 0.5f) * transform.localScale.x;
    }

    float PositionX2Ratio(float positionX)
    {
        return (positionX + transform.localScale.x / 2) / transform.localScale.x;
    }
}
