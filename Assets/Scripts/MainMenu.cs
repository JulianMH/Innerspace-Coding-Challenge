using UnityEngine;
using System.Collections;

/// <summary> Behaviour for the Main Menu. </summary>
public class MainMenu : MonoBehaviour
{
    public void SetGameStateRunning()
    {
        GameManager.Instance.SetGameState(GameState.Running);
    }
}
