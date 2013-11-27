using UnityEngine;
using System.Collections;

public class Planet : MonoBehaviour 
{
    public float SpeedScalar = 48;
    //private float _rotation;
    private float _rotationSpeed;

    void Start()
    {
        _rotationSpeed = 360.0f / 24.0f / 60.0f / 60.0f;//360 deg / 24h / 60 min / 60 sec
    }

	void Update () 
    {
        transform.rotation = Quaternion.AngleAxis(-_rotationSpeed * SpeedScalar * Time.deltaTime, transform.up) * transform.rotation;
	}
}
