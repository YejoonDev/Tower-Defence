using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private readonly float _speed = 5.0f;
    private readonly float _upsideLimit = -1.0f;
    private readonly float _downsideLimit = 10.0f;
    private readonly float _leftsideLimit = -1.0f;
    private readonly float _rightsideLimit = -7.0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveCamera();
    }

    public void MoveCamera()
    {
        if(Input.GetKey(KeyCode.S))
        {
            transform.Translate(_speed * Time.deltaTime * Vector3.forward, Space.World);
            float newZ = Mathf.Clamp(transform.position.z, _upsideLimit, _downsideLimit);
            transform.position = new Vector3(transform.position.x, transform.position.y, newZ);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(_speed * Time.deltaTime * -Vector3.forward, Space.World);
            float newZ = Mathf.Clamp(transform.position.z, _upsideLimit, _downsideLimit);
            transform.position = new Vector3(transform.position.x, transform.position.y, newZ);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(_speed * Time.deltaTime * Vector3.right, Space.World);
            float newX = Mathf.Clamp(transform.position.x, _rightsideLimit, _leftsideLimit);
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(_speed * Time.deltaTime * Vector3.left, Space.World);
            float newX = Mathf.Clamp(transform.position.x, _rightsideLimit, _leftsideLimit);
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);
        }
    }
}
