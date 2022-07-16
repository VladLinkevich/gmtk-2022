using System;
using UnityEngine;

namespace Code.Services.InputService
{
    public interface IInput
    {
        Vector2 Offset { get; }
        bool Pressed { get; }
    }
}