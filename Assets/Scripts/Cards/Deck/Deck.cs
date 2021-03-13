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

    //------------------------------------------------------
    public void Init(ScoringRules scoring)
    {
        foreach(Card32Family family in (Card32Family[])System.Enum.GetValues(typeof(Card32Family)))
        {
            foreach (Card32Value value in (Card32Value[])System.Enum.GetValues(typeof(Card32Value)))
            {
                Card card = new Card();
                card.Family = family;
                card.Value = value;
                card.Point = scoring.GetPoint(value, false);
                card.TrumpPoint = scoring.GetPoint(value, true);
                AddCard(card);
            }
        }
    }

    class CardComparer : IComparer<Card>
    {
        public Card32Family? TrumpFamily { get; set; }

        public int Compare(Card a, Card b)
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
    public void SortByFamilyAndValue(Card32Family? trumpFamily)
    {
        s_comparer.TrumpFamily = trumpFamily;
        Cards.Sort(s_comparer);
    }
}