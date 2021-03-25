using System.Collections;
using UnityEngine;
using Pebble;

//----------------------------------------------
//----------------------------------------------
// Card
//----------------------------------------------
//----------------------------------------------
public partial class Card  : BaseCard
{
    //----------------------------------------------
    // Variables

    public int Point { get; set; }

    //----------------------------------------------
    public Card()
    {
    }

    //----------------------------------------------
    public CardComponent Spawn()
    {
        if (CardStaticData.Instance.Prefab != null)
        {
            GameObject cardObj = Object.Instantiate(CardStaticData.Instance.Prefab) as GameObject;
            CardComponent cardComp = cardObj.GetComponent<CardComponent>();
            if(cardComp != null)
            {
                cardComp.Init(this);
            }
            return cardComp;
        }
        return null;
    }

    public void OnPlay()
    {
        Played evt = Pools.Claim<Played>();
        evt.Init(this);
        EventManager.SendEvent(evt);
    }

    public override string ToString()
    {
        return "(" + Point + ")";
    }
}
