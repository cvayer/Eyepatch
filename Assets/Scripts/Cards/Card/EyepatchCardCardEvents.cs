﻿using System;
using UnityEngine;

//-------------------------------------------------------
//-------------------------------------------------------
// Card
//-------------------------------------------------------
//-------------------------------------------------------
public partial class EyepatchCard
{
    //-------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------
    // Card.SelectedEvent
    //-------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------
    public class Selected : PooledEvent
    {
        private EyepatchCard m_card;
        private bool m_selected;
        private bool m_outsideOfHand;

        public EyepatchCard Card
        {
            get { return m_card; }
        }

        public bool IsSelected
        {
            get { return m_selected; }
        }

        public bool OutsideOfHand
        {
            get { return m_outsideOfHand; }
        }

        public override void Reset()
        {
            m_card = null;
            m_selected = false;
            m_outsideOfHand = false;
        }

        public void Init(EyepatchCard card, bool selected, bool outsideofHand)
        {
            m_card = card;
            m_selected = selected;
            m_outsideOfHand = outsideofHand;
        }
    }

    //-------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------
    // Card.Played
    //-------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------
    public class Played : PooledEvent
    {
        private EyepatchCard m_card;

        public EyepatchCard Card
        {
            get { return m_card; }
        }

        public override void Reset()
        {
            m_card = null;
        }

        public void Init(EyepatchCard card)
        {
            m_card = card;
        }
    }
}
