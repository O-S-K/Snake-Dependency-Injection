using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public Dialog[] Dialogs;

    public T Show<T>() where T : Dialog
    {
        Dialog _dialog = null;
        foreach (var dialog in Dialogs)
        {
            if (dialog is T)
            {
                _dialog = dialog;
            }
            else
            {
                dialog.Hide();
            }
        }
        
        _dialog.Show();
        return _dialog as T;
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