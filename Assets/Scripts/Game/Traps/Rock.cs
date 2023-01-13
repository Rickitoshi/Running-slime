using Game.Traps;
using UnityEngine;

public class Rock : Obstacle
{
    [SerializeField] private float moveSpeed;
   
    void Update()
    {
        transform.position += new Vector3(0, 0, moveSpeed) * Time.deltaTime;
    }
}
