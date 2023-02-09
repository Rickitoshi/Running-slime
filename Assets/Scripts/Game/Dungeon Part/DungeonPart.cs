using System;
using Game.Player;
using UnityEngine;

public class DungeonPart : MonoBehaviour
{
    public event Action OnPlayerExit;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            OnPlayerExit?.Invoke();
        }
    }

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
