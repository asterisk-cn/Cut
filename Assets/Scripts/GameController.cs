using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField]
    GameObject _gameOverPanel;
    [SerializeField]
    TextMeshProUGUI _scoreText;

    public static GameController Instance { get; private set; } = null;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    void Start()
    {
        _gameOverPanel.SetActive(false);

        AudioManager.Instance.PlayBGM("Main");
    }

    public void GameOver(int score)
    {
        _gameOverPanel.SetActive(true);
        _scoreText.text = "Score: " + score;
    }

}
