using System;
using UnityEngine;

/// <summary> Behaviour for the Pause and Game Over Menu. </summary>
public class OverlayMenu : MonoBehaviour
{
    [SerializeField]
    GameState gameStateForMenu;

    void Start()
    {
        ApplyGameState(GameManager.Instance.GameState);
        GameManager.Instance.GameStateChanged += ApplyGameState;
    }

    void ApplyGameState(GameState gameState)
    {
        for(int i = 0; i < transform.childCount; ++i)
        {
            transform.GetChild(i).gameObject.SetActive(gameState == gameStateForMenu);
        }
    }

    void OnDestroy()
    {
        GameManager.Instance.GameStateChanged -= ApplyGameState;
    }

    public void SetGameStateRunning()
    {
        GameManager.Instance.SetGameState(GameState.Running);
    }

    public void SetGameStateMainMenu()
    {
        GameManager.Instance.SetGameState(GameState.Menu);
    }
}
