using UnityEngine;
using System.Collections;

public class LevelObject : MonoBehaviour
{
    public virtual void ShiftUpwards(float amount)
    {
        this.transform.position += Vector3.up * amount;
    }
}
