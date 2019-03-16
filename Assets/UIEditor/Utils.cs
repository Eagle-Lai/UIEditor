/*
author： Lai Zhang Yin，
description ： If you have any question or suggestion, please add QQ/Wechat : 782966734
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// 1 Text,  2 Image,  3 RawImage,  4 Button,  5 Toggle,   6 Slider,   7 Scrollbar,  8  InputField, 9  ScrollRect,
/// </summary>
public enum UICompleteEnum
{
    Transform,
    Text,
    Image,
    RawImage,
    Button,
    Toggle,
    Slider,
    Scrollbar,
    InputField,
    ScrollRect,
    Dropdown,
    Mask,
}

public class Utils : MonoBehaviour
{
   

    /// <summary>
    /// game object 的选择状态字典
    /// </summary>
    private static Dictionary<int, bool> selectedDic;
    /// <summary>
    /// game object 的选择状态字典
    /// </summary>
    public static Dictionary<int, bool> SelectedDic
    {
        get
        {
            if (selectedDic == null)
            {
                selectedDic = new Dictionary<int, bool>();
            }
            return selectedDic;
        }

        set
        {
            selectedDic = value;
        }
    }

    /// <summary>
    /// 获取所有选择的game object和它们的UI组件
    /// </summary>
    /// <param name="uiList"></param>
    /// <returns></returns>
    public static List<UIBehaviour> SelectAllUIComponent(List<GameObject> uiList)
    {
        List<UIBehaviour> resultList = new List<UIBehaviour>();
        if (uiList != null && uiList.Count > 0)
        {
            for (int i = 0; i < uiList.Count; i++)
            {
                UIBehaviour[] tmp = uiList[i].GetComponents<UIBehaviour>();
                if(tmp.Length > 0)
                { 
                    for (int j = 0; j < tmp.Length; j++)
                    {
                        if (!resultList.Contains(tmp[j]))
                        {
                            resultList.Add(tmp[j]);
                        }
                    }
                }
            }
        }
        return resultList;
    }

   /// <summary>
   /// 生成组件列表
   /// </summary>
   /// <param name="uiList"></param>
   /// <returns></returns>
    public static List<UICompleteEnum> UIComponentList(List<GameObject> uiList)
    {
        List<UICompleteEnum> list = new List<UICompleteEnum>();
        List<UIBehaviour> bList = SelectAllUIComponent(uiList);
        for (int i = 0; i < bList.Count; i++)
        {
            list.Add(GetUIComponent(bList[i]));
        }
        return list;
    } 

    /// <summary>
    /// 获取对应的组件
    /// </summary>
    /// <param name="rect"></param>
    /// <returns></returns>
    public static UICompleteEnum GetUIComponent(UIBehaviour rect)
    {
        #region
        if (rect is RawImage)
        {
            return UICompleteEnum.RawImage;
        }
        else if (rect is Button)
        {
            return UICompleteEnum.Button;
        }
        else if (rect is Toggle)
        {
            return UICompleteEnum.Toggle;
        }
        else if (rect is Slider)
        {
            return UICompleteEnum.Slider;
        }
        else if (rect is Scrollbar)
        {
            return UICompleteEnum.Scrollbar;
        }
        else if (rect is InputField)
        {
            return UICompleteEnum.InputField;
        }
        else if (rect is ScrollRect)
        {
            return UICompleteEnum.ScrollRect;
        }
        else if (rect is Text)
        {
            return UICompleteEnum.Text;
        }
        else if (rect is Image)
        {
            return UICompleteEnum.Image;
        }
        else if (rect is Dropdown)
        {
            return UICompleteEnum.Dropdown;
        }
        else if(rect is Mask)
        {
            return UICompleteEnum.Mask;
        }
        return UICompleteEnum.Transform;
        #endregion
    }

    /// <summary>
    /// 获取一个game object的路径
    /// </summary>
    /// <param name="rootGo"></param>
    /// <returns></returns>
    public static Dictionary<Transform, string> GetChildrenPaths(GameObject rootGo)
    {
        Dictionary<Transform, string> pathDic = new Dictionary<Transform, string>();
        string path = string.Empty;
        Transform[] tfArray = rootGo.GetComponentsInChildren<Transform>(true);
        for (int i = 0; i < tfArray.Length; i++) 
        {
            Transform node = tfArray[i];

            string str = node.name;
            while (node.parent != null && node.gameObject != rootGo && node.parent.gameObject != rootGo)
            {
                str = string.Format("{0}/{1}", node.parent.name, str);
                node = node.parent;
            }
            path += string.Format("{0}\n", str);

            if (!pathDic.ContainsKey(tfArray[i]))
            {
                pathDic.Add(tfArray[i], str);
            }
        }
        return pathDic;
    }
    /// <summary>
    /// 获取所有的子物体
    /// </summary>
    /// <param name="go"></param>
    /// <returns></returns>
    public static GameObject[] GetAllUIChild(GameObject go)
    {
        List<GameObject> goList = new List<GameObject>();
        Transform[] tfArray = go.GetComponentsInChildren<Transform>(true);
        for (int i = 0; i < tfArray.Length; i++)
        {
            Transform node = tfArray[i];
            if (!goList.Contains(node.gameObject))
            {
                goList.Add(node.gameObject);
            }
        }
        return goList.ToArray();
    }
    /// <summary>
    /// 生成game object的名字列表
    /// </summary>
    /// <param name="uiList"></param>
    /// <returns></returns>
    public static List<string> UINameList(List<UIBehaviour> uiList)
    {
        List<string> nameList = new List<string>();
        int index = 0;
        for (int i = 0; i < uiList.Count; i++)
        {
            string name = uiList[i].name;
            name = name.Replace(" ", "_");
            name = name.Replace("(", "_");
            name = name.Replace(")", "_");
            if (nameList.Contains(name))
            {
                ++index;
                name = name + index;
                nameList.Add(name);
            }
            else
            {
                nameList.Add(name);
            }
        }
        return nameList;
    }

}

/*
author： Lai Zhang Yin，
description ： If you have any question or suggestion, please add QQ/Wechat : 782966734
*/
