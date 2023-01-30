using UnityEngine;

public abstract class DungeonPart : MonoBehaviour
{
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
