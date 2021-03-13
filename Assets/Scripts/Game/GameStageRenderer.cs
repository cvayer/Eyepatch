using System;
using System.Collections.Generic;
using UnityEngine;
using Pebble;

//-------------------------------------------------------
//-------------------------------------------------------
// GameRenderer
//-------------------------------------------------------
//-------------------------------------------------------
public class GameStageRenderer : StageRenderer
{
    //----------------------------------------------
    // Variables
    private List<EyepatchCardComponent> m_cards;

    //----------------------------------------------
    // Properties

    public new GameStage Stage
    {
        get 
        { 
            return base.Stage as GameStage;
        }
    }

    //----------------------------------------------
    // Methods
    //-------------------------------------------------------
    public GameStageRenderer()
    {
        m_cards = new List<EyepatchCardComponent>();
    }

    protected override void OnInit()
    {
        EventManager.Subscribe<EyepatchCard.Played>(this.OnCardPlayed, EventChannel.Post);
        EventManager.Subscribe<GameStage.NewRoundEvent>(this.OnNewRound);
        EventManager.Subscribe<GameStage.NewTurnEvent>(this.OnNewTurn);
    }

    protected override void OnShutdown()
    {
        EventManager.UnSubscribe<EyepatchCard.Played>(this.OnCardPlayed, EventChannel.Post);
        EventManager.UnSubscribe<GameStage.NewRoundEvent>(this.OnNewRound);
        EventManager.UnSubscribe<GameStage.NewTurnEvent>(this.OnNewTurn);
    }

    protected override void OnStart()
    {
 
    }

    protected override void OnStop()
    {
 
    }

    //----------------------------------------------
    protected override void OnUpdate()
    {

    }

    protected override void OnUpdateGUI()
    {
        if(!Stage.HasEnded)
        {
            if(Stage.Score != null)
            {
                GUI.Label(new Rect(UnityEngine.Screen.width - 320, 200, 100, 30), "Score : " + Stage.Score.GetScore(0) + " / " + Stage.Score.GetScore(1));
                GUI.Label(new Rect(UnityEngine.Screen.width - 320, 230, 100, 30), "Trump : " + Stage.Trump);
                GUI.Label(new Rect(UnityEngine.Screen.width - 320, 260, 100, 30), "Dealer : " + Stage.Dealer.Name);
                GUI.Label(new Rect(UnityEngine.Screen.width - 320, 290, 100, 30), "Bidder : " + Stage.Bidder.Name);
                GUI.Label(new Rect(UnityEngine.Screen.width - 320, 320, 100, 30), "Current : " + Stage.CurrentPlayer.Name);

            }
            

            /*// UI display
            HumanPlayer human = Stage.CurrentPlayer as HumanPlayer;
            if (human != null)
            {
                if (GUI.Button(new Rect(UnityEngine.Stage.width - 120, UnityEngine.Stage.height - 60, 100, 30), "End turn"))
                {
                    EventManager.SendEmptyPooledEvent<EndTurnButtonClicked>();
                }

                GUI.Label(new Rect(20, UnityEngine.Stage.height - 160, 100, 30), "Energy : " + human.Energy);
                GUI.Label(new Rect(20, UnityEngine.Stage.height - 120, 100, 30), "DrawPile : " + human.DrawPile.Size);
                GUI.Label(new Rect(UnityEngine.Stage.width - 120, UnityEngine.Stage.height - 120, 100, 30), "Discard : " + human.DiscardPile.Size);
            }


            // MinionDisplay

            foreach (Player combattant in Stage.Players)
            {
                int y = 50;
                int x = UnityEngine.Stage.width - 60;
                if (combattant is HumanPlayer)
                {
                    x = 30;
                }
            }*/
        }
        else
        {
            if(Stage.Succeded)
            {
                GUI.TextField(new Rect(20, UnityEngine.Screen.height - 160, 100, 30), "You win");
            }
            else
            {
                GUI.TextField(new Rect(20, UnityEngine.Screen.height - 160, 100, 30), "You Fail");
            }
        }
    }

    private void OnNewRound(GameStage.NewRoundEvent evt)
    {
        // TODO : Spawn once, then invisible
        if(evt.Start)
        {
            SpawnCards();
        }
        else
        {
            UnSpawnCards();
        }
            
        Refresh();
    }

    private void OnNewTurn(GameStage.NewTurnEvent evt)
    {
       Refresh();
    }

    protected void OnCardPlayed(EyepatchCard.Played evt)
    {
        Refresh();
    }

    protected void SpawnCards()
    {
        foreach (Player player in Stage.Players)
        {
            SpawnCards(player);
        }
    }
    protected void SpawnCards(Player player)
    {
        foreach (EyepatchCard card in player.Hand)
        {
            EyepatchCardComponent newCard = card.Spawn();
            if (newCard)
            {
                m_cards.Add(newCard);
            }
        }
    }

