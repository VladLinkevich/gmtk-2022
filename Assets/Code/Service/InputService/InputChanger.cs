using System;
using System.Collections;
using Code.Services.CoroutineRunnerService;
using UnityEngine;

namespace Code.Services.InputService
{
    public interface IInputChanger
    {
        void SetupInput(InputChanger.InputType inputType);
    }

    public class InputChanger : IInputChanger
    {
        private readonly IInputInverser _inverser;
        private readonly ICoroutineRunner _coroutine;
        private readonly Settings _settings;

        private bool _inverse;
        private Coroutine _timeCoroutine;

        public InputChanger(
            IInputInverser inverser,
            ICoroutineRunner coroutine,
            Settings settings)
        {
            _inverser = inverser;
            _coroutine = coroutine;
            _settings = settings;

            SetupInput(_settings.InputType);
        }

        public void SetupInput(InputType inputType)
        {
            if (_timeCoroutine != null)
                _coroutine.StopCoroutine(_timeCoroutine);
            
            switch (inputType)
            {
                case InputType.Classic when _inverse == true:
                case InputType.Inverse when _inverse == false: 
                    ChangeInput(); 
                    break;
                case InputType.Time:
                    _timeCoroutine = _coroutine.StartCoroutine(DelayChangeInput()); 
                    break;
            }
        }

        private IEnumerator DelayChangeInput()
        {
            while (true)
            {
                yield return new WaitForSeconds(_settings.InvertTime);
                ChangeInput();
            }
        }

        private void ChangeInput()
        {
            _inverse = !_inverse;
            _inverser.Inverse();
        }

        [Serializable]
        public class Settings
        {
            public InputType InputType;
            public float InvertTime;
        }

        public enum InputType
        {
            Classic,
            Inverse,
            Time
        }
    }
    
}