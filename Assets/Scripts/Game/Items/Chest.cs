using System;
using UnityEngine;
using DG.Tweening;

namespace Game.Items
{
    public class Chest : MonoBehaviour
    {
        [SerializeField] private GameObject cap;
        [SerializeField] private float animationDuration;
        [SerializeField] private int endAngle;

        private Vector3 _euler;
        
        private void Start()
        {
            _euler = new Vector3(0, 0, endAngle);
        }

        public void Open(bool useAnimation)
        {
            if (!useAnimation)
            {
                cap.transform.localRotation=Quaternion.Euler(_euler);
            }
            else
            {
                cap.transform.DOLocalRotate(_euler, animationDuration).SetEase(Ease.OutCubic);
            }
        }

        public void Close(bool useAnimation)
        {
            if (!useAnimation)
            {
                cap.transform.localRotation = Quaternion.identity;
            }
            else
            {
                cap.transform.DOLocalRotate(Vector3.zero, animationDuration).SetEase(Ease.InCubic);
            }
        }
    }
}