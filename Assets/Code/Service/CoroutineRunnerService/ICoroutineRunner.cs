using System.Collections;
using UnityEngine;

namespace Code.Services.CoroutineRunnerService
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
        void StopCoroutine(Coroutine coroutine);
    }
}