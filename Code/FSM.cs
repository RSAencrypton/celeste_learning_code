using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface FSM
{
    void enterState(playerController obj);
    void onChange();
    void onUpdate();
    void exitState();
}
