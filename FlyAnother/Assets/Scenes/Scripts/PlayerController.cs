using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 3;    
            
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }
}
