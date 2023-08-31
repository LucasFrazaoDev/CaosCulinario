using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CuttingCounter;

public class StoveCounter : BaseCounter
{
    private enum State
    {
        Idle,
        Frying,
        Fried,
        Burned
    }

    [SerializeField] private FryingRecipeSO[] m_fryingRecipeSOArray;

    private State m_currentState;
    private float m_fryingTimer;
    private float m_burningTimer;
    private FryingRecipeSO m_fryingRecipeSO;

    private void Start()
    {
        m_currentState = State.Idle;
    }

    private void Update()
    {
        if (HasKitchenObject())
        {
            switch (m_currentState)
            {
            case State.Idle:
                break;
            case State.Frying:
                m_fryingTimer += Time.deltaTime;
                if (m_fryingTimer > m_fryingRecipeSO.fryingTimerMax)
                {
                    // Fried
                    GetKitchenObject().DestroySelf();

                    KitchenObject.SpawnKitchenObject(m_fryingRecipeSO.output, this);
                        Debug.Log("Assou kkkk");
                    m_currentState = State.Idle;
                        m_burningTimer = 0f;
                }
                break;
            case State.Fried:
                    m_burningTimer += Time.deltaTime;
                    if (m_burningTimer > m_fryingRecipeSO.fryingTimerMax)
                    {
                        // Fried
                        GetKitchenObject().DestroySelf();

                        KitchenObject.SpawnKitchenObject(m_fryingRecipeSO.output, this);
                        Debug.Log("Assou kkkk");
                        m_currentState = State.Burned;
                    }
                    break;
            case State.Burned:
                break;
            default:
                break;
            }
            Debug.Log(m_currentState);
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
                
                    m_currentState = State.Frying;
                    m_fryingTimer = 0f;
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
