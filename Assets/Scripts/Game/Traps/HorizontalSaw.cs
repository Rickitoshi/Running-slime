using DG.Tweening;
using UnityEngine;

namespace Game.Traps
{
    public class HorizontalSaw : Obstacle
    {
        [SerializeField] private float rotateDuration = 2;
        [SerializeField] private bool isInversion;

        private void Start()
        {
            int angle = isInversion ? -360 : 360;
            transform.DORotate(Vector3.up * angle, rotateDuration, RotateMode.LocalAxisAdd).SetLoops(-1).SetEase(Ease.Linear);
        }
    }
}