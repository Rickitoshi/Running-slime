using System;
using Game.Player;
using UnityEngine;

public class DungeonPart : MonoBehaviour
{
    [SerializeField] private float lenght = 4;
    public float Lenght => lenght;

    public void Activate(Vector3 position)
    {
        transform.position = position;
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
