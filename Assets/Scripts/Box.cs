using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{

    public GameObject[] prefabs;
    public Vector3 spawnOffset = Vector2.zero;

    public float minOpenDelay = 2.7f;
    public float maxOpenDelay = 3.0f;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        Invoke("DoSpawn", Random.Range(minOpenDelay, maxOpenDelay));   
    }

    void DoSpawn()
    {
        //Play box open animation
        anim.SetBool("Open", true);
        if(prefabs.Length > 0)
        {
            Instantiate(prefabs[Random.Range(0, prefabs.Length)], transform.position + spawnOffset, Quaternion.identity);
        }
        Destroy(gameObject, 0f);
    }
}
