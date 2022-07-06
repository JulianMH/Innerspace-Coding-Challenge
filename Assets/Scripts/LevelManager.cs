using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary> Manages the state of the level </summary>
public class LevelManager : MonoBehaviour
{
    public GameState GameState { get; private set; }

    [SerializeField]
    public ScoreManager scoreManager;

    [SerializeField]
    public GameObject pauseMenu;

    [SerializeField]
    public GameObject gameOverMenu;

    [SerializeField]
    public MainCharacter mainCharacterPrefab;

    private MainCharacter mainCharacter;

    [SerializeField]
    public GameObject platformPrefab;

    [SerializeField]
    public BoxCollider gameWorldBounds;

    [SerializeField]
    private float platformDistance = 1f;

    [SerializeField]
    private float secondsUntilNextPlatformSpawn = 0f;

    [SerializeField]
    [Range(5, 100)]
    private int initialSpeed = 5;

    void Start()
    {
        StartNewGame();
    }

    void Update()
    {
        var gameWorldShiftSpeed = Mathf.Sqrt(scoreManager.Score + initialSpeed);

        secondsUntilNextPlatformSpawn -= Time.deltaTime;
        if (secondsUntilNextPlatformSpawn <= 0)
        {
            secondsUntilNextPlatformSpawn = platformDistance / gameWorldShiftSpeed;
            var instantiatedPlatform = Instantiate(platformPrefab, new Vector3(0, gameWorldBounds.bounds.min.y, 0), Quaternion.identity);
            var movingPlatform = instantiatedPlatform.GetComponent<MovingPlatform>();
            instantiatedPlatform.transform.parent = gameObject.transform;
            movingPlatform.totalWidth = gameWorldBounds.size.x;
            movingPlatform.despawnPositionY = gameWorldBounds.bounds.max.y;
            movingPlatform.gapRelativePosition = Random.Range(0.1f, 0.9f);
            movingPlatform.gapRelativeWidth = 0.2f;
            movingPlatform.scoreManager = scoreManager;
            movingPlatform.mainCharacter = mainCharacter;
        }

        var levelObjects = GetComponentsInChildren<LevelObject>();
        foreach (var levelObject in levelObjects)
        {
            levelObject.ShiftUpwards(gameWorldShiftSpeed * Time.deltaTime);
        }

        if (!gameWorldBounds.bounds.Contains(mainCharacter.transform.position))
        {
            SetGameOver();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    void StartNewGame()
    {
        scoreManager.ResetScore();

        for(var i = 0; i < transform.childCount; ++i)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        mainCharacter = Instantiate(mainCharacterPrefab);
        mainCharacter.transform.parent = transform;
        mainCharacter.transform.position = gameWorldBounds.center;

        SetGameState(GameState.Running);
    }

    private void SetGameState(GameState gameState)
    {
        GameState = gameState;

        enabled = GameState == GameState.Running;
        var levelObjects = GetComponentsInChildren<LevelObject>();
        foreach (var levelObject in levelObjects)
        {
            levelObject.enabled = enabled;
        }

        pauseMenu.SetActive(GameState == GameState.Paused);
        gameOverMenu.SetActive(GameState == GameState.GameOver);
    }

    void PauseGame()
    {
        SetGameState(GameState.Paused);
    }

    void ResumeGame()
    {
        SetGameState(GameState.Running);
    }

    void SetGameOver()
    {
        SetGameState(GameState.GameOver);
    }

    void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
