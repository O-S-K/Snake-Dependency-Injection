using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public Dialog[] Dialogs;

    public void Show<T>() where T : Dialog
    {
        foreach (var dialog in Dialogs)
        {
            if (dialog is T)
            {
                dialog.Show();
            }
            else
            {
                dialog.Hide();
            }
        }
    }
    
    public T Get<T>() where T : Dialog
    {
        foreach (var dialog in Dialogs)
        {
            if (dialog is T)
            {
                return dialog as T;
            }
        }
        return null;
    }
}