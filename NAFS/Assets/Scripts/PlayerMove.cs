using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(PlayerAnimator))]
public class PlayerMove : MonoBehaviour
{
    public delegate void OnTileChangeHandler(Vector3Int tilePosition);
    public static event OnTileChangeHandler OnTileChange;

    public delegate void OnDirectionChangeHandler();
    public static event OnDirectionChangeHandler OnDirectionChange;

    public delegate void OnMoveHandler(Vector3 position);
    public static event OnMoveHandler OnMove;

    public PlayerAnimator playerAnimator;
    public Terrain terrain;
    public Grid grid;

    private float WalkSpeed { get; set; } = 1.0f;
    private Vector3Int LastTile { get; set; } = Vector3Int.zero;
    private Vector3 LastDirection { get; set; } = Vector3.down;
    private Vector3 LastPosition { get; set; }

    public Vector3Int OnTile { get { return grid.WorldToCell(transform.position); } }

    private void Awake() => LastPosition = transform.position;

    private void Update()
    {
        Vector3 velocity = Vector3.zero;

        if (Input.GetKey(KeyCode.A)) {
            velocity += Time.smoothDeltaTime * WalkSpeed * Vector3.left;
        }
        if (Input.GetKey(KeyCode.D)) {
            velocity += Time.smoothDeltaTime * WalkSpeed * Vector3.right;
        }
        if (Input.GetKey(KeyCode.W)) {
            velocity += Time.smoothDeltaTime * WalkSpeed * Vector3.up;
        }
        if (Input.GetKey(KeyCode.S)) {
            velocity += Time.smoothDeltaTime * WalkSpeed * Vector3.down;
        }

        PlayerCharacter.FacingDirection = velocity.normalized;

        if (Input.GetKeyDown(KeyCode.F)) {
            var resultingItemBehaviour = playerAnimator.UseTool(LastDirection);

            if (resultingItemBehaviour == Item.Behaviour.NONE)
                return;
            else
            {
                Action(resultingItemBehaviour);
            }
        }
        
        transform.Translate(velocity);
        playerAnimator.UpdateVelocity(PlayerCharacter.FacingDirection);

        // OnDirectionChange
        if (LastDirection != PlayerCharacter.FacingDirection && PlayerCharacter.FacingDirection != Vector3.zero ) {
            OnDirectionChange?.Invoke();
            LastDirection = PlayerCharacter.FacingDirection;
        }

        // OnTileChange
        if(LastTile != OnTile) {
            OnTileChange?.Invoke(OnTile);
            LastTile = OnTile;
        }

        // OnMove
        if(LastPosition != transform.position) {
            OnMove?.Invoke(transform.position);
            LastPosition = transform.position;
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
            case Item.Behaviour.SOW:
                terrain.Sow();
                break;
        }
    }
}
