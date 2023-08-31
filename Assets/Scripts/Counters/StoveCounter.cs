using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CuttingCounter;

public class StoveCounter : BaseCounter, IHasProgress
{
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public class OnStateChangedEventArgs:EventArgs
    {
        public State state;
    }

    public enum State
    {
        Idle,
        Frying,
        Fried,
        Burned,
    }

    [SerializeField] private FryingRecipeSO[] m_fryingRecipeSOArray;
    [SerializeField] private BurningRecipeSO[] m_burningRecipeSOArray;

    private State m_currentState;
    private float m_fryingTimer;
    private float m_burningTimer;
    private FryingRecipeSO m_fryingRecipeSO;
    private BurningRecipeSO m_burningRecipeSO;

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

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalized = m_fryingTimer / m_fryingRecipeSO.fryingTimerMax });

                    if (m_fryingTimer > m_fryingRecipeSO.fryingTimerMax)
                {
                        // Fried
                        GetKitchenObject().DestroySelf();

                        KitchenObject.SpawnKitchenObject(m_fryingRecipeSO.output, this);

                        m_currentState = State.Fried;
                        m_burningTimer = 0f;
                        m_burningRecipeSO = GetBurningRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = m_currentState});
                    }
                break;
            case State.Fried:
                    m_burningTimer += Time.deltaTime;

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalized = m_burningTimer / m_burningRecipeSO.burningTimerMax });


                    if (m_burningTimer > m_burningRecipeSO.burningTimerMax)
                    {
                        // Fried
                        GetKitchenObject().DestroySelf();

                        KitchenObject.SpawnKitchenObject(m_burningRecipeSO.output, this);
                        m_currentState = State.Burned;

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = m_currentState });

                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalized = 0f});
                    }
                    break;
            case State.Burned:
                break;
            default:
                break;
            }
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

                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = m_currentState });

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalized = m_fryingTimer / m_fryingRecipeSO.fryingTimerMax});
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

                m_currentState = State.Idle;

                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = m_currentState });

                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalized = 0f });
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

    private BurningRecipeSO GetBurningRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (BurningRecipeSO burningRecipeSO in m_burningRecipeSOArray)
        {
            if (burningRecipeSO.input == inputKitchenObjectSO)
                return burningRecipeSO;
        }

        return null;
    }
}
