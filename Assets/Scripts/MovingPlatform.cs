using UnityEngine;

[ExecuteInEditMode()]
public class MovingPlatform : LevelObject
{
    [SerializeField]
    [Range(0.0f, float.MaxValue)]
    public float totalWidth = 10f;

    [SerializeField]
    [Range(0.0f, float.MaxValue)]
    public float despawnPositionY = 10f;

    [SerializeField]
    [Range(0.0f, 1.0f)]
    public float gapRelativePosition = 0.5f;

    [SerializeField]
    [Range(0.0f, 1.0f)]
    public float gapRelativeWidth = 0.1f;

    [SerializeField]
    private GameObject platformLeft;

    [SerializeField]
    private GameObject platformRight;

    void Start()
    {
        SetupPlatforms();
    }

    void SetupPlatforms()
    {
        var halfWidth = 0.5f * totalWidth;

        var platformLeftStart = -halfWidth;
        var plaformLeftWidth = (gapRelativePosition - 0.5f * gapRelativeWidth) * totalWidth;
        var platformRightStart = platformLeftStart + plaformLeftWidth + (gapRelativeWidth * totalWidth);
        var plaformRightWidth = halfWidth - platformRightStart;


        platformLeft.SetActive(plaformLeftWidth > 0);
        if (plaformLeftWidth > 0)
        {
            platformLeft.transform.localPosition = new Vector3(platformLeftStart + plaformLeftWidth * 0.5f, 0, 0);
            platformLeft.transform.localScale = new Vector3(plaformLeftWidth, 1, 5);
        }
        
        platformRight.SetActive(plaformRightWidth > 0);
        if(plaformRightWidth > 0)
        {
            platformRight.transform.localPosition = new Vector3(platformRightStart + plaformRightWidth * 0.5f, 0, 0);
            platformRight.transform.localScale = new Vector3(plaformRightWidth, 1, 5);
        }
    }

    void Update()
    {
        SetupPlatforms();

        if (Application.isPlaying)
        {

            if(transform.position.y > despawnPositionY)
            {
                Destroy(gameObject);
            }
        }
    }
}