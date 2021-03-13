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
    public int TrumpPoint { get; set; }

    public Card32Value Value { get; set; }
    public Card32Family Family { get; set; }

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

    public int GetPoint(Card32Family? trumpFamily)
    {
        if(trumpFamily != null && Family == trumpFamily)
        {
            return TrumpPoint;
        }
        return Point;
    }

    public override string ToString()
    {
        return "(" + Value + " " + Family + ")";
    }

     public static Card GetBestCard(Card a, Card b, Card32Family trumpFamily)
    {
        if(a.Family == b.Family)
        {
            int aCardPoint = a.GetPoint(trumpFamily);
            int bCardpoint = b.GetPoint(trumpFamily);
            if(aCardPoint > bCardpoint)
            {
                return a;
            }
            else if(aCardPoint == bCardpoint) // Same point, value wins
            {
                if(a.Value > b.Value)
                {
                    return a;
                }
            }
        }
        else
        {
            if(a.Family == trumpFamily)
            {
                return a;
            }     
        }
        return b;
    }
}
