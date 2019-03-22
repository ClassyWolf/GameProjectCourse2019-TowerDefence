using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageAmmo : MonoBehaviour
{
    private Transform target;

    public float speed = 10f;
    public float boomSize = 0f;
    public GameObject ammoEffect;
   
    
    public void Seek(Transform _target)
    {
        target = _target;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            GameObject effectIns = (GameObject)Instantiate(ammoEffect, transform.position, transform.rotation);
            Destroy(effectIns, 2f);
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    void HitTarget()
    {
        GameObject effectIns = (GameObject)Instantiate(ammoEffect, transform.position, transform.rotation);
        Destroy(effectIns, 2f);
        
        Boom();
                      
        Destroy(gameObject);
    }

        void Boom()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, boomSize);
            foreach (Collider2D collider in colliders)
            {
                if (collider.tag == "Enemy")
                {
                    Damage(collider.transform);
                }
            }
        }

    void Damage (Transform enemy)
    {
        //Destroy(enemy.gameObject);       
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, boomSize);
    }
        
}

