using System.Collections.Generic;
using UnityEngine;

public class DestructionSystem : MonoBehaviour
{
    private Rigidbody[] _rigidbody;
    private readonly List<Vector3> _partsDefaultPosition = new();

    private void Awake()
    {
        _rigidbody = GetComponentsInChildren<Rigidbody>();

        foreach (var body in _rigidbody)
        {
            _partsDefaultPosition.Add(body.transform.localPosition);
        }
        
        gameObject.AddComponent<Rigidbody>().useGravity = false;
    }

    private void Start()
    {
        SetKinematic(true);
    }
    
    private void SetKinematic(bool value)
    {
        foreach (var body in _rigidbody)
        {
            body.isKinematic = value;
        }
    }

    public void Destruction()
    {
        SetKinematic(false);
    }
    
    public void Destruction(float impulseForce, Vector3 direction)
    {
        foreach (var body in _rigidbody)
        {
            body.isKinematic = false;
            body.AddForce(direction * impulseForce, ForceMode.Impulse);
        }
    }

    public void Recovery()
    {
        SetKinematic(true);
        
        for (int i = 0; i < _rigidbody.Length; i++)
        {
            _rigidbody[i].transform.localPosition = _partsDefaultPosition[i];
            _rigidbody[i].transform.localRotation = Quaternion.identity;
        }
    }
}
