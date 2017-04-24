using UnityEngine;
using System.Collections;

public class DestroyByTime : MonoBehaviour {

    public float _lifeTime;

    void Start()
    {
        Destroy(gameObject, _lifeTime);
    }


}
