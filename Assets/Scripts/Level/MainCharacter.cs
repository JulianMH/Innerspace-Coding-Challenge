using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> Player character of the game. </summary>
public class MainCharacter : LevelObject
{
    [SerializeField]
    private float minGravity = -1.0f;

    [SerializeField]
    private float gravityAccelleration = -1.0f;

    [SerializeField]
    private float speed;

    [SerializeField]
    private float currentGravityVelocity = 1.0f;

    [SerializeField]
    private CharacterController characterController;

    public override void ShiftUpwards(float amount)
    {
        // Character Controllers do not like being teleported.
        // The hacky solution to this is disabling them for the teleport.
        // The clean solution would be to write a custom character controller
        // that supports shifting around with the game world, but this costs
        // quite a lot of time, so the hacky solution is employed here.
        characterController.enabled = false;
        transform.position += Vector3.up * amount;
        characterController.enabled = true;
    }

    void Update()
    {
        currentGravityVelocity += gravityAccelleration * Time.deltaTime;
        if (characterController.collisionFlags == CollisionFlags.Below)
        {
            currentGravityVelocity = minGravity;
        }

        var movement = new Vector3(0, currentGravityVelocity * Time.deltaTime, 0);
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            movement += Vector3.left * speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            movement += Vector3.right * speed * Time.deltaTime;
        }
        characterController.Move(movement);
    }
}
