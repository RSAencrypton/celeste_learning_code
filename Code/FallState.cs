using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallState : FSM
{
    private playerController _obj;
    public  string _stateName = "Fall";

    public void enterState(playerController obj)
    {
        _obj = obj;

        if (_obj.lastState == "Jump")
            _obj.jumpParticle.Stop();
    }

    public void exitState()
    {
        _obj.switchState(_obj.idleState);
    }

    public void onChange()
    {
        if (Input.GetButtonDown("Jump") && _obj.curJump != _obj.MaxJump)
        {

            _obj.lastState = _stateName;
            _obj.isJump = true;
            _obj.switchState(_obj.jumpState);
        }

        if (_obj.isGround)
        {
            _obj.lastState = _stateName;
            exitState();
        }



        if (Input.GetMouseButtonDown(0) && _obj.isGrab) {

            _obj.lastState = _stateName;
            _obj.switchState(_obj.onWallState);
        }
    }

    public void onUpdate()
    {
        _obj.anim.Play("Fall");

        if (_obj.rb.velocity.y < 0)
        {
            _obj.rb.velocity += Vector2.up * Physics2D.gravity.y * (_obj.fallingMultiplier - 1) * Time.deltaTime;
        }

        _obj.MovementMethod();
    }
}
