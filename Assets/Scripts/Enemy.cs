using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 1.0f;
    private List<GameObject> _destinations = new List<GameObject>();
    private int _destIdx = 0;
    public int currentEnemyNumber;
    public GameObject curDestination;
    [FormerlySerializedAs("newVec")] public Vector3 destVec;
    private void Start()
    {
        GetDestinations();
        curDestination = _destinations[_destIdx];
    }

    void Update()
    {
        if (_destIdx < _destinations.Count - 1)
            MoveToDestination();
        transform.Translate(_speed * Time.deltaTime * Vector3.forward);

        if (transform.position.z < -6.5f)
        {
            Destroy(gameObject);
        }
    }

    void GetDestinations()
    {
        GameObject parentObject = GameObject.Find("Destinations");
        if (parentObject != null)
        {
            for (int i = 0; i < parentObject.transform.childCount; i++)
            {
                Transform childTransform = parentObject.transform.GetChild(i);
                if (childTransform != null)
                {
                    _destinations.Add(childTransform.gameObject);
                }
                else
                    Debug.Log("No child found");
            }
        }
        else
            Debug.Log("parentObject is not founded");
    }
    void MoveToDestination()
    {
        
        destVec = new Vector3(curDestination.transform.position.x,
            transform.position.y, curDestination.transform.position.z);
        float distance = (destVec - transform.position).magnitude;
        if (distance < 0.1f)
        {
            _destIdx++; 
            transform.Rotate(Vector3.up, -90.0f, Space.World);
            curDestination = _destinations[_destIdx]; 
        }
    }
}
