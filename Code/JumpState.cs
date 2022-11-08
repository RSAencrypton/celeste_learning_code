using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : FSM
{
    private playerController _obj;
    public string _stateName = "Jump";

    public void enterState(playerController obj)
    {
        _obj = obj;
        _obj.jumpParticle.Play();
    }

    public void exitState()
    {
        
    }

    public void onChange()
    {
        if ((!_obj.isJump && _obj.rb.velocity.y < 0))
        {
            _obj.lastState = _stateName;
            _obj.switchState(_obj.fallState);
        }

        if (Input.GetMouseButtonDown(0) && _obj.isGrab) {
            _obj.lastState = _stateName;
            _obj.switchState(_obj.onWallState);
        }


    }

    public void onUpdate()
    {

        if (_obj.isJump)
            _obj.Jump();

        if (_obj.rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            _obj.rb.velocity += Vector2.up * Physics2D.gravity.y * (_obj.lowJumpMultiplier - 1) * Time.deltaTime;
        }


        _obj.MovementMethod();



    }
}
