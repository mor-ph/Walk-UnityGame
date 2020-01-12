sing System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 1f;
    public float offSet;
    Camera cam;
    public Player player;

    [SerializeField]
    private float xMin;
    [SerializeField]
    private float xMax;
    [SerializeField]
    private float yMin;
    [SerializeField]
    private float yMax;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        transform.position = new Vector3(Mathf.Clamp(target.position.x, xMin, xMax), Mathf.Clamp(target.position.y, yMin, yMax), offSet);
        // camera zhoom()
       /* if (target.transform.position.y <= transform.position.y)
        {
            cam.orthographicSize -=Time.deltaTime*5 ;
        }
        if (player.grounded)
        {
            if (cam.orthographicSize <= 10)
            {
                cam.orthographicSize += Time.deltaTime * 10;
            }
            else cam.orthographicSize = 10;


        }*/
        
        
            //target.position + offSet;
    }
}
