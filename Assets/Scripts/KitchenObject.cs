using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO m_kitchenObjectSO;

    private IKitchenObjectParent m_kitchenObjectParent;

    public KitchenObjectSO GetKitchenObjectSO()
    {
        return m_kitchenObjectSO;
    }

    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent)
    {
        if(m_kitchenObjectParent != null)
            m_kitchenObjectParent.ClearKitchenObject();

        m_kitchenObjectParent = kitchenObjectParent;

        if(kitchenObjectParent.HasKitchenObject())
            Debug.LogError("Kitchen object parent já tem um objeto");

        kitchenObjectParent.SetKitchenObject(this);

        transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public IKitchenObjectParent GetKitchenObjectParent()
    {
        return m_kitchenObjectParent;
    }

    public void DestroySelf()
    {
        m_kitchenObjectParent.ClearKitchenObject();

        Destroy(gameObject);
    }

    public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent kitchenObjectParent)
    {
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
        KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();

        kitchenObject.SetKitchenObjectParent(kitchenObjectParent);

        return kitchenObject;

    }
}
