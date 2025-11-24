using UnityEngine;

public enum UIState
{
    Game,
    GameOver
}

public class UIStateMachine : MonoBehaviour
{
    public UIState currentState;

    public GameObject gameRoot;
    public GameObject gameOverRoot;

    void Start()
    {
        SetState(UIState.Game);
    }

    public void SetState(UIState newState)
    {
        currentState = newState;

        gameRoot.SetActive(currentState == UIState.Game);
        gameOverRoot.SetActive(currentState == UIState.GameOver);
    }
}
