using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootCard : Card
{
    public override CardType Type
    {
        get { return CardType.LootCard; }
    }

    public override string ToString()
    {
        return String.Format("{0}", base.Value);
    }
}
