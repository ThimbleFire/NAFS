using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMove : MonoBehaviour
{
    public delegate void OnTileChangeHandler(Vector3Int tilePosition);
    public static event OnTileChangeHandler OnTileChange;

    public delegate void OnDirectionChangeHandler(Vector3 direction);
    public static event OnDirectionChangeHandler OnDirectionChange;

    public delegate void OnMoveHandler(Vector3 position);
    public static event OnMoveHandler OnMove;

    public float walkSpeed = 1.0f;
    public PlayerAnimator pAnimator;
    public Grid grid;
    public Vector3Int OnTile { get { return grid.WorldToCell(transform.position); } }

    private Vector3Int lastTile = Vector3Int.zero;
    private Vector3 lastDirection;
    private Vector3 lastPosition;

    private void Awake() => lastPosition = transform.position;

    private void Update()
    {
        Vector3 velocity = Vector3.zero;

        if (Input.GetKey(KeyCode.A)) {
            velocity += Time.smoothDeltaTime * walkSpeed * Vector3.left;
        }
        if (Input.GetKey(KeyCode.D)) {
            velocity += Time.smoothDeltaTime * walkSpeed * Vector3.right;
        }
        if (Input.GetKey(KeyCode.W)) {
            velocity += Time.smoothDeltaTime * walkSpeed * Vector3.up;
        }
        if (Input.GetKey(KeyCode.S)) {
            velocity += Time.smoothDeltaTime * walkSpeed * Vector3.down;
        }

        if (Input.GetKeyDown(KeyCode.F)) {
            pAnimator.UseTool();
        }

        Vector3 direction = velocity.normalized;

        transform.Translate(velocity);
        pAnimator.UpdateVelocity(direction);

        if (lastDirection != direction) {
            OnDirectionChange?.Invoke(direction);
            lastDirection = direction;
        }

        if(lastTile != OnTile) {
            OnTileChange?.Invoke(OnTile);
            lastTile = OnTile;
        }

        if(lastPosition != transform.position) {
            OnMove?.Invoke(transform.position);
            lastPosition = transform.position;
            PlayerCharacter.WorldPosition = transform.position;
        }
    }
}
