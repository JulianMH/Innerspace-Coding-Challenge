using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> Manages the state of the level </summary>
public class LevelManager : MonoBehaviour
{
    public int Score { get; private set; }

    [SerializeField]
    public GameObject platformPrefab;

    [SerializeField]
    public BoxCollider gameWorldBounds;

    [SerializeField]
    private float secondsUntilNextPlatformSpawn = 1f;
    
    void Start()
    {
        
    }

    void Update()
    {
        secondsUntilNextPlatformSpawn -= Time.deltaTime;
        if(secondsUntilNextPlatformSpawn <= 0)
        {
            secondsUntilNextPlatformSpawn = 2f;
            var instantiatedPlatform = Instantiate(platformPrefab, new Vector3(0, gameWorldBounds.bounds.min.y, 0), Quaternion.identity);
            var movingPlatform = instantiatedPlatform.GetComponent<MovingPlatform>();
            movingPlatform.totalWidth = gameWorldBounds.size.x;
            movingPlatform.despawnPositionY = gameWorldBounds.bounds.max.y;
            movingPlatform.gapRelativePosition = Random.Range(0.1f, 0.9f);
            movingPlatform.gapRelativeWidth = 0.2f;
        }
    }

    public void IncrementScore()
    {
        Score++;
    }

    public void Reset()
    {
        Score = 0;
        // reset logic
    }
}
