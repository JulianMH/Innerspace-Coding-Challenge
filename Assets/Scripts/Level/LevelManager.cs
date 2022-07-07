using System.Collections;
using UnityEngine;

/// <summary> Manages the state of the level </summary>
public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private ScoreManager scoreManager;

    [SerializeField]
    private MainCharacter mainCharacterPrefab;

    [SerializeField]
    private GameObject platformPrefab;

    [SerializeField]
    private BoxCollider gameWorldBounds;

    [SerializeField]
    private float platformDistance = 1f;

    [SerializeField]
    private float secondsUntilNextPlatformSpawn = 0f;

    [SerializeField]
    [Range(5, 100)]
    private int initialSpeed = 5;

    void Start()
    {
        ResetGame();

        ApplyGameState(GameManager.Instance.GameState);
        GameManager.Instance.GameStateChanged += ApplyGameState;
    }

    void OnDestroy()
    {
        GameManager.Instance.GameStateChanged -= ApplyGameState;
    }

    void Update()
    {
        var gameWorldShiftSpeed = Mathf.Sqrt(scoreManager.Score + initialSpeed);

    
        secondsUntilNextPlatformSpawn -= Time.deltaTime;
        if (secondsUntilNextPlatformSpawn <= 0)
        {
            secondsUntilNextPlatformSpawn = platformDistance / gameWorldShiftSpeed;
            SpawnPlatform();
        }

        var levelObjects = GetComponentsInChildren<LevelObject>();
        foreach (var levelObject in levelObjects)
        {
            levelObject.ShiftUpwards(gameWorldShiftSpeed * Time.deltaTime);
        }

        var mainCharacter = GetComponentInChildren<MainCharacter>();
        if (!gameWorldBounds.bounds.Contains(mainCharacter.transform.position))
        {
            GameManager.Instance.SetGameState(GameState.GameOver);
        }
    }

    void SpawnPlatform()
    {
        var instantiatedPlatform = Instantiate(platformPrefab, new Vector3(0, gameWorldBounds.bounds.min.y, 0), Quaternion.identity);
        var movingPlatform = instantiatedPlatform.GetComponent<MovingPlatform>();
        instantiatedPlatform.transform.parent = gameObject.transform;
        movingPlatform.totalWidth = gameWorldBounds.size.x;
        movingPlatform.despawnPositionY = gameWorldBounds.bounds.max.y;
        movingPlatform.gapRelativePosition = Random.Range(0.1f, 0.9f);
        movingPlatform.gapRelativeWidth = 0.2f;
        movingPlatform.scoreManager = scoreManager;
        movingPlatform.mainCharacter = GetComponentInChildren<MainCharacter>();
    }

    public void ResetGame()
    {
        scoreManager.ResetScore();

        for(var i = 0; i < transform.childCount; ++i)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        var mainCharacter = Instantiate(mainCharacterPrefab);
        mainCharacter.transform.parent = transform;
        mainCharacter.transform.position = gameWorldBounds.center;
    }

    void ApplyGameState(GameState gameState)
    {
        enabled = gameState == GameState.Running;
        var levelObjects = GetComponentsInChildren<LevelObject>();
        foreach (var levelObject in levelObjects)
        {
            levelObject.enabled = enabled;
        }
    }
}
