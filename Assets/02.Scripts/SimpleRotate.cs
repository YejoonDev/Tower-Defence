using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRotate : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 20.0f;
    
    void Update()
    {
        transform.Rotate(Time.deltaTime * rotateSpeed * Vector3.up);
    }
}
