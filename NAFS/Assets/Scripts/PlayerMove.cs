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

    public PlayerAnimator pAnimator;
    public Grid grid;
    public Terrain terrain;

    private float walkSpeed = 1.0f;
    private Vector3Int lastTile = Vector3Int.zero;
    private Vector3 lastDirection = Vector3.down;
    private Vector3 lastPosition;

    public Vector3Int OnTile { get { return grid.WorldToCell(transform.position); } }

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
            var resultingItemBehaviour = pAnimator.UseTool(lastDirection);

            if (resultingItemBehaviour == Item.Behaviour.NONE)
                return;
            else
            {
                Action(resultingItemBehaviour);
            }
        }

        Vector3 direction = velocity.normalized;
        
        transform.Translate(velocity);
        pAnimator.UpdateVelocity(direction);

        // OnDirectionChange
        if (lastDirection != direction && direction != Vector3.zero ) {
            OnDirectionChange?.Invoke(direction);
            lastDirection = direction;
        }

        // OnTileChange
        if(lastTile != OnTile) {
            OnTileChange?.Invoke(OnTile);
            lastTile = OnTile;
        }

        // OnMove
        if(lastPosition != transform.position) {
            OnMove?.Invoke(transform.position);
            lastPosition = transform.position;
            PlayerCharacter.WorldPosition = transform.position;
        }
    }

    private void Action(Item.Behaviour resultingItemBehaviour)
    {
        switch (resultingItemBehaviour)
        {
            case Item.Behaviour.NONE:
            case Item.Behaviour.DAMAGE:
            case Item.Behaviour.MINE:
            case Item.Behaviour.CHOP_TREE:
                break;
            case Item.Behaviour.DIG_GRASS:
                terrain.RemoveGrass();
                break;
            case Item.Behaviour.TILL_EARTH:
                terrain.Till();
                break;
        }
    }
}
