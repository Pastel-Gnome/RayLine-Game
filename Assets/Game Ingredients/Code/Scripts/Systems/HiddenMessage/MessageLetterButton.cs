using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageLetterButton : MonoBehaviour
{
    public void PressButton()
    {
        HiddenMessageManager.instance.InputLetter(name);
    }
}
