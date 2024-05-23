using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class TowerManager : MonoBehaviour
{
    // static variables
    public static TowerManager Instance;
    // readonly variables
    private readonly Vector3 _offMapVector = new Vector3(100, 100, 100);
    // Inspector variables
    [SerializeField] private Transform indicator;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask whatIsObstacle;
    [SerializeField] private LayerMask whatIsBlock;
    // public variables
    public bool isPlacing;
    // private variables
    private Camera _mainCamera; 
    private Tower _activeTower;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            _mainCamera = Camera.main;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (isPlacing)
        {
            indicator.transform.position = GetGridPosition();

            Ray ray = new Ray(indicator.position + new Vector3(0, -2.0f, 0), Vector3.up);
            RaycastHit[] hits = new RaycastHit[3];
            
            int hitCount = Physics.RaycastNonAlloc(ray, hits, 10.0f);
            for (int i = 0; i < hitCount; i++)
            {
                GameObject hitGameObject = hits[i].collider.gameObject;
                if ((whatIsBlock.value & (1 <<hitGameObject.layer)) > 0)
                {    
                    hitGameObject.SetActive(true);
                    Block block = hitGameObject.GetComponent<Block>();
                    indicator.position = block.spawnPos.position;
                    
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (!block.isExisted)
                        {
                            indicator.gameObject.SetActive(false);
                            Instantiate(_activeTower, indicator.transform.position, indicator.transform.rotation);
                            block.isExisted = true;
                            isPlacing = false;
                        }
                        else
                        {
                            UIManager.Instance.DisplayAlarmText("We can't build there");
                        }
                    }
                    
                }
                else if ((whatIsObstacle.value & (1 << hitGameObject.layer)) > 0)
                {
                    hitGameObject.SetActive(false);
                }
            }
        }
    }

    public void StartTowerPlacement(Tower towerToPlace)
    {
        isPlacing = true;
        _activeTower = towerToPlace;
        
        Destroy(indicator.gameObject);
        Tower placeTower = Instantiate(towerToPlace);
        indicator = placeTower.transform;
        placeTower.InitializeIndicatorTower();
    }

    private Vector3 GetGridPosition()
    {
        Vector3 location = _offMapVector;
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out RaycastHit hit, 200.0f, whatIsGround))
        {
            indicator.gameObject.SetActive(true);
            location = hit.point;
        }
        else
        {
            indicator.gameObject.SetActive(false);
        }
        return location;
    }
    
    
    
    
}