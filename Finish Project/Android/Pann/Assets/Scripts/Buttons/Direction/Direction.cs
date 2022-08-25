using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Direction : MonoBehaviour
{
    public void RightMove()
    {
        Run.moveX = 1;
        Run.buttonPress = true;
    }

    public void LeftMove()
    {
        Run.moveX = -1;
        Run.buttonPress = true;
    }

    public void Jump()
    {
        Run.buttonPress = true;
        Run.isJump = true;
    }
    public void JumpStop()
    {
        Run.isJump = false;
    }

    public void Stop()
    {
        Run.moveX = 0;
    }
}
