using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pebble;
//----------------------------------------------
//----------------------------------------------
// Player
//----------------------------------------------
//----------------------------------------------
public class Player : IDeckOwner
{

    //----------------------------------------------
    // Variables
    protected GameStage m_game;
    private   Deck m_hand;

    private bool m_isAllowedToPlay = false;

    //----------------------------------------------
    // Properties

    public GameStage Stage
    {
        get
        {
            return m_game;
        }
        set
        {
            m_game = value;
        }
    }

    public Deck Hand
    {
        get
        {
            return m_hand;
        }
    }

    public Deck TurnPlayableCards
    {
        get; set;
    }

    public string Name { get; set; }

    //----------------------------------------------
    public Player()
    {
        m_hand = new Deck(this);
    }

    //----------------------------------------------
    public void Init()
    {
        EventManager.Subscribe<GameStage.NewTurnEvent>(this.OnNewTurn);

        OnInit();
    }

    //----------------------------------------------
    protected virtual void OnInit()
    {

    }

    //--------------------------------------------------------------------
    public void Shutdown()
    {
        OnShutdown();
        EventManager.UnSubscribe<GameStage.NewTurnEvent>(this.OnNewTurn);
    }

    //--------------------------------------------------------------------
    protected virtual void OnShutdown()
    {

    }

   
    //----------------------------------------------
    public void Update()
    {
        OnUpdate();
    }

    //----------------------------------------------
    protected virtual void OnUpdate()
    {

    }

    //----------------------------------------------
    protected void Play(Card card)
    {
        if (CanPlay(card))
        {
            DoPlay(card);
        }
    }

    //----------------------------------------------
    public bool CanPlay(Card card)
    {
        if (m_isAllowedToPlay && Hand.Contains(card))
        {
            if(TurnPlayableCards != null && TurnPlayableCards.Contains(card))
                return true;
        }
        return false;
    }

    //----------------------------------------------
    protected void DoPlay(Card card)
    {
        //m_hand.MoveCardTo(card, fold.Deck);
        card.OnPlay();
    }

    List<Card>  m_trumpCards = new List<Card> ();
    List<Card>  m_trumpBetterCards = new List<Card> ();

    //----------------------------------------------
    protected Deck ComputePlayableCards(Card32Family trumpFamily)
    {
        Deck playables = new Deck();

        return playables;
    }

    //----------------------------------------------
    private void OnNewTurn(GameStage.NewTurnEvent evt)
    {
       if(evt.Previous == this)
       {
           m_isAllowedToPlay = false;
           OnTurnStop();
           TurnPlayableCards = null;
       }

       if(evt.Current == this)
       {
           m_isAllowedToPlay = true;

           TurnPlayableCards = ComputePlayableCards(Stage.Trump);
           OnTurnStart();
       }
    }

    protected virtual void OnTurnStart() {}
    protected virtual void OnTurnStop() {}

    public void PrintHand()
    {
        Hand.Print(Name);
    }
}
