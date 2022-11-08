using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class idleState : FSM
{
    
    private playerController _obj;
    string _stateName = "Idle";

    public void enterState(playerController obj)
    {
        _obj = obj;
        _obj.curJump = 0;
    }

    public void exitState()
    {
        throw new System.NotImplementedException();
    }

    public void onChange()
    {
        if (_obj.dir != 0) {
            _obj.lastState = _stateName;
            _obj.switchState(_obj.runState);
        }

        if (_obj.isJump) {
            _obj.lastState = _stateName;
            _obj.switchState(_obj.jumpState);
        }

        if (!_obj.isGround) {
            _obj.lastState = _stateName;
            _obj.switchState(_obj.fallState);
        }
    }

    public void onUpdate()
    {
        _obj.anim.Play("Idle");
    }
}
