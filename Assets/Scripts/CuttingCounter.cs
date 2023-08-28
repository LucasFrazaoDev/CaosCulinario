using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO m_cuttingKitchenObjectSO;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            // No kitchen object
            if (player.HasKitchenObject())
            {
                // Player is carrying something
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else
            {
                // Player has nothing
            }
        }
        else
        {
            // There is a kitchen object
            if (player.HasKitchenObject())
            {
                // player has something
            }
            else
            {
                // player has nothing
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

    public override void InteractAlternate(Player player)
    {
        if(HasKitchenObject())
        {
            // There is a kitchen object
            GetKitchenObject().DestroySelf();

            KitchenObject.SpawnKitchenObject(m_cuttingKitchenObjectSO, this);
        }
    }
}
