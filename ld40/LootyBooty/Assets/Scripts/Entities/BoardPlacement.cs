using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardPlacement : MonoBehaviour
{
    public BoardPlacementType Type;
    public int PlaceId;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void FindMoveablePlacesFromHere()
    {
        
    }

    public void PlayerLanded(Player player)
    {
        switch (Type)
        {
            case (BoardPlacementType.Loot):
                player.AddToHand(GameManager.GivePlayerCard(true));
                break;
            case (BoardPlacementType.Trap):
                player.RemoveFromHand(GameManager.GivePlayerCard(false).Value);
                break;
            default:
                break;

        }
    }
}
