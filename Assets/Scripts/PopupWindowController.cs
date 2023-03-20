using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupWindowController : MonoBehaviour
{

    public void OpenPopup()
    {
        gameObject.SetActive(true);
    }

    public void ClosePopup()
    {
        gameObject.SetActive(false);
    }
}
