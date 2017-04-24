using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {

    public float _tumble;

    void Start()
    {
        Rigidbody pRigidBody = gameObject.GetComponent<Rigidbody>();
        pRigidBody.angularVelocity = Random.insideUnitSphere * _tumble;
    }

}
