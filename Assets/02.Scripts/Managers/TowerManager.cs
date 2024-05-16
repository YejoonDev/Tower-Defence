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
    private readonly Vector3 _offMapVector = new Vector3(100, 100, 100);
    
    public static TowerManager Instance;
    public GameObject[] deployableTowers;
    
    public LayerMask whatIsGround;
    public LayerMask whatIsObstacle;
    public LayerMask whatIsBlock;
    
    [SerializeField] private Transform indicator;
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
                // Debug.Log("레이캐스트 시작 : " + hitGameObject.name);
                if ((whatIsBlock.value & (1 <<hitGameObject.layer)) > 0)
                {    
                    hitGameObject.SetActive(true);
                    Block block = hitGameObject.GetComponent<Block>();
                    indicator.position = block.spawnPos.position;
                    if (Input.GetMouseButtonDown(0) && !block.isExisted)
                    {
                        Instantiate(_activeTower, indicator.transform.position, indicator.transform.rotation);
                        indicator.gameObject.SetActive(false);
                        isPlacing = false;
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
        placeTower.enabled = false;
        placeTower.GetComponent<Collider>().enabled = false;
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
