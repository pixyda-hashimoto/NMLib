using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NMLib;

public class SinglePad : MonoBehaviour
{
    string str = "";
    int updown = 0;

    void Start()
    {   
    }
    void Update()
    {
        var state = InputManager.GetPadState();
        if(InputManager.GetButtonDown(InputManager.Button.A)){ updown++; }
        if(InputManager.GetButtonUp(InputManager.Button.A)){ updown++; }

        str = "UP:"+state.Up+" / "+"DOWN:"+state.Down+" / "+"LEFT:"+state.Left+" / "+"RIGHT:"+state.Right+"\n"+
              "A:"+state.A+"(UpDown:"+updown+")"+" / "+"B:"+state.B+" / "+"X:"+state.X+" / "+"Y:"+state.Y+"\n"+
              "L1:"+state.L1+" / "+"R1:"+state.R1+" / "+"L2:"+state.L2+" / "+"R2:"+state.R2+"\n"+
              "LStick:"+state.LStick+" / "+"RStick:"+state.RStick+"\n"+
              "LStickX:"+state.LeftStickAxis.x+" / "+"LStickY:"+state.LeftStickAxis.y+"\n"+
              "RStickX:"+state.RightStickAxis.x+" / "+"RStickY:"+state.RightStickAxis.y+"\n"+
              "DPadX:"+state.DpadAxis.x+" / "+"DPadY:"+state.DpadAxis.y+"\n"+
              "LTrigger:"+state.LeftTrigger+" / "+"RTrigger:"+state.RightTrigger+"\n";
    }

    void OnGUI(){

        // GUI用の解像度設定
        float scale = (Screen.width > Screen.height) ? Screen.width  / 720f : Screen.height / 720f;
        GUIUtility.ScaleAroundPivot(new Vector2(scale, scale), Vector2.zero);
        GUI.Label(new Rect(0, 0, 1024, 512), str);
    }


}