using System;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
   
    void Update()
    {
        transform.position += new Vector3(0, 0, moveSpeed) * Time.deltaTime;
    }
}
