using System.Collections;
using System.Collections.Generic;
using Rewired;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : ManagedObjectBehaviour
{
    private bool _sceneLoading = false;
    private AsyncOperation _scene;
    private Player _player;

    public override void StartMe(GameObject managers)
    {
        _player = ReInput.players.GetPlayer(0);
    }

    public override void UpdateMe()
    {
        if (!_sceneLoading && _player.GetButtonDown("Select"))
        {
            StartCoroutine(LoadGame());
        }
        else if (_player.GetButtonDown("Back"))
        {
            Application.Quit();
        }
    }

    IEnumerator LoadGame()
    {
        _scene = SceneManager.LoadSceneAsync(1);

        while (!_scene.isDone)
        {
            yield return null;
        }
    }
}
