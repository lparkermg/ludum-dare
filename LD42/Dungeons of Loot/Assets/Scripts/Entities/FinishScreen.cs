using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishScreen : ManagedObjectBehaviour
{
    [SerializeField] private Text _goldText;

    public override void StartMe(GameObject managers)
    {
        var gold = PlayerPrefs.GetInt("GoldFromRun");

        _goldText.text = "You made " + gold +" gold coins...";
    }

    public override void UpdateMe(){}
}
