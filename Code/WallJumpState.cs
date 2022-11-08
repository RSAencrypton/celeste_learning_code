using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJumpState : FSM
{
    private playerController _obj;
    public string _stateName = "WallJump";
    private Vector2 wallDir;
    private bool iswallJump;
    private float nextOperation;
    private float jumpMaxTime;

    public void enterState(playerController obj)
    {
        _obj = obj;
        iswallJump = true;

        nextOperation = Time.time + 0.1f;


        if (_obj.isLeftGrab)
        {
            wallDir = Vector2.right;
        }
        else if (_obj.isRightGrab)
        {
            wallDir = Vector2.left;
        }
    }

    public void exitState()
    {
        throw new System.NotImplementedException();
    }

    public void onChange()
    {

        if (Time.time <= nextOperation)
            return;

        if (_obj.rb.velocity.y < 0)
        {
            _obj.lastState = _stateName;
            _obj.switchState(_obj.fallState);
        }

        if (Input.GetMouseButtonDown(0) && _obj.isGrab)
        {

            _obj.lastState = _stateName;
            _obj.switchState(_obj.onWallState);
        }
    }

    public void onUpdate()
    {

        if (iswallJump) {
            iswallJump = false;
            _obj.wallJump(wallDir);
        }

    }
}
