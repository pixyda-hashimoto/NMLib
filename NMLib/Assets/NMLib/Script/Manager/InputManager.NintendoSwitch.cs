#if UNITY_SWITCH //&& !UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using nn.hid;
using static NMLib.InputManager;

namespace NMLib
{
    /// <summary>
    /// 
    /// </summary>
    public class InputManagerNintendoSwitch : IInputManagerCore
    {
        private NpadId npadId = NpadId.Invalid;
        private NpadStyle npadStyle = NpadStyle.Invalid;
        private NpadState npadState = new NpadState();
        private bool padEnabled = false;

        private Dictionary<Button,NpadButton> assignButton;

        public InputManagerNintendoSwitch() {
//            Logger.Log("InputManagerNintendoSwitch constructor");
            this.assignButton = new Dictionary<Button,NpadButton>();
        }

        public void Initialize() 
        {
            Logger.Log("input init : switch");

            Npad.Initialize();
            Npad.SetSupportedIdType(new NpadId[]{ NpadId.Handheld, NpadId.No1 });
            Npad.SetSupportedStyleSet(NpadStyle.FullKey | NpadStyle.Handheld | NpadStyle.JoyDual);

            this.assignButton.Add(Button.Up, NpadButton.Up);
            this.assignButton.Add(Button.Down,NpadButton.Down);
            this.assignButton.Add(Button.Left, NpadButton.Left);
            this.assignButton.Add(Button.Right, NpadButton.Right);
            this.assignButton.Add(Button.A, NpadButton.A);
            this.assignButton.Add(Button.B, NpadButton.B); 
            this.assignButton.Add(Button.X, NpadButton.X); 
            this.assignButton.Add(Button.Y, NpadButton.Y); 
            this.assignButton.Add(Button.L1, NpadButton.L); 
            this.assignButton.Add(Button.R1, NpadButton.R); 
            this.assignButton.Add(Button.L2, NpadButton.ZL); 
            this.assignButton.Add(Button.R2, NpadButton.ZR); 
            this.assignButton.Add(Button.LStick, NpadButton.StickL); 
            this.assignButton.Add(Button.RStick, NpadButton.StickR); 
            this.assignButton.Add(Button.Start, NpadButton.Plus);
            this.assignButton.Add(Button.Back, NpadButton.Minus); 
        }

        public void Update() 
        {
            this.padEnabled = UpdatePadState();
//            DebugLogger.Log("padEnabled:"+padEnabled);
        }

        private bool UpdatePadState()
        {
            NpadStyle handheldStyle = Npad.GetStyleSet(NpadId.Handheld);
            NpadState handheldState = this.npadState;
            if (handheldStyle != NpadStyle.None) {
                Npad.GetState(ref handheldState, NpadId.Handheld, handheldStyle);
                if (handheldState.buttons != NpadButton.None) {
                    this.npadId = NpadId.Handheld;
                    this.npadStyle = handheldStyle;
                    this.npadState = handheldState;
                    return true;
                }
            }

            NpadStyle no1Style = Npad.GetStyleSet(NpadId.No1);
            NpadState no1State = npadState;
            if (no1Style != NpadStyle.None) {
                Npad.GetState(ref no1State, NpadId.No1, no1Style);
                if (no1State.buttons != NpadButton.None) {
                    this.npadId = NpadId.No1;
                    this.npadStyle = no1Style;
                    this.npadState = no1State;
                    return true;
                }
            }

            if ((this.npadId == NpadId.Handheld) && (handheldStyle != NpadStyle.None)) {
                this.npadId = NpadId.Handheld;
                this.npadStyle = handheldStyle;
                this.npadState = handheldState;
            }
            else if ((this.npadId == NpadId.No1) && (no1Style != NpadStyle.None)) {
                this.npadId = NpadId.No1;
                this.npadStyle = no1Style;
                this.npadState = no1State;
            }
            else {
                this.npadId = NpadId.Invalid;
                this.npadStyle = NpadStyle.Invalid;
                this.npadState.Clear();
                return false;
            }
            return true;
        }

        public bool GetButtonDown(Button button, int controlIndex=0 )
        {
            if(!this.assignButton.ContainsKey(button)) return false;
            return this.npadState.GetButtonDown(this.assignButton[button]);
        }

        public bool GetButtonUp(Button button, int controlIndex=0 )
        {
            if(!this.assignButton.ContainsKey(button)) return false;
            return this.npadState.GetButtonUp(this.assignButton[button]);
        }

        public bool GetButton(Button button, int controlIndex=0 )
        {
            if(!this.assignButton.ContainsKey(button)) return false;
            return this.npadState.GetButton(this.assignButton[button]);
        }

        public float GetTrigger(Trigger trigger, int controlIndex=0) {
            return 0f;
        }

        public Vector2 GetAxis(Axis axis, int controlIndex=0)
        {
            Vector2 axisXY = Vector3.zero;
            switch(axis) {
            case Axis.LeftStick: 
                AnalogStickState lStick = this.npadState.analogStickL;
                axisXY.x = lStick.fx;
                axisXY.y = lStick.fy;
                break;
            case Axis.RightStick:
                AnalogStickState rStick = this.npadState.analogStickR;
                axisXY.x = rStick.fx;
                axisXY.y = rStick.fy;
                break;
            case Axis.Dpad:
                break;
            }
            return axisXY;
        }

    }


}
#endif