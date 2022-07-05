using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : LevelObject
{
    [SerializeField] private float gravity = 1.0f;
    [SerializeField] private float speed;
    [SerializeField] private CharacterController characterController;

    void Start()
    {
    }

    public override void ShiftUpwards(float amount)
    {
        characterController.enabled = false;
        this.transform.position += Vector3.up * amount;
        characterController.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {

        var movement = new Vector3(0, -gravity * 10f * Time.deltaTime, 0);
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            movement += Vector3.left * speed;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            movement += Vector3.right * speed;
        }
        characterController.Move(movement);
    }

}
