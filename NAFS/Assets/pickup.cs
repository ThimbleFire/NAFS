using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickup : MonoBehaviour
{
    public bool pickupable = true;
    public ItemMono itemMono;

    private void Awake()
    {
        PlayerMove.OnMove += PlayerMove_OnMove;
    }

    private void PlayerMove_OnMove(Vector3 position)
    {
        float distance = Vector3.Distance(position, transform.position);

        if( distance <= 0.32f && Inventory.CountEmptySlots > 0)
        {
            pickupable = false;
            PlayerMove.OnMove -= PlayerMove_OnMove;
        }
    }

    private void Update()
    {
        if (pickupable == false)
            transform.position = Vector3.MoveTowards(transform.position, PlayerCharacter.WorldPosition, 0.001f);

        if (Vector3.Distance(PlayerCharacter.WorldPosition, transform.position) <= 0.01f)
        {
            Inventory.AddItem(itemMono.item);
            Destroy(gameObject, 1.0f);
        }
    }
}
