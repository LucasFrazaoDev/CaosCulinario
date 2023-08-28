using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    public event EventHandler OnPlayerGrabbedObject;

    [SerializeField] private KitchenObjectSO m_kitchenObjectSO;

    public override void Interact(Player player)
    {
         Transform kitchenObjectTransform = Instantiate(m_kitchenObjectSO.prefab);
         kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(player);
        OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
    }

    
}
