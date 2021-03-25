using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pebble;
//----------------------------------------------
//----------------------------------------------
// Deck
//----------------------------------------------
//----------------------------------------------

public class Deck : BaseDeck<Card>
{
    //------------------------------------------------------
    public Deck()
     : base()
    {
        
    }

    //------------------------------------------------------
    public Deck(IDeckOwner owner)
     : base(owner)
    {

    }
}