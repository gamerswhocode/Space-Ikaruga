using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class DetermineScoring
{

    public float _ComboTimerCap = 2.0f;
    public int _CollideScore = 20;
    public int _CollideWindowScore = 50;
    public int _ShootScore = 10;
    

    private float _ComboTimer;
    private int _CurrentCombo;

    private int _Score;

    public DetermineScoring()
    {
        _Score = 0;
        _ComboTimer = 0.0f;
        _CurrentCombo = 0;
    }

    public void UpdateScore(string pCollidedObj)
    {
        if (Time.time < _ComboTimer)
        {
            _CurrentCombo++;
            _ComboTimer = Time.time + _ComboTimerCap;
        }
        else
        {
            _CurrentCombo = 0;
            _ComboTimer = 0.0f;
        }

        if (pCollidedObj == "Untagged") {
            _Score += _CollideWindowScore;
        }
        else if (pCollidedObj.Contains("Asteroid")) {
            _Score += _CollideScore;
        }
        else {
            _Score += _ShootScore;
        }
        Debug.Log(_Score);
        
    }

    public int getCurrentScore()
    {
        return _Score;
    }

}

public class GameController : MonoBehaviour {

    public GameObject[] _hazards;
    public Vector3 _spawnValues;
    public int _hazardCount;
    public float _startWait;
    public float _spawnWait;
    public float _nextLevelWait;

    public DetermineScoring _ScoreCard;

    public GUIText _scoreText;
    public GUIText _restartText;
    public GUIText _gameOverText;

    private bool _gameOver;
    private bool _restart;
    private bool _menuOpened;


    void Start()
    {
        initUI();
        StartCoroutine(SpawnWaves());
    }

    private void initUI()
    {
        _ScoreCard = new DetermineScoring();

        _gameOver = false;
        _restart = false;
        _menuOpened = false;
        _restartText.text = string.Empty;
        _gameOverText.text = string.Empty;
    }

    void Update()
    {
        validateOpenMenu();
        validateRestartLevel();
    }

    private void validateOpenMenu()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button7) || Input.GetKeyDown(KeyCode.Return))
        {
            if (!_menuOpened)
            {
                _menuOpened = true;
                openMenu();
            }
            else
            {
                _menuOpened = false;
                closeMenu();
            }
        }

    }

    private void openMenu()
    {
        Time.timeScale = 0.0f;
    }

    private void closeMenu()
    {
        Time.timeScale = 1.0f;
    }

    private void validateRestartLevel()
    {
        if (_restart)
        {
            if (Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.Joystick1Button6))
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }
    }

    IEnumerator SpawnWaves()
    {

        yield return new WaitForSeconds(_startWait);
        while(true)
        { 
            for (int i = 0 ; i < _hazardCount; i++) { 
                Vector3 pSpawnPosition = new Vector3(Random.Range(- _spawnValues.x, _spawnValues.x)
                     , _spawnValues.y, _spawnValues.z);
                Quaternion pSpawnRotation = Quaternion.identity;

                Instantiate(_hazards[Random.Range(0, _hazards.Length)], pSpawnPosition, pSpawnRotation);

                yield return new WaitForSeconds(_spawnWait);
            }
            yield return new WaitForSeconds(_nextLevelWait);
            if (_gameOver)
            { 
                _restartText.text = "Press R to try again";
                _restart = true;
                break;
            }
        }
    }

    public void GameOver()
    {
        _gameOverText.text = "Game Over!";
        _gameOver = true;
    }

    public void AddScore(string pCollidedObject)
    {
        _ScoreCard.UpdateScore(pCollidedObject);
        UpdateScore(_ScoreCard.getCurrentScore());
    }

    private void UpdateScore(int pScore)
    {
        _scoreText.text = "Score: " + pScore.ToString();
    }


    
	
}
