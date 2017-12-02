using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerData PlayerData;
    private List<Card> _heldCards;

    public int CardCount
    {
        get { return _heldCards.Count; }
    }

	// Use this for initialization
	void Start ()
	{
	    _heldCards = new List<Card>();
	}
	
    public void MoveTo(Vector3 place)
    {
        transform.position = place;
    }

    public void AddToHand(Card card)
    {
        _heldCards.Add(card);
    }

    public void RemoveFromHand(int amount)
    {
        //TODO: May need to change this to use minus 1 rather than just the count.
        for (var i = 0; i < amount; i++) _heldCards.RemoveAt(_heldCards.Count);
    }
}
