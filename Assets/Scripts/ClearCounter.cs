using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] private KitchenObjectSO m_kitchenObjectSO;
    [SerializeField] private Transform m_counterTopPoint;

    private KitchenObject m_kitchenObject;

    public void Interact(Player player)
    {
        if (m_kitchenObject == null)
        {
            Transform kitchenObjectTransform = Instantiate(m_kitchenObjectSO.prefab, m_counterTopPoint);
            kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(this);
        }
        else
        {
            // Give object to the player
            m_kitchenObject.SetKitchenObjectParent(player);
        }
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return m_counterTopPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        m_kitchenObject = kitchenObject;
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
