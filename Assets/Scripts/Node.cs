using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Color hoverColorNegative;
    public Vector3 positionOffset;

    private GameObject tower;

    private Renderer rend;
    private Color startColor;

    private Shop shop;

    private Economy economy;
    [SerializeField] private int archerTowerCost = 100;
    [SerializeField] private int mageTowerCost = 100;
    [SerializeField] private int slowTowerCost = 100;

    private int price;

    BuildManager buildManager;


    void Start()
    {
        
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        economy = GameObject.Find("GameMaster").GetComponent<Economy>();
        shop = GameObject.Find("Shop").GetComponent<Shop>();

        buildManager = BuildManager.instance;              
        
    }

    private void Update()
    {
        if(shop.selectedArcher == true)
        {
            price = archerTowerCost;
        }

        else if(shop.selectedMage == true)
        {
            price = mageTowerCost;
        }

        else if(shop.selectedSlow == true)
        {
            price = slowTowerCost;
        }

        else{}
    }


    void OnMouseDown()
    {
        if (shop.selectedArcher == true || shop.selectedMage == true || shop.selectedSlow == true)
        {

        
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (buildManager.GetTowerToBuild() == null)
            return;

        if (tower != null)
        {
            Debug.Log("Nocando");
            return;
        }

        if (economy.Gold >= price)
        {
            GameObject towerToBuild = BuildManager.instance.GetTowerToBuild();
            tower = (GameObject)Instantiate(towerToBuild, transform.position + positionOffset, transform.rotation);
            economy.Gold -= price;
        }

        else
        {
            Debug.Log("Not enough money");
            return;
        }
        }

    }
    

            

    void OnMouseEnter()
    {
        
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (tower != null)
        {
            GetComponent<Renderer>().material.color = hoverColorNegative;
            return;
        }

        GetComponent<Renderer>().material.color = hoverColor;
    }

    void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
