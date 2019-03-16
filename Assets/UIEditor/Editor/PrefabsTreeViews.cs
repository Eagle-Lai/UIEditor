/*
author： Lai Zhang Yin，
description ： If you have any question or suggestion, please add QQ/Wechat : 782966734
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.IMGUI.Controls;

public class PrefabsTreeViews : TreeView
{
    private PrefabsTreeItem treeItem;

    private Transform parentTransform;

    public Transform ParentTransform
    {
        get
        {
            return parentTransform;
        }
        set
        {

            parentTransform = value;
        }
    }

    private List<TreeViewItem> listItem;

    public PrefabsTreeViews(TreeViewState state) : base(state)
    {
        this.showBorder = true;
    }

    public void Clear()
    {

    }

    protected override TreeViewItem BuildRoot()
    {
        //if(this.parentTransform != null)
        //{
        //    treeItem = new PrefabsTreeItem(this.parentTransform.GetInstanceID(), -1, this.parentTransform.name);
        //}
        PrefabsTreeItem item = new PrefabsTreeItem(1, -1, "root");
        this.AddChildItem();
        // if(this.listItem != null)
        {
            SetupParentsAndChildrenFromDepths(item, this.listItem);
        }
        return item;
    }

    protected override void SelectionChanged(IList<int> selectedIds)
    {

    }
    private void AddChildItem()
    {
        List<TreeViewItem> rows = new List<TreeViewItem>();
        if (this.parentTransform != null)
        {
            GameObject[] transforms = Utils.GetAllUIChild(this.parentTransform.gameObject);
            for (int i = 0; i < transforms.Length; i++)
            {
                PrefabsTreeItem item = this.CreateItem(transforms[i].transform, i);
                rows.Add(item);
                this.SetExpanded(i, true);
            }
        }
        this.listItem = rows;
    }

    private PrefabsTreeItem CreateItem(Transform transform, int id)
    {
        int depth = transform.GetComponentsInParent<Transform>(true).Length <= 0 ? 1 : transform.GetComponentsInParent<Transform>(true).Length;
        PrefabsTreeItem item = new PrefabsTreeItem(id, depth, transform.name);
        if (Utils.SelectedDic.ContainsKey(id))
        {
            item.defaultSelect = Utils.SelectedDic[id];
        }
        else
        {
            Utils.SelectedDic.Add(id, item.defaultSelect);
        }
        return item;
    }

    protected override void RowGUI(RowGUIArgs args)
    {
        Event evt = Event.current;
        extraSpaceBeforeIconAndLabel = 18f;

        // GameObject isStatic toggle
        PrefabsTreeItem item = args.item as PrefabsTreeItem;

        Rect toggleRect = args.rowRect;
        toggleRect.x += GetContentIndent(item);
        toggleRect.width = 16;

        // Ensure row is selected before using the toggle (usability)
        if (evt.type == EventType.MouseDown && toggleRect.Contains(evt.mousePosition))
        {
            SelectionClick(item, false);
        }
        EditorGUI.BeginChangeCheck();
        bool isStatic = EditorGUI.Toggle(toggleRect, item.defaultSelect);
        if (EditorGUI.EndChangeCheck())
        {
            Utils.SelectedDic[item.id] = isStatic;
            EventManager.Dispatch<int, bool>("UISelectEvent", item.id, isStatic);
        }
        // Text
        base.RowGUI(args);
    }
}

/*
author： Lai Zhang Yin，
description ： If you have any question or suggestion, please add QQ/Wechat : 782966734
*/
