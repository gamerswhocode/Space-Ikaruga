using UnityEngine;
using System.Collections;

public class BGScroller : MonoBehaviour {

    public float _scrollSpeed;
    public float _tileSizeZ;

    private Vector3 _startPosition;

    void Start()
    {
        _startPosition = transform.position;
    }

	void Update () {
        
        float newPosition = Mathf.Repeat(Time.time * _scrollSpeed, _tileSizeZ);
        transform.position = _startPosition + Vector3.forward * newPosition;
	
	}


}
