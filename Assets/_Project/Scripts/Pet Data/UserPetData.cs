using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserPetData : MonoBehaviour
{
    public string petID;
    public State currentState;
    public enum State
    {
        Normal,
        Hungry,
        Dirty,
        Boring,
    }
    public UserPetData(string petID)
    {
        this.petID = petID;
        currentState = State.Normal;
    }
}
