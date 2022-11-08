using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbState : FSM
{
    private playerController _obj;
    public string _stateName = "Climb";

    public void enterState(playerController obj)
    {
        _obj = obj;
    }

    public void exitState()
    {
        throw new System.NotImplementedException();
    }

    public void onChange()
    {
        if (Input.GetMouseButtonUp(0))
        {
            _obj.lastState = _stateName;
            _obj.switchState(_obj.fallState);
        }

        if (_obj.climbDir == 0) {
            _obj.lastState = _stateName;
            _obj.switchState(_obj.onWallState);
        }

        if (Input.GetButtonDown("Jump"))
        {
            _obj.lastState = _stateName;
            _obj.switchState(_obj.wallJumpState);
        }

        if (!_obj.isGrab) {
            _obj.lastState = _stateName;
            _obj.switchState(_obj.wallJumpState);
        }
    }

    public void onUpdate()
    {
        _obj.Climbing();
    }
}
