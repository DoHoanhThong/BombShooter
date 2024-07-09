using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public enum CharacterState
{
    Normal,
    Requesting,
    Playing,
    Pickup,   
    Boring,
}
public enum RequestType
{
    None,
    Feed,
    Bath,
}
[System.Serializable]
public class RuntimeData
{
    public PetData petData;

    public CharacterState currentState;

    public RequestType lastRequest;

    private List<RequestType> availableRequest = new List<RequestType>((RequestType[])Enum.GetValues(typeof(RequestType)));
    public RuntimeData(PetData data)
    {
        this.petData = data; 
        currentState = CharacterState.Normal;
        lastRequest = RequestType.None;
        availableRequest.Remove(RequestType.None);
    }   
    public void ResetAvailableStates()
    {
        availableRequest = new List<RequestType>((RequestType[])Enum.GetValues(typeof(RequestType)));
    }
    public RequestType GetRandomRequest()
    {
        if (availableRequest.Count == 0)
        {
            ResetAvailableStates();
            availableRequest.Remove(lastRequest);
            availableRequest.Remove(RequestType.None);
        }

        int randomIndex = UnityEngine.Random.Range(0, availableRequest.Count);
        RequestType randomState = availableRequest[randomIndex];
        availableRequest.RemoveAt(randomIndex);
        lastRequest = randomState;

        return randomState;
    }
}
