using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    private FSM playerState;
    List<IReSetGame> ResetList = new List<IReSetGame>();
    public static GameManager Instance { get { return instance; } }
    public static FSM PlayerState { get { return PlayerState; } }

    private void Awake()
    {

        instance = this;
    }

    public void getPlayer(FSM playerState) {
        this.playerState = playerState;
    }

    public void addResetGame(IReSetGame obs) {
        ResetList.Add(obs);
    }

    public void Notificaton() {
        foreach (var obj in ResetList) {
            obj.ResetNotify();
        }
    }
}
