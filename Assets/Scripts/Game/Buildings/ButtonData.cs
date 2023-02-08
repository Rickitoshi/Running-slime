
using UnityEngine;

namespace Game.Buildings
{
    [CreateAssetMenu(fileName = "ButtonData", menuName = "Configs/Game/ButtonData", order = 0)]
    public class ButtonData : ScriptableObject
    {
        [SerializeField] private Mesh buttonView;
        [SerializeField] private ButtonType buttonType;
        [SerializeField] private Mesh stoneView;


        public Mesh ButtonView => buttonView;

        public ButtonType Type => buttonType;

        public Mesh StoneView => stoneView;
    }
}