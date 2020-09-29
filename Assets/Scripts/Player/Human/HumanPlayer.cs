using System.Collections.Generic;
using UnityEngine;

//----------------------------------------------
//----------------------------------------------
// HumanPlayer
//----------------------------------------------
//----------------------------------------------
public class HumanPlayer : Player
{


    //----------------------------------------------
    public HumanPlayer()
    {
    }

    //----------------------------------------------
    protected override void OnInit()
    {
        EventManager.Subscribe<EyepatchCard.Selected>(this.OnCardSelectedEvent);
    }

    //--------------------------------------------------------------------
    protected override void OnShutdown()
    {
        EventManager.UnSubscribe<EyepatchCard.Selected>(this.OnCardSelectedEvent);
    }

    private void OnCardSelectedEvent(EyepatchCard.Selected evt)
    {
        if(evt.IsSelected == false && evt.OutsideOfHand)
        {
            Play(evt.Card, Screen.CurrentFold);
        }
    }
}

