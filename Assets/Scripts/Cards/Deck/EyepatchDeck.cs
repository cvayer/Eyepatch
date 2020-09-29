using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//----------------------------------------------
//----------------------------------------------
// Deck
//----------------------------------------------
//----------------------------------------------

public class EyepatchDeck : Deck<EyepatchCard>
{
    //------------------------------------------------------
    public EyepatchDeck()
     : base()
    {
        
    }

    //------------------------------------------------------
    public EyepatchDeck(IDeckOwner owner)
     : base(owner)
    {

    }

    //------------------------------------------------------
    public void Init(ScoringRules scoring)
    {
        foreach(EyepatchCardFamily family in (EyepatchCardFamily[])System.Enum.GetValues(typeof(EyepatchCardFamily)))
        {
            foreach (EyepatchCardValue value in (EyepatchCardValue[])System.Enum.GetValues(typeof(EyepatchCardValue)))
            {
                EyepatchCard card = new EyepatchCard();
                card.Family = family;
                card.Value = value;
                card.Point = scoring.GetPoint(value, false);
                card.TrumpPoint = scoring.GetPoint(value, true);
                AddCard(card);
            }
        }
    }

    class CardComparer : IComparer<EyepatchCard>
    {
        public EyepatchCardFamily? TrumpFamily { get; set; }

        public int Compare(EyepatchCard a, EyepatchCard b)
        {
            int compareFamily = a.Family.CompareTo(b.Family);
            if(compareFamily == 0)
            {
                int pointsA = a.GetPoint(TrumpFamily);
                int pointsB = b.GetPoint(TrumpFamily);

                if(pointsA == pointsB)
                {
                    return a.Value.CompareTo(b.Value);
                }
                return pointsB - pointsA;
            }

            if(TrumpFamily != null)
            {
                if(a.Family == TrumpFamily)
                    return -1;
                if(b.Family == TrumpFamily)
                    return 1;
            }
            return compareFamily;
        }
    }

    static CardComparer s_comparer = new CardComparer();
    public void SortByFamilyAndValue(EyepatchCardFamily? trumpFamily)
    {
        s_comparer.TrumpFamily = trumpFamily;
        Cards.Sort(s_comparer);
    }
}