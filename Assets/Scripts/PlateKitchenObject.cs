using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
    public class OnIngredientAddedEventArgs:EventArgs
    {
        public KitchenObjectSO kitchenObjectSO;
    }

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

            OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs { kitchenObjectSO = kitchenObjectSO});
            return true;
        }
    }

    public List<KitchenObjectSO> GetKitchenObjectSOList()
    {
        return m_kitchenObjectSOList;
    }
}
