using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    [SerializeField] private List<KitchenObjectSO> m_validKitchenObjectsSO;

    private List<KitchenObjectSO> m_kitchenObjectSOList;

    private void Awake()
    {
        m_kitchenObjectSOList = new List<KitchenObjectSO>();
    }

    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
    {
        if(!m_validKitchenObjectsSO.Contains(kitchenObjectSO))
            return false;

        if(m_kitchenObjectSOList.Contains(kitchenObjectSO))
            return false;
        else
        {
            m_kitchenObjectSOList.Add(kitchenObjectSO);
            return true;
        }
    }
}
