using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Add new Interfaces for more listeners
#region
public interface ICommunicatorListener<T>
{
    void  OnValueChanged(T value);
}

public interface IGroundedListener : ICommunicatorListener<bool>
{

}

public interface IMoveSpeedListener : ICommunicatorListener<float>
{

}
public interface ILookChangeListener : ICommunicatorListener<float>
{

}

public interface IStoveOnListener : ICommunicatorListener<bool>
{

}

public interface IPlayerDeath : ICommunicatorListener<bool>
{

}

#endregion


public class MovementCommunicator : MonoBehaviour
{
    public static MovementCommunicator instance;

    private List<IGroundedListener> groundedListeners;
    private List<IMoveSpeedListener> moveSpeedListeners;
    private List<ILookChangeListener> lookListeners;
    private List<IStoveOnListener> stoveListener;
    private List<IPlayerDeath> playerDeathListeners;


    private void Awake()
    {
        instance = this;
        groundedListeners = new List<IGroundedListener>();
        moveSpeedListeners = new List<IMoveSpeedListener>();
        lookListeners = new List<ILookChangeListener>();
        stoveListener = new List<IStoveOnListener>();
        playerDeathListeners = new List<IPlayerDeath>();
    }

    //Adding and removing listeners
    #region
    public void AddGroundedListener(IGroundedListener listener)
    {
        groundedListeners.Add(listener);
    }

    public void RemoveJumpListener(IGroundedListener listener)
    {
        groundedListeners.Remove(listener);
    }


    public void AddMoveListener(IMoveSpeedListener listener)
    {
        moveSpeedListeners.Add(listener);
    }

    public void RemoveMoveListener(IMoveSpeedListener listener)
    {
        moveSpeedListeners.Remove(listener);
    }

    public void AddLookListener(ILookChangeListener listener)
    {
        lookListeners.Add(listener);
    }

    public void RemoveLookListener(ILookChangeListener listener)
    {
        lookListeners.Remove(listener);
    }

    public void AddStoveListener(IStoveOnListener listener)
    {
        stoveListener.Add(listener);
    }

    public void RemoveStoveListener(IStoveOnListener listener)
    {
        stoveListener.Remove(listener);
    }

    public void AddPlayerDeathListeners(IPlayerDeath listener)
    {
        playerDeathListeners.Add(listener);
    }

    public void RemovePlayerDeathListeners(IPlayerDeath listener)
    {
        playerDeathListeners.Remove(listener);
    }
    #endregion




    //Notify listeners
    public void NotifyGroundedListeners(bool isGrounded)
    {
        foreach (var listener in groundedListeners)
        {
            listener.OnValueChanged(isGrounded);
        }
    }

    public void NotifyMoveSpeedListeners(float moveSpeed)
    {
        foreach (var listener in moveSpeedListeners)
        {
            listener.OnValueChanged(moveSpeed);
        }
    }

    public void NotifyLookListeners(float look)
    {
        foreach (var listener in lookListeners)
        {
            listener.OnValueChanged(look);
        }
    }

    public void NotifyStoveListeners(bool isOn)
    {
        foreach (var listener in stoveListener)
        {
            listener.OnValueChanged(isOn);
        }
    }

    public void NotifyDeathListeners(bool hasDied)
    {
        foreach (var listener in playerDeathListeners)
        {
            listener.OnValueChanged(hasDied);
        }
    }
}
