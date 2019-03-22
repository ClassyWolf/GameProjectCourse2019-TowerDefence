using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    BuildManager buildManager;
    public bool selectedArcher = false;
    public bool selectedMage = false;
    public bool selectedSlow = false;
    public bool selectedBoost = false;


    void Start()
    {
        buildManager = BuildManager.instance;
    }

    //Select towers from shopmenu
    public void PurchaseArcherTower()
    {
        Debug.Log("Archer Tower selected");
        buildManager.SetTowerToBuild(buildManager.archerTowerPrefab);
        selectedArcher = true;
        selectedMage = false;
        selectedSlow = false;
        selectedBoost = false;
}

    public void PurchaseMageTower()
    {
        Debug.Log("Mage Tower selected");
        buildManager.SetTowerToBuild(buildManager.mageTowerPrefap);
        selectedArcher = false;
        selectedMage = true;
        selectedSlow = false;
        selectedBoost = false;
    }

    public void PurchaseSlowTower()
    {
        Debug.Log("Slow Tower selected");
        buildManager.SetTowerToBuild(buildManager.slowTowerPrefap);
        selectedArcher = false;
        selectedMage = false;
        selectedSlow = true;
        selectedBoost = false;
    }

    public void PurchaseBoost()
    {
        Debug.Log("Boost selected");
                
        selectedArcher = false;
        selectedMage = false;
        selectedSlow = false;
        selectedBoost = true;


    }
}
