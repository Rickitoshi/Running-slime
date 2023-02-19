using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.UI
{
    public class GraphicsToggle : MonoBehaviour,IPointerClickHandler
    {
        [SerializeField] private GameObject graphicOn;
        [SerializeField] private GameObject graphicOff;

        private GameObject _currentObject;
        public event Action<bool> OnValueChanged;
        private bool _isOn;

        private void Awake()
        {
            graphicOn.SetActive(false);
            graphicOff.SetActive(false);
        }

        private void Start()
        {
            _isOn = true;
            Switch(graphicOn);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Switch(_isOn ? graphicOff : graphicOn);
            _isOn = !_isOn;
            OnValueChanged?.Invoke(_isOn);
        }
        
        private void Switch(GameObject newObject)
        {
            if (_currentObject)
            {
                _currentObject.SetActive(false);
            }

            _currentObject = newObject;
            _currentObject.SetActive(true);
        }
    }
}