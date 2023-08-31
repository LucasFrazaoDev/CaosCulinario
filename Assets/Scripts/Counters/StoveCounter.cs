using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CuttingCounter;

public class StoveCounter : BaseCounter
{
    [SerializeField] private FryingRecipeSO[] m_fryingRecipeSOArray;

    private float m_fryingTimer;
    private FryingRecipeSO m_fryingRecipeSO;

    private void Update()
    {
        if(HasKitchenObject())
        {
            m_fryingTimer += Time.deltaTime;
            if(m_fryingTimer > m_fryingRecipeSO.fryingTimerMax)
            {
                // Fried
                m_fryingTimer = 0f;
                Debug.Log("Assou!");
                GetKitchenObject().DestroySelf();

                KitchenObject.SpawnKitchenObject(m_fryingRecipeSO.output, this);
            }
            Debug.Log(m_fryingTimer);
        }
    }

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            // No kitchen object
            if (player.HasKitchenObject())
            {
                // Player is carrying something
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    // Player carrying something that can fry
                    player.GetKitchenObject().SetKitchenObjectParent(this);

                    m_fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                }
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

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        return fryingRecipeSO != null;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);

        if (fryingRecipeSO != null)
            return fryingRecipeSO.output;
        else
            return null;
    }

    private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (FryingRecipeSO fryingRecipeSO in m_fryingRecipeSOArray)
        {
            if (fryingRecipeSO.input == inputKitchenObjectSO)
                return fryingRecipeSO;
        }

        return null;
    }
}
