using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : FSM
{

    private playerController _obj;
    public string _stateName = "Run";

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
        if (_obj.dir == 0) {
            _obj.lastState = _stateName;
            _obj.switchState(_obj.idleState);
        }

        if (_obj.isJump) {
            _obj.lastState = _stateName;
            _obj.switchState(_obj.jumpState);
        }

        if (!_obj.isGround) {

            _obj.lastState = _stateName;
            _obj.switchState(_obj.cotoyeState);
        }
    }

    public void onUpdate()
    {
        _obj.anim.Play("Run");
        _obj.MovementMethod();
        
    }

}
