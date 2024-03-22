using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager main;

    [Header("References")]
    [SerializeField] private Tower[] towers;

    private int selectedTowerIndex = 0;

    private void Awake()
    {
        main = this;
    }

    public Tower GetTowerPrefab()
    {
        return towers[selectedTowerIndex];
    }

    public void SetSelectTower(int _selectedTower)
    {
        selectedTowerIndex = _selectedTower;
    }
}
