using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public enum ModeState
{
    Normal,
    Wave,
    Build,
}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public List<GameObject> trackedEnemies;
    public ModeState currentMode;
    public int currentRound;
    public LayerMask unitLayerMask;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        trackedEnemies = new List<GameObject>();
    }

    private void Start()
    {
        currentMode = ModeState.Normal;
        currentRound = 0;
    }

    private void Update()
    {
        if (currentMode != ModeState.Build && trackedEnemies.Count == 0)
        {
            currentMode = ModeState.Normal;
            UIManager.Instance.modeTitle.text = "Normal";
        }
        
        

        if (currentMode != ModeState.Wave && Input.GetKeyDown(KeyCode.B))
        {
            SetBuildMode();
        }
        
        
    }

    public void SetBuildMode()
    {
        if (currentMode != ModeState.Build)
        {
            currentMode = ModeState.Build;
            UIManager.Instance.modeTitle.text = "Build Mode";
        }
        else if (currentMode == ModeState.Build)
        {
            currentMode = ModeState.Normal;
            UIManager.Instance.modeTitle.text = "";
        }
    }
    
    
    
    
    
    
}
