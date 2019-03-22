using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Economy : MonoBehaviour
{
    public TMPro.TextMeshProUGUI goldLabel;
    private int gold;
    [SerializeField] private int startingGold = 100;

    public int Gold
    {
        get
        {
            return gold;
        }
        set
        {
            gold = value;
            goldLabel.text = gold.ToString();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Gold = startingGold;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
