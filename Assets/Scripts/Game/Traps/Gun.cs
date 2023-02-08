using System;

namespace Game.Traps
{
    public class Gun: Obstacle
    {
        private Bullet _bullet;

        private void Awake()
        {
            _bullet = GetComponentInChildren<Bullet>();
        }

        public void Shoot()
        {
            _bullet.Move();
        }

        public void Reset()
        {
            _bullet.Reset();
        }
    }
}