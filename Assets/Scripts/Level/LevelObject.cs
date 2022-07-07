using UnityEngine;
using System.Collections;

/// <summary> An object of the game level, supporting the scrolling of the level. </summary>
public class LevelObject : MonoBehaviour
{
    public virtual void ShiftUpwards(float amount)
    {
        transform.position += Vector3.up * amount;
    }
}
