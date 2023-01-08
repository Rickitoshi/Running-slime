using UnityEngine;

namespace Game.Player
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/Game/PlayerConfig", order = 0)]
    public class PlayerConfig : ScriptableObject
    {
        [SerializeField] private float jumpDistance;
        [SerializeField] private float jumpDuration;
        [SerializeField] private float jumpHeight;
        [SerializeField] private float strafeDistance;
        [SerializeField] private float strafeDuration;

        public float JumpDistance => jumpDistance;

        public float JumpDuration => jumpDuration;

        public float JumpHeight => jumpHeight;

        public float StrafeDistance => strafeDistance;

        public float StrafeDuration => strafeDuration;
    }
}