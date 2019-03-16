/*
author： Lai Zhang Yin，
description ： If you have any question or suggestion, please add QQ/Wechat : 782966734
*/
using System.Collections;
using System.Collections.Generic;
using UnityEditor.IMGUI.Controls;
using UnityEngine;


public class PrefabsTreeItem : TreeViewItem
{
    private string name;

    public bool defaultSelect;

    public string Name
    {
        get
        {
            return name;
        }

        set
        {
            name = value;
        }
    } 

    public PrefabsTreeItem(int id, int depth, string name) : base (id, depth, name)
    {
        this.id = id;
        this.depth = depth;
        this.name = name;
        this.defaultSelect = true;
    }

    private PrefabsTreeItem() : base (0, -1)
    {

    }
}

/*
author： Lai Zhang Yin，
description ： If you have any question or suggestion, please add QQ/Wechat : 782966734
*/
