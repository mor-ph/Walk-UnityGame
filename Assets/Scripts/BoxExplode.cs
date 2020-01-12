using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxExplode : MonoBehaviour
{
    public GameObject[] prefabs;
    public Vector3 spawnOffset = Vector2.zero;

    public float minOpenDelay = 2.7f;
    public float maxOpenDelay = 3.0f;

    public int damage = 10;
    
    public static bool isContactingPlayer = false;
    

    [SerializeField]
    Animator anim;

    
    void Start()
    {       
        isContactingPlayer = false;
        anim = GetComponent<Animator>();
        Invoke("DoSpawn", Random.Range(minOpenDelay, maxOpenDelay));
    }

    public void Update()
    {
       
    }

    void DoSpawn()
    {
        anim.SetTrigger("boom");
        if (prefabs.Length > 0)
        {
            Instantiate(prefabs[Random.Range(0, prefabs.Length)], transform.position + spawnOffset, Quaternion.identity);
        }
        //*** tochno v tozi moment se puska animaciikata
        Invoke("CircleColliderEnabled", 0.2f);
        Destroy(gameObject, 1f);
        
        
    }

    // *** enable circle collider za izbuhvane
    void CircleColliderEnabled()
    {
        CircleCollider2D col = GetComponent<CircleCollider2D>();
        col.enabled = true;
    }
    //*** proverka dali player.collider dokosva box.collider
    private void OnTriggerEnter2D(Collider2D col)
    {
        Player player = col.transform.GetComponent<Player>();
        if (player == null) return;
        isContactingPlayer = true;
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        Player player = col.transform.GetComponent<Player>();
        if (player == null) return;
        isContactingPlayer = false;
    }
    //----------------------------------------------------------
}
