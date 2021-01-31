using System.Collections;
using UnityEngine;

//----------------------------------------------
//----------------------------------------------
// EyepatchCard
//----------------------------------------------
//----------------------------------------------
public partial class EyepatchCard  : BaseCard
{
    //----------------------------------------------
    // Variables

    public int Point { get; set; }
    public int TrumpPoint { get; set; }

    public EyepatchCardValue Value { get; set; }
    public EyepatchCardFamily Family { get; set; }

    //----------------------------------------------
    public EyepatchCard()
    {
    }

    //----------------------------------------------
    public EyepatchCardComponent Spawn()
    {
        if (EyepatchCardStaticData.Instance.Prefab != null)
        {
            GameObject cardObj = Object.Instantiate(EyepatchCardStaticData.Instance.Prefab) as GameObject;
            EyepatchCardComponent cardComp = cardObj.GetComponent<EyepatchCardComponent>();
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

    public int GetPoint(EyepatchCardFamily? trumpFamily)
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

     public static EyepatchCard GetBestCard(EyepatchCard a, EyepatchCard b, EyepatchCardFamily trumpFamily)
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
