using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    // Use this for initialization
    public float speed;
    public Boundary _boundry;
    public float tilt;

    public GameObject[] _shot;
    public Transform _shotSpawn;

    public float _FireRate;
    private float _NextFire;

    public float _SwitchRate;
    private float _NextSwitch;
    private bool _SwitchingLasers;

    private bool _redShot;

    void Start()
    {
        _redShot = false;
        _SwitchingLasers = false;
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        

        Rigidbody rigidBody = gameObject.GetComponent<Rigidbody>();
        rigidBody.velocity = new Vector3((speed * moveHorizontal),0.0f, (speed * moveVertical));

        rigidBody.position = new Vector3(Mathf.Clamp(rigidBody.position.x, _boundry.xMin, _boundry.xMax),
            0.0f,
            Mathf.Clamp(rigidBody.position.z, _boundry.zMin, _boundry.zMax));

        rigidBody.rotation = Quaternion.Euler(0.0f, 0.0f, rigidBody.velocity.x * -tilt);
    }

    void Update()
    {

        CheckDoneSwitching();
        if (Input.GetButton("Fire1"))
        {
            FactoryShot();
        }
        if (Input.GetButton("Jump"))
        {
            UpdateSelectedShot();
        }
    }

    public bool getSwitchingLasers()
    {
        return _SwitchingLasers;
    }

    public bool getRedShot()
    {
        return _redShot;
    }

    private void CheckDoneSwitching()
    {
        if (Time.time > _NextSwitch)
        {
            _SwitchingLasers = false;
            Renderer objRenderer = gameObject.GetComponent<Renderer>();
            objRenderer.material.color = _redShot ? Color.blue : Color.red;
        }
    }

    private void FactoryShot()
    {
        if (Time.time > _NextFire && !_SwitchingLasers)
        { 
            _NextFire = Time.time + _FireRate;
            Instantiate(DetermineShotType(), _shotSpawn.position, _shotSpawn.rotation);
            gameObject.GetComponent<AudioSource>().Play();
        }
    }

    private void UpdateSelectedShot()
    {
        if(Time.time > _NextSwitch)
        {
            _NextSwitch = Time.time + _SwitchRate;
            _redShot = !_redShot;
            _SwitchingLasers = true;
            Renderer objRenderer = gameObject.GetComponent<Renderer>();
            objRenderer.material.color = Color.white;
        }
    }

    private GameObject DetermineShotType()
    {
        if (_shot.Length == 2)
        {
            if (_redShot)
                return _shot[0];
            else
                return _shot[1];
        }
        else
            return null;
    }
}

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}
