﻿using System.Collections.Generic;
using System;

[Serializable]
public class CardPoint
{
    public EyepatchCardValue Value;
    public int Point = 0;
    public int TrumpPoint = 0;
}

[Serializable]
public class ScoringRules
{
    public List<CardPoint> Points;
    public int LastFold = 0;
    public int Rebelote = 20;

    public int GetPoint(EyepatchCardValue value, bool trump)
    {
        foreach(CardPoint point in Points)
        {
            if(point.Value == value)
            {
                return (trump) ? point.TrumpPoint : point.Point;
            }
        }
        return 0;
    }
}