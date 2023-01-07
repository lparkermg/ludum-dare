using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Title : MonoBehaviour
{
    public UIDocument UI;
    public Label StartLabel;

    // Start is called before the first frame update
    void Start()
    {
        StartLabel = UI.rootVisualElement.Q<Label>("PressLabel");
        StartLabel.text = $"Press {Gamepad.current.buttonSouth.displayName} or {Mouse.current.leftButton.displayName} on the mouse to start";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnAction(InputAction.CallbackContext context)
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
}
