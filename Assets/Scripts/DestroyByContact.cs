using UnityEngine;
using System.Collections;



public class DestroyByContact : MonoBehaviour {

    public GameObject _explosion;
    public GameObject _playerExplosion;

    private GameController _gameController;

    private PlayerController _playerController;

    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        GameObject playerControllerObject = GameObject.FindWithTag("Player");

        if (playerControllerObject != null)
        {
            _playerController = playerControllerObject.GetComponent<PlayerController>();
        }
        if (_playerController == null)
            Debug.Log("Cannot find Player script");

        if (gameControllerObject != null)
        {
            _gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (_gameController == null)
            Debug.Log("Cannot find GameController script");

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Boundary")
        {
            if (other.tag == "Player")
            {
                if (!ValidateShieldPass())
                {
                    Instantiate(_playerExplosion, other.transform.position, other.transform.rotation);
                    _gameController.GameOver();
                    Destroy(other.gameObject);
                    Destroy(gameObject);
                }
                else
                {
                    _gameController.AddScore(gameObject.tag);
                    Instantiate(_explosion, transform.position, transform.rotation);
                    Destroy(gameObject);
                }
            }
            if (other.tag == "BoltRed" || other.tag == "BoltBlue") //Colided with a Bolt
            {
                if(ValidateDestroyedAsteroid(other.tag))
                {
                    Instantiate(_explosion, transform.position, transform.rotation);
                    _gameController.AddScore(other.tag);
                    Destroy(other.gameObject);
                    Destroy(gameObject);
                }
                else
                {
                    gameObject.GetComponent<AudioSource>().Play();
                    Destroy(other.gameObject);
                }
            }
            

        }
        else
            return;
    }

    private bool ValidateShieldPass()
    {
        switch(gameObject.tag)
        {
            case "AsteroidRed":
                //return !_playerController.getRedShot() && !_playerController.getSwitchingLasers();
                return false;
            case "AsteroidBlue":
                return false;
              //  return _playerController.getRedShot() && !_playerController.getSwitchingLasers();
            default:
                return _playerController.getSwitchingLasers();
        }
    }

    private bool ValidateDestroyedAsteroid(string pBolt)
    {
        switch (gameObject.tag)
        {
            case "AsteroidRed":
                return pBolt == "BoltRed";
            case "AsteroidBlue":
                return pBolt == "BoltBlue";
            default:
                return false;
        }
    }
}
