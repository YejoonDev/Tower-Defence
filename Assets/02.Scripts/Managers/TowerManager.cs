using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class TowerManager : MonoBehaviour
{
    public static TowerManager Instance;
    public GameObject[] deployableTowers;
    
    public LayerMask whatIsBlock;
    public LayerMask whatIsGround;
    private LayerMask indicatorLayer;
    
    [SerializeField] private Transform indicator;
    private Vector3 _indicatorLastPos = Vector3.zero; 
    private Tower _activeTower;
    
    public bool isPlacing;

    private Camera _mainCamera;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            _mainCamera = Camera.main;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        indicatorLayer = indicator.gameObject.layer;
    }

    private void Update()
    {
        if (isPlacing)
        {
            var result = GetRaycastInfo();
            indicator.transform.position = result.location;
            
            
            if (Input.GetMouseButtonDown(0))
            {
                if (result.hitObject != null)
                {
                    if (result.hitObject.CompareTag("Block"))
                    {
                        isPlacing = false;
                        Block block = result.hitObject.GetComponent<Block>();
                    
                        if (!block.isExisted)
                        {
                            Instantiate(_activeTower, indicator.transform.position, indicator.transform.rotation);
                            indicator.gameObject.SetActive(false);
                            block.isExisted = true;
                        }
                        else
                        {
                            UIManager.Instance.DisplayAlarmText("We can't build there");
                        }
                    }
                    else if (result.hitObject.CompareTag("Ground"))
                    {
                        UIManager.Instance.DisplayAlarmText("We can't build there");
                    }
                }
                else
                {
                    UIManager.Instance.DisplayAlarmText("We can't build there");
                }
            }
        }
    }

    public void StartTowerPlacement(Tower towerToPlace)
    {
        isPlacing = true;
        _activeTower = towerToPlace;
        
        Destroy(indicator.gameObject);
        Tower placeTower = Instantiate(_activeTower);
        placeTower.enabled = false;
        indicator = placeTower.transform;
    }

    private (Vector3 location, GameObject hitObject) GetRaycastInfo()
    {
        indicator.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        Vector3 location = Vector3.zero;
        GameObject hitObject = null;
        
        // Debug.DrawRay(ray.origin, ray.direction * 200, Color.red);
        if (Physics.Raycast(ray, out hit, 200.0f))
        {
            int hitLayer = hit.collider.gameObject.layer;
            if ((whatIsBlock.value & (1 << hitLayer)) > 0)
            {
                Block block = hit.collider.GetComponent<Block>();
                location = block.spawnPos.position;
            }
            else if ((whatIsGround.value & (1 << hitLayer)) > 0)
            {
                location = hit.point;
            }
            else
            {
                location = _indicatorLastPos;
            }
            _indicatorLastPos = hit.point;
            hitObject = hit.collider.gameObject;
        }
        else
        {
            location = _indicatorLastPos;
        }
        return  (location, hitObject);
    }
    
    
    
    
}
