using UnityEngine;

namespace Player.InputService
{
    public class KeyboardInputService : InputService
    {
        public override Vector2 Axis => new(Input.GetAxis(Horizontal), Input.GetAxis(Vertical));
    }
}