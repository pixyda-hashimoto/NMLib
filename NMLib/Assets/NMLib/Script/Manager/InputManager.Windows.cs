#if UNITY_STANDALONE || UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static NMLib.InputManager;

namespace NMLib
{
    public class InputManagerWindows: IInputManagerCore
    {
        private Dictionary<Button,KeyCode> assignButton;

        public InputManagerWindows() {
//            Logger.Log("InputManagerWinodws constructor");
            this.assignButton = new Dictionary<Button,KeyCode>();
        }

        public void Initialize() 
        {
            Logger.Log("input init : Windows");

//            this.assignButton.Add(Button.Up, KeyCode.Joystick1Button12);
//            this.assignButton.Add(Button.Down, KeyCode.Joystick1Button13);
//            this.assignButton.Add(Button.Left, KeyCode.Joystick1Button14);
//            this.assignButton.Add(Button.Right, KeyCode.Joystick1Button15);
            this.assignButton.Add(Button.A, KeyCode.Joystick1Button0);
            this.assignButton.Add(Button.B, KeyCode.Joystick1Button1);
            this.assignButton.Add(Button.X, KeyCode.Joystick1Button2);
            this.assignButton.Add(Button.Y, KeyCode.Joystick1Button3);
            this.assignButton.Add(Button.L1, KeyCode.Joystick1Button4);
            this.assignButton.Add(Button.R1, KeyCode.Joystick1Button5);
//            this.assignButton.Add(Button.L2, KeyCode.Joystick1Button10);
//            this.assignButton.Add(Button.R2, KeyCode.Joystick1Button11);
            this.assignButton.Add(Button.LStick, KeyCode.Joystick1Button8);
            this.assignButton.Add(Button.RStick, KeyCode.Joystick1Button9);
            this.assignButton.Add(Button.Start, KeyCode.Joystick1Button7);
            this.assignButton.Add(Button.Back, KeyCode.Joystick1Button6);
        }

        public void Update() {}

        public bool GetButtonDown(Button button, int controlIndex=0 )
        {
            if(!this.assignButton.ContainsKey(button)) return false;
            return Input.GetKeyDown(this.assignButton[button]);
        }

        public bool GetButtonUp(Button button, int controlIndex=0 )
        {
            if(!this.assignButton.ContainsKey(button)) return false;
            return Input.GetKeyUp(this.assignButton[button]);
        }

        public bool GetButton(Button button, int controlIndex=0 )
        {
            if(!this.assignButton.ContainsKey(button)) return false;
            return Input.GetKey(this.assignButton[button]);
        }

        public float GetTrigger(Trigger trigger, int controlIndex=0)
        {
            switch(trigger) {
            case Trigger.LeftTrigger: return Input.GetAxis("L_Trigger");
            case Trigger.RightTrigger: return Input.GetAxis("R_Trigger");
            }
            return 0f;
        }

        public Vector2 GetAxis(Axis axis, int controlIndex=0)
        {
            Vector2 axisXY = Vector3.zero;
            switch(axis) {
            case Axis.LeftStick: 
                axisXY.x = Input.GetAxis("L_Stick_H");
                axisXY.y = Input.GetAxis("L_Stick_V");
                break;
            case Axis.RightStick:
                axisXY.x = Input.GetAxis("R_Stick_H");
                axisXY.y = Input.GetAxis("R_Stick_V");
                break;
            case Axis.Dpad:
                axisXY.x = Input.GetAxis("D_Pad_H");
                axisXY.y = Input.GetAxis("D_Pad_V");
                break;
            }
            return axisXY;
        }

        private bool getConnectedPad() 
        {
            var controllerNames = Input.GetJoystickNames();
            if( controllerNames[0] == "" ) 
                return false;
            return true;
        }
    }

}
#endif

