using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongueCollider : MonoBehaviour
{

    // *** Copy-paste from Bell(Script)
    public static bool isContactingPlayer = false;


    private void Awake()
    {
        isContactingPlayer = false;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player player = collision.transform.GetComponent<Player>();
        if (player == null) return;
        isContactingPlayer = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Player player = collision.transform.GetComponent<Player>();
        if (player == null) return;
        isContactingPlayer = false;
    }
}
