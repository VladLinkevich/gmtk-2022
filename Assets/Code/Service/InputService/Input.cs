using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Code.Services.InputService
{
    public interface IInputInverser
    {
        void Inverse();
    }

    public class Input : IInput, IInputInverser, ITickable
    {

        private bool _pressed = false;

        public Vector2 Offset { get; }
        public bool Pressed => _pressed;

        private delegate void TouchAction();
        private TouchAction _began;
        private TouchAction _finished;

        public Input()
        {
            _began = () => _pressed = true;
            _finished = () => _pressed = false;
        }

        public void Inverse()
        {
            (_began, _finished) = (_finished, _began);
            _pressed = !_pressed;
        }

        public void Tick()
        {
#if  (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
            MobileInput();
#else
            StandaloneInput();
#endif            
        }

#if  (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
        private void MobileInput()
        {
            if (UnityEngine.Input.touchCount == 0) return;
            Touch touch = UnityEngine.Input.touches[0];
            switch (touch)
            {
                case {phase: TouchPhase.Began}: _began(); break;
                case {phase: TouchPhase.Ended}: _finished();   break;
            }
        }
#endif

        private void StandaloneInput()
        {
            if (UnityEngine.Input.GetMouseButtonDown(0) ||
                UnityEngine.Input.GetKeyDown(KeyCode.Space))
                _began();
            if (UnityEngine.Input.GetMouseButtonUp(0) ||
                UnityEngine.Input.GetKeyUp(KeyCode.Space))
                _finished();
        }
    }

    public class StandaloneInput : IInput, IInputInverser, ITickable
    {
        private bool _isInverse;
        
        private Vector3 _clickPoint;

        public Vector2 Offset { get; private set; }
        public bool Pressed { get; private set; }
        
        public void Inverse()
        {
            _isInverse = !_isInverse;
        }

        public void Tick()
        {
            Pressed = IsPressed() ? !_isInverse : _isInverse;
            UpdateOffset();
        }

        private void UpdateOffset()
        {
            if (UnityEngine.Input.GetMouseButtonDown(0))
                _clickPoint = UnityEngine.Input.mousePosition;

            if (IsPressed())
                Offset = UnityEngine.Input.mousePosition - _clickPoint;
        }

        private static bool IsPressed() =>
            (UnityEngine.Input.GetMouseButton(0) ||
             UnityEngine.Input.GetKey(KeyCode.Space)) &&
            !EventSystem.current.IsPointerOverGameObject();
    }
    
    public class MobileInput : IInput, IInputInverser, ITickable
    {
        private Vector3 _clickPoint;
        private bool _isInverse;

        public Vector2 Offset { get; private set; }
        public bool Pressed { get; private set; }
        public void Inverse()
        {
            _isInverse = !_isInverse;
        }

        public void Tick()
        {
            Pressed = IsPressed() ? !_isInverse : _isInverse;
            UpdateOffset();
        }

        private void UpdateOffset()
        {
            if (BeganTouch())
                _clickPoint = UnityEngine.Input.mousePosition;

            if (IsPressed())
                Offset = UnityEngine.Input.mousePosition - _clickPoint;
        }

        private bool BeganTouch() =>
            IsPressed() &&
            UnityEngine.Input.touches[0].phase == TouchPhase.Began;

        private bool IsPressed() => 
            UnityEngine.Input.touchCount != 0 &&
            !EventSystem.current.IsPointerOverGameObject(UnityEngine.Input.touches[0].fingerId);
    }
}