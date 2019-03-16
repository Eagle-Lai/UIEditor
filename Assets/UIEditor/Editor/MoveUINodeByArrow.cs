/*
author： Lai Zhang Yin，
description ： If you have any question or suggestion, please add QQ/Wechat : 782966734
*/
using UnityEngine;
using UnityEditor;
public class MoveUINodeByArrow
{
    public static bool isMoveUIByArrow = true;
    [InitializeOnLoadMethod]
    private static void Init()
    {
        SceneView.onSceneGUIDelegate += OnSceneGUI;
    }
    private static void OnSceneGUI(SceneView view)
    {
        Event e = Event.current;
        if (e.type == EventType.KeyDown && isMoveUIByArrow)
        {
            foreach (var item in Selection.transforms)
            {
                Transform trans = item;
                if (trans != null)
                {
                    bool isHandled = false;
                    if (e.keyCode == KeyCode.UpArrow)
                    {
                        trans.localPosition = new Vector3(trans.localPosition.x, trans.localPosition.y + 1, trans.localPosition.z);
                        isHandled = true;
                    }
                    if (e.keyCode == KeyCode.DownArrow)
                    {
                        trans.localPosition = new Vector3(trans.localPosition.x, trans.localPosition.y - 1, trans.localPosition.z);
                        isHandled = true;
                    }
                    if (e.keyCode == KeyCode.RightArrow)
                    {
                        trans.localPosition = new Vector3(trans.localPosition.x + 1, trans.localPosition.y, trans.localPosition.z);
                        isHandled = true;
                    }
                    if (e.keyCode == KeyCode.LeftArrow)
                    {
                        trans.localPosition = new Vector3(trans.localPosition.x - 1, trans.localPosition.y, trans.localPosition.z);
                        isHandled = true;
                    }
                    if (isHandled)
                    {
                        Event.current.Use();
                    }
                }
            }
        }
    }
}

/*
author： Lai Zhang Yin，
description ： If you have any question or suggestion, please add QQ/Wechat : 782966734
*/
