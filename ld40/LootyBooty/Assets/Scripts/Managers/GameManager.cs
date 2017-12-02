using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public static class GameManager
{
    private static QueueList<LootCard> _lootDeck = new QueueList<LootCard>();
    private static QueueList<TrapCard> _trapDeck = new QueueList<TrapCard>();

    public static Player[] Players;
    public static int CurrentPlayer = 0;

    public static void InitialiseCards()
    {
        
    }

    public static void InitialisePlayers()
    {
        
    }

    public static Card GivePlayerCard(bool isLoot)
    {
        if (isLoot)
            return _lootDeck.PopFromTop();
        else
            return _trapDeck.PopFromTop();
    }
}
