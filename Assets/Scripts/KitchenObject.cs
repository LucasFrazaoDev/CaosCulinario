using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO m_kitchenObjectSO;

    public KitchenObjectSO GetKitchenObjectSO()
    {
        return m_kitchenObjectSO;
    }
}
