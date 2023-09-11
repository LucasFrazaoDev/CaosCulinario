using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    public static event EventHandler OnAnyObjectPlacedHere;

    public static void ResetStaticData()
    {
        OnAnyObjectPlacedHere = null;
    }

    [SerializeField] private Transform m_counterTopPoint;

    private KitchenObject m_kitchenObject;

    public virtual void Interact(Player player)
    {
        Debug.LogError("Base counter.interact");
    }
    
    public virtual void InteractAlternate(Player player)
    {
        //Debug.LogError("Base counter.interactAlternate");
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return m_counterTopPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        m_kitchenObject = kitchenObject;

        if(m_kitchenObject != null)
        {
            OnAnyObjectPlacedHere?.Invoke(this, EventArgs.Empty);
        }
    }

    public KitchenObject GetKitchenObject()
    {
        return m_kitchenObject;
    }

    public void ClearKitchenObject()
    {
        m_kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return m_kitchenObject != null;
    }
}
