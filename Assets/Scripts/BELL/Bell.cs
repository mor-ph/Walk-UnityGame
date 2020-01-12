using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bell : MonoBehaviour
{
    public static bool isContactingPlayer = false;


    private void Awake()
    {
        isContactingPlayer = false;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.transform.GetComponent<Player>();
        if (player == null) return;
        isContactingPlayer = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Player player = collision.transform.GetComponent<Player>();
        if (player == null) return;
        isContactingPlayer = false;
    }

}
