using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CotoyeState : FSM
{

    private playerController _obj;
    private float cotoyeTime = 0.05f;
    private float timer;

    private string _stateName = "Cotoye";
    public void enterState(playerController obj)
    {
        _obj = obj;
        timer = Time.time + cotoyeTime;
    }

    public void exitState()
    {
        throw new System.NotImplementedException();
    }

    public void onChange()
    {
        if (Time.time >= timer) {
            _obj.lastState = _stateName;
            _obj.switchState(_obj.fallState);
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            _obj.lastState = _stateName;
            _obj.isJump = true;
            _obj.switchState(_obj.jumpState);
        }
    }

    public void onUpdate()
    {
        
    }
}
