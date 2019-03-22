using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableColliderWithScript : MonoBehaviour
{

    public float delayTime = 0.2f;

    private Collider2D objectsCollider;


    public void Start()
    {

        objectsCollider = GetComponent<Collider2D>();

    }


    public void Update()
    {

        StartCoroutine(Delay());

    }


    public IEnumerator Delay()
    {

        yield return new WaitForSeconds(delayTime);
        objectsCollider.enabled = false;

    }

}
