using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MainHallStatusScript : MonoBehaviour
{

    public float mainHallHealth;
    public GameObject smokeEffect;

    [SerializeField] private GameOverScript gameOver;
    [SerializeField] private DamageScript damageScript;
    [SerializeField] private SpriteRenderer mainHallSprite;

    private float mainHallMaxHealth;
    private SoundManager soundManager;

    private void Start()
    {

        mainHallMaxHealth = mainHallHealth;
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }


    public void Update()
    {

        if (mainHallHealth <= mainHallMaxHealth * 0.75 && mainHallHealth > mainHallMaxHealth * 0.5)
        {

            mainHallSprite.color = new Color(0.9f, 0.6f, 0.6f);

        }

        if (mainHallHealth <= mainHallMaxHealth * 0.5 && mainHallHealth > mainHallMaxHealth * 0.25)
        {

            smokeEffect.SetActive(true);
            mainHallSprite.color = new Color(0.9f, 0.2f, 0.2f);

        }

        if (mainHallHealth <= mainHallMaxHealth * 0.25)
        {

            mainHallSprite.color = new Color(0.8f, 0, 0);

        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy1"))
        {

            if (mainHallHealth - damageScript.enemy1Damage > 0)
            {
                mainHallHealth = mainHallHealth - damageScript.enemy1Damage;
                Debug.Log("Main Hall health: " + mainHallHealth);
                soundManager.PlayEfx(3);
            }

            else
            {

                mainHallHealth = 0;
                gameOver.GameLost();
                Destroy(this.gameObject);

            }

        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy2"))
        {

            if (mainHallHealth - damageScript.enemy2Damage > 0)
            {

                mainHallHealth = mainHallHealth - damageScript.enemy2Damage;
                Debug.Log("Main Hall health: " + mainHallHealth);
                soundManager.PlayEfx(3);
            }

            else
            {

                mainHallHealth = 0;
                gameOver.GameLost();
                Destroy(this.gameObject);

            }
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy3"))
        {

            if (mainHallHealth - damageScript.enemy3Damage > 0)
            {

                mainHallHealth = mainHallHealth - damageScript.enemy3Damage;
                Debug.Log("Main Hall health: " + mainHallHealth);
                soundManager.PlayEfx(3);
            }

            else
            {

                mainHallHealth = 0;
                gameOver.GameLost();
                Destroy(this.gameObject);

            }

        }

    }

}
