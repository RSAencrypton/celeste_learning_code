using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : FSM
{

    private playerController _obj;
    private string _stateName = "Dead";

    public void enterState(playerController obj)
    {
        _obj = obj;
        GameManager.Instance.Notificaton();
    }

    public void exitState()
    {
        throw new System.NotImplementedException();
    }

    public void onChange()
    {
        if (_obj.anim.GetCurrentAnimatorStateInfo(0).IsName("hurt")
            && _obj.anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f) {
            _obj.transform.position = _obj.RespawPoint.position;
            _obj.lastState = _stateName;
            _obj.rb.velocity = Vector2.zero;
            _obj.switchState(_obj.idleState);
        }
    }

    public void onUpdate()
    {
        
    }
}
