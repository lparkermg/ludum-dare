using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapCard : Card {

    public override CardType Type
    {
        get { return CardType.TrapCard; }
    }

    public override string ToString()
    {
        switch (base.Value)
        {
            case (1):
                return "Discard all your loot cards.";
            case (2):
                return "Discard half of your loot cards.";
            case (3):
                return "Discard a third of your loot cards.";
            case (4):
                return "Discard a quarter of you loot cards.";
            default:
                return base.ToString();
        }
    }
}
