using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class TowerManager : MonoBehaviour
{
    // static variables
    public static TowerManager Instance;
    // Inspector variables
    [SerializeField] private Tower indicatorTower;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask whatIsBlock;
    [SerializeField] private LayerMask whatIsTower;
    // public variables
    public Tower[] normalTowerVersions;
    public Tower[] fireTowerVersions;
    public Tower[] iceTowerVersions;
    public Tower[] electronicTowerVersions;
    public Tower[] magicTowerVersions;
    [HideInInspector] public Tower selectedTower; 
    [HideInInspector] public bool isPlacing;
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
            indicatorTower.transform.position = GetMousePositionWithRaycast();
            Ray ray = new Ray(indicatorTower.transform.position + new Vector3(0, -2.0f, 0),
                Vector3.up);
            if (Physics.Raycast(ray,out RaycastHit hit,10.0f, whatIsBlock))
            {
                Block block = hit.collider.GetComponent<Block>();
                indicatorTower.transform.position = block.spawnPos.position;
            }
        }
        
        // 마우스 클릭을 감지
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                Debug.Log("레이 종료");
                return;
            }
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            { 
                // 클릭된 객체가 Tower일 때
                if ((whatIsTower.value & (1 << hit.collider.gameObject.layer)) > 0)
                { 
                    Debug.Log("타워");
                    Tower clickedTower = hit.collider.GetComponent<Tower>();
                    if (clickedTower == selectedTower)
                    { 
                        UIManager.Instance.CloseTowerDetailsPanel();
                    }
                    else
                    { 
                        UIManager.Instance.CloseTowerDetailsPanel();
                        UIManager.Instance.OpenTowerDetailsPanel(clickedTower);
                    }
                }
                // placing 상태이고, 클릭된 객체가 Block일 때
                else if (isPlacing && (whatIsBlock.value & (1 << hit.collider.gameObject.layer)) > 0)
                {
                    Block block = hit.collider.GetComponent<Block>();
                    ConstructTower(block);
                    isPlacing = false;
                }
                // 그 외의 것들이 클릭 되었을 때
                else 
                {
                    UIManager.Instance.CloseTowerDetailsPanel();
                }
            }
            else
            { 
                UIManager.Instance.CloseTowerDetailsPanel();
            }
        }
    }

    private void ConstructTower(Block block)
    {
        if (MoneyManager.Instance.SpendMoney(_activeTower.cost))
        {
            Instantiate(_activeTower, indicatorTower.transform.position, indicatorTower.transform.rotation);
            indicatorTower.gameObject.SetActive(false);
            block.isExisted = true;
            isPlacing = false;
        }
    }
    
    public void StartTowerPlacement(Tower towerToPlace)
    {
        _activeTower = towerToPlace;
        Destroy(indicatorTower.gameObject);
        indicatorTower = Instantiate(towerToPlace);
        indicatorTower.SetupIndicatorTower();
        isPlacing = true;
        UIManager.Instance.CloseTowerDetailsPanel();
    }

    public void UpgradeTower()
    {
        selectedTower.Upgrade();
    }
    
    
    private Vector3 GetMousePositionWithRaycast() // 마우스 위치값 받아오는 함수
    {
        Vector3 location = Vector3.zero;
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out RaycastHit hit, 200.0f, whatIsGround))
        {
            indicatorTower.gameObject.SetActive(true);
            location = hit.point;
        }
        else
        {
            indicatorTower.gameObject.SetActive(false);
        }
        return location;
    }
    
}