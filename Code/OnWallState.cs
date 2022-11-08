using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnWallState : FSM
{
    private playerController _obj;
    public string _stateName = "OnWall";

    public void enterState(playerController obj)
    {
        _obj = obj;

        if (_obj.isRightGrab && _obj.transform.localEulerAngles.y == 0) {
            _obj.dirFilp();
        }

        if (_obj.isLeftGrab && _obj.transform.localEulerAngles.y == 180) {
            _obj.dirFilp();
        }

        _obj.jumpParticle.Stop();

        _obj.curJump = 0;

    }

    public void exitState()
    {
        throw new System.NotImplementedException();
    }

    public void onChange()
    {
        if (Input.GetMouseButtonUp(0)) {
            _obj.lastState = _stateName;
            _obj.switchState(_obj.fallState);
        }

        if (_obj.climbDir != 0) {
            _obj.lastState = _stateName;
            _obj.switchState(_obj.climbState);
        }

        if (Input.GetButtonDown("Jump")) {
            _obj.lastState = _stateName;
            _obj.switchState(_obj.wallJumpState);
        }
    }

    public void onUpdate()
    {
        if (Input.GetMouseButton(0)) {
            _obj.Grabing();
        }
    }

    
}
