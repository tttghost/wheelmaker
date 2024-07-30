using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
interface IPanel
{
    panel_Base SetData<T2>(T2 t2);
    panel_Base SetData<T1, T2>(T1 t1, T2 t2);
}

public class panel_Base : ui_Base, IPanel
{

    public virtual panel_Base Push()
    {
        if (UIManager.instance.stackPanel.Count > 0)
        {
            ui_Base ui_Base = UIManager.instance.stackPanel.Peek();
            ui_Base.Close();
        }

        UIManager.instance.stackPanel.Push(this);
        Open();

        return this;
    }

    public virtual panel_Base Swap()
    {
        if (UIManager.instance.stackPanel.Count > 0)
        {
            ui_Base ui_Base = UIManager.instance.stackPanel.Pop();
            ui_Base.Close();
        }

        UIManager.instance.stackPanel.Push(this);
        Open();

        return this;
    }

    public virtual panel_Base SetData<T2>(T2 t2)
    {
        return this;
    }

    public virtual panel_Base SetData<T1, T2>(T1 t1, T2 t2)
    {
        return this;
    }
}
