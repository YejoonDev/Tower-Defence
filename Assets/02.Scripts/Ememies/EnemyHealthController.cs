using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthController : MonoBehaviour
{
    // inspector variables
    [SerializeField] private Slider healthSlider;
    // serialize variables
    [SerializeField] private int totalHealth;
    // private variables
    private Camera _mainCamera;
    private EnemyController _enemyController;
    private int _currentHealth;
    
    private void Awake()
    {
        _enemyController = GetComponent<EnemyController>();
        _currentHealth = totalHealth;
        healthSlider.maxValue = totalHealth;
        healthSlider.value = _currentHealth;
    }
    private void Start()
    {
        _mainCamera = Camera.main;
    }

    void Update()
    {
        healthSlider.transform.rotation = _mainCamera.transform.rotation;
    }
    
    public void TakeDamage(int damageAmount)
    {
        _currentHealth -= damageAmount;
        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            _enemyController.DestroyEnemy();
        }
        healthSlider.value = _currentHealth;
    }
}
