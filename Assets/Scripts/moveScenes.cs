using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class moveScenes : MonoBehaviour
{
    [SerializeField]
    private string newLevel;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            SceneManager.LoadScene(newLevel);
        }
    }
}
