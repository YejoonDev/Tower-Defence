using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerButton : MonoBehaviour
{
    public Tower towerToPlace;

    public void SelectTower()
    {
        TowerManager.Instance.StartTowerPlacement(towerToPlace);
        Debug.Log("Pressed");
    }
}
