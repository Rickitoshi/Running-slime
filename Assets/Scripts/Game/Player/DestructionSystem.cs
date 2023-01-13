using UnityEngine;

public class DestructionSystem : MonoBehaviour
{
    private Rigidbody[] _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponentsInChildren<Rigidbody>();
    }

    private void Start()
    {
        Recovery();
    }

    public void Destruction()
    {
        foreach (var body in _rigidbody)
        {
            body.isKinematic = false;
        }
    }
    
    public void Explosion(float impulseForce)
    {
        foreach (var body in _rigidbody)
        {
            body.isKinematic = false;
            Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(0, 1f), Random.Range(-1f, 1f));
            body.AddForce(randomDirection * impulseForce, ForceMode.Impulse);
        }
    }

    public void Recovery()
    {
        foreach (var body in _rigidbody)
        {
            body.isKinematic = true;
            Transform bodyTransform = body.transform;
            bodyTransform.localPosition = Vector3.zero;
            bodyTransform.localRotation = Quaternion.identity;
        }
    }
}
