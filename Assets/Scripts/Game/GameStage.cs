﻿using System;
using System.Collections.Generic;
using UnityEngine;
using Pebble;
//----------------------------------------------
//----------------------------------------------
// Game
//----------------------------------------------
//----------------------------------------------
public class GameStage : Stage, IDeckOwner
{
    public enum EndState
    {
        None,
        Success, 
        Fail
    }

    //----------------------------------------------
    // Variables
    private List<Player>                       m_players;
    private Deck                       m_deck;

    private ActionQueue                        m_actionQueue;

    private EndState m_endState;

    private static int s_invalidRoundCount = -1;

    private int m_currentRound = s_invalidRoundCount;


    private static float s_afterPlayDuration = 1.0f;
    private float m_afterPlayTimer = -1.0f;

    public Score Score { get; set; }

    //----------------------------------------------
    // Properties
    public Player CurrentPlayer
    {
        get; set;
    }

    public Player Dealer
    {
        get; set;
    }

    public Player RoundFirstPlayer
    {
        get; set;
    }

    public Player Bidder
    {
        get; set;
    }

    public List<Player> Players
    {
        get
        {
            return m_players;
        }
    }

    public ActionQueue ActionQueue
    {
        get
        {
            return m_actionQueue;
        }
    }

    public bool HasEnded
    {
        get{ return !(m_endState == EndState.None); }
    }

    public bool Succeded
    {
        get { return m_endState == EndState.Success; }
    }

    public bool Failed
    {
        get { return m_endState == EndState.Fail; }
    }

    public new GameDefinition Definition
    {
        get 
        { 
            return base.Definition as GameDefinition;
        }
    }

    
    public Card32Family Trump {get; set; }

    //----------------------------------------------
    public GameStage()
    {
        m_players = new List<Player>();
        m_actionQueue = new ActionQueue();
        m_endState = EndState.None;
        m_deck = new Deck(this);

        Score = new Score();
    }

    //----------------------------------------------
    protected override void OnInit()
    {
        EventManager.Subscribe<Card.Played>(this.OnCardPlayed);

        AddPlayers();
    }

    
    protected override void OnShutdown()
    {
        EventManager.UnSubscribe<Card.Played>(this.OnCardPlayed);

        foreach (Player player in m_players)
        {
            player.Shutdown();
        }
        m_players.Clear();

        m_deck.Clear();
    }


    //----------------------------------------------
    protected override void OnStart() 
    {
        m_deck.Shuffle();

        StartRound();
    }

    protected override void OnStop()
    {
        // TODO : recompute deck
    }

    //----------------------------------------------
    protected override void OnUpdate()
    {
        UpdatePlayers();

        if(m_afterPlayTimer >= 0.0f)
        {
            m_afterPlayTimer -= Time.deltaTime;
            if(m_afterPlayTimer <= 0.0f)
            {
                m_afterPlayTimer = -1.0f;
                OnAfterPlayTimerDone();
            }
        }


        if(CurrentPlayer != null)
        {
            m_actionQueue.Process();
        }
    }

    //----------------------------------------------
    protected void SetEndState(EndState state)
    {
        if(m_endState != state)
        {
            m_endState = state;
        }
    }

    //----------------------------------------------
    protected void AddPlayer<PlayerType>(string name)  where PlayerType : Player, new()
    {
        PlayerType newPlayer = new PlayerType();
        newPlayer.Stage = this;
        newPlayer.Name = name;
        newPlayer.Init();
        m_players.Add(newPlayer);
        
    }

    //----------------------------------------------
    protected void AddPlayers()
    {
        AddPlayer<HumanPlayer>("South");
        AddPlayer<AIPlayer>("West");
    }

    protected Player GetLeftPlayer(Player player)
    {
        if(m_players.Count > 0)
        {
            if(player != null)
            {
                 int index = m_players.IndexOf(player);   
                 index = (index + 1)% m_players.Count;
                 return m_players[index];
            }
            return m_players[0];
        }
        return null;
    }

    protected void DealCards()
    {
        // TODO : Cut
        // New dealer is the left player of the current player
     /*   Dealer = GetLeftPlayer(Dealer);
        RoundFirstPlayer = GetLeftPlayer(Dealer);
   
        for(int  iDeal = 0; iDeal < Definition.Rules.Dealing.Dealings.Count; ++iDeal)
        {
            int dealing = Definition.Rules.Dealing.Dealings[iDeal];

            Player player = RoundFirstPlayer;
            do
            {
                m_deck.MoveCardsTo(dealing, player.Hand);
                player = GetLeftPlayer(player); 
            }
            while(player != RoundFirstPlayer);
        }*/
    }

    //----------------------------------------------
    void StartRound()
    {
        m_currentRound++;

        DealCards();

        NewRoundEvent evt = Pools.Claim<NewRoundEvent>();
        evt.Start = true;
        EventManager.SendEvent(evt);

        StartTurn(RoundFirstPlayer);
    }

    Score m_roundScore = new Score();
    void EndRound()
    {
        m_roundScore.Reset();
    
      /*  for(int i = 0; i < m_pastFolds.Length; ++i)
        {
            PlayerTeam team = (PlayerTeam) i;
            List<Fold> folds = m_pastFolds[i];
            foreach(Fold fold in folds)
            {
                m_roundScore.AddScore(team, fold.Points);  
                fold.BaseDeck.MoveAllCardsTo(m_deck);
            }
        }

        PlayerTeam winningTeam = m_roundScore.GetLeadingTeam(Bidder.Team);

        // TODO : Round points
        // TODO : Bet
        Score.AddScore(winningTeam, m_roundScore.GetScore(winningTeam));

        // 10 de der
        if(LastFoldingTeam != null)
        {
            Score.AddScore((PlayerTeam)LastFoldingTeam, 10);
        }*/
        NewRoundEvent evt = Pools.Claim<NewRoundEvent>();
        evt.Start = false;
        EventManager.SendEvent(evt);
    }

    //----------------------------------------------
    void StartTurn(Player player)
    {
        Player previous = CurrentPlayer;
        CurrentPlayer = player;

        NewTurnEvent evt = Pools.Claim<NewTurnEvent>();
        evt.Current = CurrentPlayer;
        evt.Previous = previous;
        EventManager.SendEvent(evt);
    }

    //----------------------------------------------
    protected  void UpdatePlayers()
    {
        foreach (Player player in m_players)
        {
            player.Update();
        }
    }

    protected void OnCardPlayed(Card.Played evt)
    {
        m_afterPlayTimer = s_afterPlayDuration;
    }

    protected void OnAfterPlayTimerDone()
    {
        // One Fold is done, select new player.
        if(true)
        {
            Player winner = null;
          /*  LastFoldingTeam = winner.Team;

            Fold newFold = new Fold();
            CurrentFold.MoveTo(newFold);
            PastFolds[(int)winner.Team].Add(newFold);*/

            // New player has no cards in hand, we end the round
            if(winner.Hand.Empty)
            {
                // Next Round;
                EndRound();
                // TODO : Win condition
                StartRound();
            }
            else
            {
                StartTurn(winner);   
            }
        }
        else
        {
            StartTurn(GetLeftPlayer(CurrentPlayer));    
        }
    }



    //------------------------------------
    // Events
    public class NewRoundEvent : PooledEvent
    {
        public bool Start { get; set;}
        public override void Reset()
        {
            Start = true;
        }
    }

    public class NewTurnEvent : PooledEvent
    {
        public Player Current { get; set;}
        public Player Previous { get; set;}
        public override void Reset()
        {

        }
    }
}




