using UnityEngine;
using System.Collections;

public class LevelObject : MonoBehaviour
{
    public virtual void ShiftUpwards(float amount)
    {
        transform.position += Vector3.up * amount;
    }
}
