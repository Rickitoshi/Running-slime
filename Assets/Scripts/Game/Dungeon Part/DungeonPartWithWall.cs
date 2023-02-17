using Game.Buildings;
using Game.Traps;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Dungeon_Part
{
    public class DungeonPartWithWall: DungeonPart
    {
        [SerializeField] private ButtonData[] buttonData;
        
        private MovingWallVertical _wall;
        private Gun _gun;
        private HintSign hintSign;
        private DungeonButton[] _buttons;
        private ButtonType _correctButton;

        private void Awake()
        {
            _wall = GetComponentInChildren<MovingWallVertical>();
            hintSign = GetComponentInChildren<HintSign>();
            _gun = GetComponentInChildren<Gun>();
            _buttons = GetComponentsInChildren<DungeonButton>();
        }

        private void Start()
        {
            Subscribe();
            
            for (var i = 0; i < buttonData.Length; i++)
            {
                _buttons[i].Initialize(buttonData[i].Type,buttonData[i].ButtonView);
            }
        }

        private void OnEnable()
        {
            int index = Random.Range(0, _buttons.Length);
            _correctButton = (ButtonType)index;
            hintSign.Initialize(buttonData[index].SignView);
        }

        private void OnDisable()
        {
            foreach (var button in _buttons)
            {
                button.Reset();
            }
            
            _wall.Reset();
            _gun.Reset();
        }

        private void OnDestroy()
        {
            Unsubscribe();
        }

        private void Subscribe()
        {
            foreach (var button in _buttons)
            {
                button.OnPressed += OnButtonPressed;
            }
        }
        
        private void Unsubscribe()
        {
            foreach (var button in _buttons)
            {
                button.OnPressed -= OnButtonPressed;
            }
        }

        private void OnButtonPressed(ButtonType type)
        {
            if (type == _correctButton)
            {
                _wall.Disarm();
            }
            else
            {
                _gun.Shoot();
            }
        }
    }
}