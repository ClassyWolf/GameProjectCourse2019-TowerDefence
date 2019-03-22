using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MoveEnemy))]


public class EnemyStatusScript : MonoBehaviour
{

    public float enemyHealth;
    public float magicResistance = 1;
    public float physicalResistance = 1;
    public float slow = 0.5f;
    public float slowDuration = 3f;
    public float normalSpeed;
    public GameObject bloodBURST;

    [SerializeField] private DamageScript damageScript;
    [SerializeField] private int goldGained = 100;

    private Economy economy;
    private SoundManager soundManager;
    private SpriteRenderer enemySprite;
    private MoveEnemy moveEnemy;
    private float enemyMaxHealth;


    private void Start()
    {

        economy = GameObject.Find("GameMaster").GetComponent<Economy>();
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        moveEnemy = GetComponent<MoveEnemy>();
        enemyMaxHealth = enemyHealth;
        normalSpeed = moveEnemy.speed;
        enemySprite = GetComponentInChildren<SpriteRenderer>();

    }

    
    public void Update()
    {

        if (enemyHealth <= enemyMaxHealth * 0.75 && enemyHealth > enemyMaxHealth * 0.5)
        {
            enemySprite.color = new Color(0.9f, 0.6f, 0.6f);
        }

        if (enemyHealth <= enemyMaxHealth * 0.5 && enemyHealth > enemyMaxHealth * 0.25)
        {
            enemySprite.color = new Color(0.9f, 0.2f, 0.2f);
        }

        if (enemyHealth <= enemyMaxHealth * 0.25)
        {
            enemySprite.color = new Color(0.8f, 0, 0);
        }
        
    }
    

    // Enemy take damage
    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Arrow")
        {
            
            if (enemyHealth - damageScript.arrowDamage > 0)
            {

                Debug.Log("Enemy took physical damage");
                enemyHealth = enemyHealth - (damageScript.arrowDamage * physicalResistance);

            }

            else
            {

                GameObject bloodBurst = (GameObject)Instantiate(bloodBURST, transform.position, transform.rotation);
                Destroy(bloodBurst, 3f);

                enemyHealth = 0;
                Destroy(this.gameObject);
                economy.Gold += goldGained;

                if(gameObject.layer == LayerMask.NameToLayer("Enemy1"))
                {
                    soundManager.PlayEfx(2);
                }
                if (gameObject.layer == LayerMask.NameToLayer("Enemy2"))
                {
                    soundManager.PlayEfx(1);
                }
            }

        }
        
        if (collision.tag == "Spell")
        {

            if (enemyHealth - damageScript.spellDamage > 0)
            {

                Debug.Log("Enemy took magic damage");
                enemyHealth = enemyHealth - (damageScript.spellDamage * magicResistance);

            }

            else
            {

                enemyHealth = 0;
                Destroy(this.gameObject);
                economy.Gold += goldGained;

            }

            if (gameObject.layer == LayerMask.NameToLayer("Enemy1"))
            {
                soundManager.PlayEfx(2);
            }
            if (gameObject.layer == LayerMask.NameToLayer("Enemy2"))
            {
                soundManager.PlayEfx(1);
            }

        }
        
        if (collision.tag == "SlowingProjectile")
        {
            if (enemyHealth - damageScript.slowDamage > 0)
            {
                moveEnemy.slowed = true;
                moveEnemy.slowDuration = moveEnemy.SlowedDurationHolder;
                enemyHealth = enemyHealth - damageScript.slowDamage;
            }

            else
            {
                enemyHealth = 0;
                Destroy(this.gameObject);
                economy.Gold += goldGained;
            }

            if (gameObject.layer == LayerMask.NameToLayer("Enemy1"))
            {
                soundManager.PlayEfx(2);
            }
            if (gameObject.layer == LayerMask.NameToLayer("Enemy2"))
            {
                soundManager.PlayEfx(1);
            }
        }   
    }
}