    protected void UnSpawnCards()
    {
        foreach (EyepatchCardComponent cardObj in m_cards)
        {
            UnityEngine.Object.Destroy(cardObj.gameObject);
        }
        m_cards.Clear();
    }

    protected void UnSpawnCard(EyepatchCardComponent cardObj)
    {
        m_cards.Remove(cardObj);
        UnityEngine.Object.Destroy(cardObj.gameObject);
    }

    void Refresh()
    {
        foreach (Player player in Stage.Players)
        {
            RefreshHand(player);
        }

        RefreshCurrentFold();

        RemovePastFolds();
    }

     private Vector3 spawnRef = new Vector3();
    private Vector3 rotation = new Vector3();
    protected void RefreshHand(Player player)
    {
        float halfHeight = Camera.main.orthographicSize;
        float halfWidth = halfHeight*Camera.main.aspect;

        float spacing = -0.4f;

      /*  if(player.Position == PlayerPosition.South)
        {
            spawnRef.x = -0.5f * halfWidth;
            spawnRef.y = -0.75f * halfHeight;   
        }
        else  if(player.Position == PlayerPosition.West)
        {
            spawnRef.x = -0.85f * halfWidth;
            spawnRef.y = 0.8f * halfHeight;     
        }
        else if(player.Position == PlayerPosition.North)
        {
            spawnRef.x = -0.5f * halfWidth;
            spawnRef.y = 0.75f * halfHeight;   
        }
        else // East
        {
            spawnRef.x = 0.85f * halfWidth;
            spawnRef.y = 0.8f * halfHeight;    
        }
        */
        foreach (BaseCard card in player.Hand)
        {
            EyepatchCardComponent cardComp = GetCardComponent(card);
            if (cardComp)
            {
                cardComp.SetInitialPosition(spawnRef);

                Renderer renderer = cardComp.gameObject.GetComponent<Renderer>();

             /*   if(player.Position == PlayerPosition.South)
                {
                    spawnRef.x += renderer.bounds.size.x + spacing;
                }
                else  if(player.Position == PlayerPosition.West)
                {
                    spawnRef.y -= (renderer.bounds.size.x + spacing);
                    rotation.z = 90.0f;
                    cardComp.gameObject.transform.eulerAngles = rotation;
                }
                else if(player.Position == PlayerPosition.North)
                {
                   spawnRef.x += renderer.bounds.size.x + spacing;
                }
                else // East
                {
                    spawnRef.y -= (renderer.bounds.size.x + spacing);
                    rotation.z = -90.0f;
                    cardComp.gameObject.transform.eulerAngles = rotation;
                } */
            }
        }
    }

    void RefreshCurrentFold()
    {
        float halfHeight = Camera.main.orthographicSize;
        float halfWidth = halfHeight*Camera.main.aspect;

        foreach (BaseCard card in Stage.CurrentFold.Deck)
        {
            Player player = card.Owner as Player;

            EyepatchCardComponent cardComp = GetCardComponent(card);
            if (cardComp)
            {
              /*  if(player.Position == PlayerPosition.South)
                {
                    spawnRef.x = 0.0f;
                    spawnRef.y = -0.25f * halfHeight;  
                }
                else  if(player.Position == PlayerPosition.West)
                {
                    spawnRef.x = -0.20f * halfWidth;  
                    spawnRef.y = 0.0f;  
                }
                else if(player.Position == PlayerPosition.North)
                {
                    spawnRef.x = 0.0f;
                    spawnRef.y = 0.25f * halfHeight;
                }
                else // East
                {
                    spawnRef.x = 0.20f * halfWidth;  
                    spawnRef.y = 0.0f;  
                } */
                
                cardComp.SetInitialPosition(spawnRef);
            }
        }
    }

    void RemovePastFolds()
    {
        Fold lastFold = Stage.LastFold;
        if(lastFold != null)
        {
            foreach (BaseCard card in lastFold.Deck)
            {
                EyepatchCardComponent cardComp = GetCardComponent(card);
                if(cardComp != null)
                {
                    UnSpawnCard(cardComp);
                }
            }
        }
    }

    protected EyepatchCardComponent GetCardComponent(BaseCard card)
    {
        foreach (EyepatchCardComponent cardObj in m_cards)
        {
            if(cardObj.Card == card)
            {
                return cardObj;
            }
        }
        return null;
    }

}

//-------------------------------------------------------------------------------------
//-------------------------------------------------------------------------------------
// EndTurnButtonClicked
//-------------------------------------------------------------------------------------
//-------------------------------------------------------------------------------------
public class EndTurnButtonClicked : PooledEvent
{
    public override void Reset()
    {

    }
}