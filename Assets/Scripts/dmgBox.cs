using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dmgBox : MonoBehaviour
{
    [SerializeField]
    private float waitTime = 4.0f;

    [SerializeField]
    private float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > waitTime)
        {
            
        }
    }

    private void BlowUp()
    {
        Destroy(this.gameObject);
    }
}
