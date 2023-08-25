using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO m_kitchenObjectSO;

    private ClearCounter m_clearCounter;

    public KitchenObjectSO GetKitchenObjectSO()
    {
        return m_kitchenObjectSO;
    }

    public void SetClearCounter(ClearCounter clearCounter)
    {
        if(m_clearCounter != null)
        {
            m_clearCounter.ClearKitchenObject();
        }
        m_clearCounter = clearCounter;

        if(clearCounter.HasKitchenObject())
        {
            Debug.LogError("Counter já tem um objeto");
        }
        clearCounter.SetKitchenObject(this);

        transform.parent = clearCounter.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public ClearCounter GetClearCounter()
    {
        return m_clearCounter;
    }
}
