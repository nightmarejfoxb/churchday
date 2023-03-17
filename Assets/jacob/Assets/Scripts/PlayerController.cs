using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;

    private Vector2 moveInput;
    private bool interactInput;

    private Vector2 facingDir;

    public LayerMask interactLayerMask;

    public Rigidbody2D rig;
    public SpriteRenderer sr;

    // Called every frame.
    void Update ()
    {
        // Set the facing direction.
        if(moveInput.magnitude != 0.0f)
        {
            facingDir = moveInput.normalized;
            sr.flipX = (moveInput.x == 0) ? sr.flipX : moveInput.x > 0;
        }

        // When we press down the interact key.
        if(interactInput)
        {
            TryInteractTile();
            interactInput = false;
        }
    }

    // Called every 0.02 seconds.
    void FixedUpdate ()
    {
        // Move the player based on the input and move speed.
        rig.velocity = moveInput.normalized * moveSpeed;
    }

    // Raycast in facing direction and interact with that tile.
    void TryInteractTile ()
    {
        RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position + facingDir, Vector3.up, 0.1f, interactLayerMask);

        if(hit.collider != null)
        {
            FieldTile tile = hit.collider.GetComponent<FieldTile>();
            tile.Interact();
        }
    }

    // Called when we press a movement key.
    public void OnMoveInput (InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    // Called when we press the interact key.
    public void OnInteractInput (InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            interactInput = true;
        }
    }
}