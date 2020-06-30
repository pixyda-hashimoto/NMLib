using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NMLib
{
    /// <summary>
    /// 
    /// </summary>
    public interface IInputManagerCore
    {
        void Initialize();
        void Update();
        bool GetButtonDown(InputManager.Button button, int controlIndex=0 );
        bool GetButtonUp(InputManager.Button button, int controlIndex=0 );
        bool GetButton(InputManager.Button button, int controlIndex=0 );
        float GetTrigger(InputManager.Trigger trigger, int controlIndex=0);
        Vector2 GetAxis(InputManager.Axis axis, int controlIndex=0);
    };


    /// <summary>
    /// 
    /// </summary>
    public static class InputManager
    {
        public enum Button {
            Up, Down, Left, Right,
            A, B, X, Y,
            L1, R1, L2, R2,
            LStick, RStick,
            Start, Back,
            User,
        }
        public enum Axis { 
            LeftStick, 
            RightStick, 
            Dpad 
        }
        public enum Trigger { 
            LeftTrigger, 
            RightTrigger 
        }


        private static IInputManagerCore instance = null;
        static InputManager() {
//            Logger.Log("InputManager constructor");
#if UNITY_STANDALONE || UNITY_EDITOR
            instance = new InputManagerWindows();
#elif UNITY_SWITCH //&& !UNITY_EDITOR
            instance = new InputManagerNintendoSwitch();
#endif
        }

        public static void Initialize(){ instance.Initialize(); }
        public static void Update(){ instance.Update(); }
        public static bool GetButtonDown(Button button, int controlIndex=0 ){ return instance.GetButtonDown(button,controlIndex); }
        public static bool GetButtonUp(Button button, int controlIndex=0 ){ return instance.GetButtonUp(button,controlIndex); }
        public static bool GetButton(Button button, int controlIndex=0 ){ return instance.GetButton(button,controlIndex); }
        public static float GetTrigger(Trigger trigger, int controlIndex=0){ return instance.GetTrigger(trigger,controlIndex); }
        public static Vector2 GetAxis(Axis axis, int controlIndex=0){ return instance.GetAxis(axis,controlIndex); }


        public class PadState
        {
            public PadState(
                bool up, bool down, bool left, bool right,
                bool a, bool b, bool x, bool y,
                bool l1, bool r1, bool l2, bool r2,
                bool lStick, bool rStick,
                bool start, bool back, 
                Vector2 lStickAxis, Vector2 rStickAxis, Vector2 dpadAxis, 
                float lTrigger, float rTrigger 
            ) {
                this.Up = up;
                this.Down = down;
                this.Left = left;
                this.Right = right;
                this.A = a;
                this.B = b;
                this.X = x;
                this.Y = y;
                this.L1 = l1;
                this.R1 = r1;
                this.L2 = l2;
                this.R2 = r2;
                this.LStick = lStick;
                this.RStick = rStick;
                this.Start = start;
                this.Back = back;
                this.LeftStickAxis = lStickAxis;
                this.RightStickAxis = rStickAxis;
                this.DpadAxis = dpadAxis;
                this.LeftTrigger = lTrigger;
                this.RightTrigger = rTrigger;
            }

            public bool Up { get; private set; } = false;
            public bool Down { get; private set; } = false;
            public bool Left { get; private set; } = false;
            public bool Right { get; private  set; } = false;
            public bool A { get; private set; } = false;
            public bool B { get; private set; } = false;
            public bool X { get; private set; } = false;
            public bool Y { get; private set; } = false;
            public bool L1 { get; private set; } = false;
            public bool R1 { get; private set; } = false;
            public bool L2 { get; private set; } = false;
            public bool R2 { get; private set; } = false;
            public bool LStick { get; private set; } = false;
            public bool RStick { get; private set; } = false;
            public bool Start { get; private set; } = false;
            public bool Back { get; private set; } = false;

            public Vector2 LeftStickAxis = Vector2.zero;
            public Vector2 RightStickAxis = Vector2.zero;
            public Vector2 DpadAxis = Vector2.zero;

            public float LeftTrigger = 0f;
            public float RightTrigger = 0f;
        }
        public static PadState GetPadState(int controlIndex=0)
        {
            PadState state = new PadState(
                GetButton(Button.Up, controlIndex),
                GetButton(Button.Down, controlIndex),
                GetButton(Button.Left, controlIndex),
                GetButton(Button.Right, controlIndex),
                GetButton(Button.A, controlIndex),
                GetButton(Button.B, controlIndex),
                GetButton(Button.X, controlIndex),
                GetButton(Button.Y, controlIndex),
                GetButton(Button.L1, controlIndex),
                GetButton(Button.R1, controlIndex),
                GetButton(Button.L2, controlIndex),
                GetButton(Button.R2, controlIndex),
                GetButton(Button.LStick, controlIndex),
                GetButton(Button.RStick, controlIndex),
                GetButton(Button.Start, controlIndex),
                GetButton(Button.Back, controlIndex),
                GetAxis(Axis.LeftStick, controlIndex),
                GetAxis(Axis.RightStick, controlIndex),
                GetAxis(Axis.Dpad, controlIndex),
                GetTrigger(Trigger.LeftTrigger, controlIndex),
                GetTrigger(Trigger.RightTrigger, controlIndex)
            );
            return state;
        }
    }



}
