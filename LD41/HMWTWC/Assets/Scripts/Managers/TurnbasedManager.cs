using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public class TurnbasedManager : MonoBehaviour
{
    // Players
    private List<Player> _playersInMatch;

    // Turn countdown.
    private float _maxTime = 5.0f;

    private float _currentTime = 0.0f;

    private bool _inPlacementSelection = false;


	// Update is called once per frame
	void Update ()
	{
	    if (_inPlacementSelection && GameplayManager.InGame)
	        TimeCheck();
	}

    public void StartGame(List<Player> players)
    {
        GameplayManager.UpdateInGame(true);
    }

    private void EndGame()
    {
        GameplayManager.UpdateInGame(false);
    }

    private void TimeCheck()
    {
        if (_currentTime >= _maxTime)
        {
            _inPlacementSelection = false;
            StartCoroutine(TakeTurn());
            _currentTime = 0.0f;
        }
        else
        {
            _currentTime = _currentTime + GameplayManager.DeltaTime;
        }
    }

    private IEnumerator TakeTurn()
    {
        // TODO: Loop through all the players and make their moves.

        // TODO: Use a Linq statement to remove those from the game.
        // TODO: loop through to remove the gameobjects etc from the gameview.

        // TODO: Recalculate the targets for all of those in the game.

        // Done
        yield return null;
    }

}
