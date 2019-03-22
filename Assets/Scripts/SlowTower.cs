using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowTower : MonoBehaviour
{
    public float range = 3f;
    public float fireRate = 1f;
    public float lifetime;

    private float fireCountdown = 0f;

    public string enemyTag = "Enemy";
    public GameObject slowAmmoPrefab;
    public GameObject towerBOOM;
    public Transform firePoint;
    public Transform target;

    SpriteRenderer m_SpriteRenderer;
    Color color;    
    public GameObject boostFlame;

    private Economy economy;
    private SoundManager soundManager;
    public int upgradeCost = 600;

    private Shop shop;
    private bool upraged = false;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        economy = GameObject.Find("GameMaster").GetComponent<Economy>();
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        shop = GameObject.Find("Shop").GetComponent<Shop>();
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }

        else
        {
            target = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
            return;

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    void Shoot()
    {
        soundManager.PlayEfx(5);
        GameObject slowAmmoGO = (GameObject)Instantiate(slowAmmoPrefab, firePoint.position, firePoint.rotation);
        SlowAmmo slowAmmo = slowAmmoGO.GetComponent<SlowAmmo>();

        if (slowAmmo != null)
            slowAmmo.Seek(target);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    void OnMouseDown()
    {
        if (shop.selectedBoost == true && upraged == false)
        {
            if (economy.Gold >= upgradeCost)
            {
                soundManager.PlayEfx(8);
                upraged = true;
                
                boostFlame.SetActive(true);

                range = 4f;
                fireRate = 3f;

                GameObject towerBoom = (GameObject)Instantiate(towerBOOM, transform.position, transform.rotation);
                Destroy(towerBoom, 7f);

                Destroy(gameObject, lifetime);

                economy.Gold -= upgradeCost;
            }

            else
            {
                Debug.Log("No cash");
            }
        }
    }
}
