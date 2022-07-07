using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary> Manages the state of the whole application </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameState GameState { get { return _gameState; } }

    public delegate void GameStateChangedEvent(GameState gameState);
    public event GameStateChangedEvent GameStateChanged;

    [SerializeField]
    GameState _gameState;

    [SerializeField]
    string menuScene;

    [SerializeField]
    string gameScene;


    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void SetGameState(GameState gameState)
    {
        _gameState = gameState;
        if (GameStateChanged != null)
        {
            GameStateChanged(gameState);
        }

        var requiredScene = gameState == GameState.Menu ? menuScene : gameScene;
        if (SceneManager.GetActiveScene().name != requiredScene)
        {
            SceneManager.LoadScene(requiredScene);
        }
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            switch(GameState)
            {
                case GameState.Running:
                    SetGameState(GameState.Paused);
                    break;
                case GameState.Paused:
                    SetGameState(GameState.Running);
                    break;
                case GameState.GameOver:
                    SetGameState(GameState.Menu);
                    break;
                case GameState.Menu:
                    Application.Quit();
                    break;

            }
        }
    }
}