using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touchable : MonoBehaviour, IInteractable
{
    public void Interact(PlayerCtrl player)
    {
        Debug.Log("Touch");
    }
}
