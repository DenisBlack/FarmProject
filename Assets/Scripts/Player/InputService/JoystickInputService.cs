using UnityEngine;

namespace Player.InputService
{
    public class JoystickInputService : InputService
    {
        //private readonly FixedJoystick _fixedJoystick;
        public JoystickInputService()
        {
           // _fixedJoystick = Object.FindObjectOfType<FixedJoystick>();
        }

        public override Vector2 Axis => new Vector2();//new Vector2(_fixedJoystick.Horizontal, _fixedJoystick.Vertical));
    }
}