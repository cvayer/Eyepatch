using System.Collections.Generic;
using UnityEngine;

public class Fold
{
    public EyepatchDeck Deck { get; set;}

    public Player Winner { get; set; }

    public int Points { get; set; }

    public EyepatchCardFamily? RequestedFamily
    {
        get
        {
             if(Deck.Cards.Count > 0)
             {
                 return Deck.Cards.Front().Family;
             }   
             return null;
        }
    }

    public Fold()
    {
        Deck = new EyepatchDeck();
    }

    public void MoveTo(Fold fold)
    {
        Deck.MoveAllCardsTo(fold.Deck);
        fold.Winner = Winner;
        fold.Points = Points;
        Winner = null;
        Points = 0;
    }

    public void Finalize(EyepatchCardFamily trumpFamily)
    {
        EyepatchCard bestCard = GetBest(trumpFamily);
        if(bestCard != null)
        {
            Winner = bestCard.Owner as Player;
            Points = GetPoints(trumpFamily);
        }
    }

    public EyepatchCard GetBest(EyepatchCardFamily trumpFamily)
    {
        EyepatchCardFamily? requested = RequestedFamily;
        if(requested != null)
        {
            EyepatchCard bestCard = Deck.Cards[0];
            if(Deck.Cards.Count > 1)
            {
                for(int  i = 1; i < Deck.Cards.Count ; ++i)
                {
                    EyepatchCard card = Deck.Cards[i];

                    bestCard = EyepatchCard.GetBestCard(card, bestCard, trumpFamily);
                }    
            }
            return bestCard;
        }
        return null;
    }

    public int GetPoints(EyepatchCardFamily trumpFamily)
    {
        int points = 0;
        foreach(EyepatchCard card in Deck.Cards)
        {
            points += card.GetPoint(trumpFamily);
        }
        return points;
    }
}