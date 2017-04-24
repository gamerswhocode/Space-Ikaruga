using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {

    public float _Speed;

    void Start()
    {
        Rigidbody pRigidBody = gameObject.GetComponent<Rigidbody>();
        pRigidBody.velocity = transform.forward * _Speed;
    }

}
