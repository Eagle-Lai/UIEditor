/*
author： Lai Zhang Yin，
description ： If you have any question or suggestion, please add QQ/Wechat : 782966734
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEditor.IMGUI.Controls;
using UnityEngine.EventSystems;
using System.Text;
using System.IO;

public class UIEditorPanel : EditorWindow
{
    /// <summary>
    /// true is C# Code, false is Lua Code
    /// </summary>
    private bool isCSCode = true;

    /// <summary>
    /// 类的名字
    /// </summary>
    private string className = string.Empty;
    /// <summary>
    /// 选择的GameObject的列表
    /// </summary>
    private List<GameObject> uiCompleteList;
    /// <summary>
    /// 当前选择的GameObject的对象
    /// </summary>
    private RectTransform selectedObj;

    private Vector2 _scrollPos;

    private Vector2 scrollTextPos;
    /// <summary>
    /// 列表树
    /// </summary>
    private TreeViewState treeViewState;
    private PrefabsTreeViews treeViews;

    /// <summary>
    /// 所有的脚本
    /// </summary>
    private StringBuilder allCodeBuilder;

    /// <summary>
    /// UI的类型列表
    /// </summary>
    private List<string> uiNameList;
    /// <summary>
    /// 字段的名字列表
    /// </summary>
    private List<string> variableNameList;


    private void OnEnable()
    {
        this.init();
    }

    private void OnFocus()
    {
        this.init();
        //  this.DrawTitle();
    }

    private void OnLostFocus()
    {

    }

    private void OnDisable()
    {

    }

    private void OnDestroy()
    {
        Utils.SelectedDic = null;
        this.uiCompleteList = null;
        this.uiNameList = null;
        this.variableNameList = null;
        this.className = string.Empty;
        this.allCodeBuilder = null;

        this.allCodeBuilder = null;
        this.isCSCode = true;
    }

    private UIEditorPanel()
    {
        this.titleContent = new GUIContent("UI Editor Panel");
    }

    [MenuItem("UIEditor/Open UIEditor")]
    private static void showWindow()
    {
        EditorWindow.GetWindow(typeof(UIEditorPanel));
    }

    public void init()
    {
        if (treeViewState == null)
        {
            treeViewState = new TreeViewState();
        }
        //  treeViews = null;
        //this.selectedObj = null;
    }

    private void OnGUI()
    {

        // using (EditorGUILayout.VerticalScope vScope = new EditorGUILayout.VerticalScope(GUILayout.Width(EditorGUIUtility.currentViewWidth * 0.5f)))
        {
            GUI.backgroundColor = Color.white;
            // Rect rect = vScope.rect;
            // GUI.Box(rect, "");

            GUI.backgroundColor = Color.white;
            this.DrawTitle();
            this.DrawMainContent();
        }
        //右半部分
        // using (new EditorGUILayout.VerticalScope(GUILayout.Width(EditorGUIUtility.currentViewWidth * 0.5f)))
        {
            GUI.backgroundColor = Color.white;


        }
    }
    /// <summary>
    /// 绘制标题和按钮
    /// </summary>
    private void DrawTitle()
    {
        #region 标题内容
        GUILayout.BeginVertical();
        GUILayout.Space(10);
        GUI.skin.label.fontSize = 15;
        GUI.skin.label.alignment = TextAnchor.UpperLeft;
        //不再重新赋值是为了防止误点，所以，如果想要选择别的game object只能重新打开
        if (selectedObj == null && Selection.activeGameObject != null)
        {
            selectedObj = this.GetSelectionObj();
        }

        if (selectedObj != null)
        {
            GUILayout.Label("Selected Prefab:" + selectedObj.name);
        }
        else
        {
            GUILayout.Label("please selected UI prefab in hierarchy");
        }
        GUILayout.BeginHorizontal();
        GUILayout.Space(300);
        if (GUILayout.Button("预览C#代码", GUILayout.Width(100), GUILayout.Height(20)))
        {
            this.isCSCode = true;
            this.GenerateCode();
        }
        if (GUILayout.Button("预览Lua代码", GUILayout.Width(100), GUILayout.Height(20)))
        {
            this.isCSCode = false;
            this.GenerateCode();
        }
        if (GUILayout.Button("生成脚本文件", GUILayout.Width(100), GUILayout.Height(20)))
        {
            if (this.isCSCode)
            {
                this.CreateCsUIScript(".cs", "cs");
            }
            else
            {
                this.CreateCsUIScript(".lua", "lua");
            }
        }
        if (GUILayout.Button("复制代码", GUILayout.Width(100), GUILayout.Height(20)))
        {
            TextEditor p = new TextEditor();
            if (this.allCodeBuilder == null || this.allCodeBuilder.Length <= 0)
            {
                EditorUtility.DisplayDialog("提示", "请先生成C#代码或者Lua代码", "OK");
                return;
            }
            if (this.allCodeBuilder != null && this.allCodeBuilder.Length > 0)
            {
                p.text = this.allCodeBuilder.ToString();
                p.OnFocus();
                p.Copy();
            }
            EditorUtility.DisplayDialog("提示", "代码复制成功", "OK");
        }
        GUILayout.EndHorizontal();
        GUILayout.EndVertical();
        #endregion
    }
    /// <summary>
    /// 绘制左边的GameObjec列表
    /// </summary>
    private void DrawMainContent()
    {
        //EditorGUILayout.BeginHorizontal();
        _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);
        EditorGUILayout.BeginVertical();
        EditorGUILayout.Space();
        this.DrawUIEles();
        EditorGUILayout.EndScrollView();

        GUILayout.Space(100);
        GUI.skin.scrollView.alignment = TextAnchor.UpperLeft;
        scrollTextPos = EditorGUILayout.BeginScrollView(scrollTextPos, GUILayout.Height(550));
        this.DrawCodeView();
        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();
        GUILayout.Space(100);
        //EditorGUILayout.EndHorizontal();

    }
    /// <summary>
    /// 绘制代码预览
    /// </summary>
    private void DrawCodeView()
    {

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.Space();
        GUILayout.Space(350);
        //GUILayout.Label("代码预览：\n" + this.GenerateCSCode());

        if (this.allCodeBuilder != null && this.allCodeBuilder.Length > 0)
        {
            GUILayout.Label("代码预览：\n" + this.GenerateCode());
        }
        EditorGUILayout.EndHorizontal();
    }
    /// <summary>
    /// 获取被选中的物体
    /// </summary>
    /// <returns></returns>
    private RectTransform GetSelectionObj()
    {
        GameObject go = Selection.activeGameObject;
        RectTransform objTransorm = null;
        if (go.GetComponent<RectTransform>() != null)
        {
            objTransorm = go.GetComponent<RectTransform>();
        }
        //判断是否是画布,暂时这么粗略的判断吧
        if (objTransorm != null && objTransorm.GetComponent<GraphicRaycaster>() &&
            objTransorm.GetComponent<CanvasScaler>() != null &&
            objTransorm.GetComponent<Canvas>() != null)
        {
            return null;
        }
        else
        {
            if (objTransorm != null && objTransorm.parent != null)
            {
                if (objTransorm.parent.GetComponent<GraphicRaycaster>() != null &&
                objTransorm.parent.GetComponent<CanvasScaler>() != null &&
                objTransorm.parent.GetComponent<Canvas>() != null)
                {
                    if (string.IsNullOrEmpty(this.className))
                    {
                        this.className = objTransorm.name;
                    }
                    return objTransorm;
                }
            }
        }
        return null;
    }
    /// <summary>
    /// 绘制预设包含的UI组件
    /// </summary>
    private void DrawUIEles()
    {
        if (this.selectedObj != null)
        {
            GameObject[] rects = Utils.GetAllUIChild(this.selectedObj.gameObject);
            if (treeViews == null)
            {
                treeViews = new PrefabsTreeViews(treeViewState);
                if (this.uiCompleteList == null)
                {
                    this.uiCompleteList = new List<GameObject>();
                }
            }
            this.treeViews.ParentTransform = this.selectedObj;
            this.treeViews.Reload();
            this.treeViews.OnGUI(new Rect(0, 50, 300, 500));
            this.UpdateUIDict(rects);
        }
    }
    /// <summary>
    /// 实时更新选择的列表
    /// </summary>
    /// <param name="uIBehaviours"></param>
    private void UpdateUIDict(GameObject[] uIBehaviours)
    {
        for (int i = 0; i < uIBehaviours.Length; i++)
        {
            GameObject tempBehaviour = uIBehaviours[i];
            if (!this.uiCompleteList.Contains(tempBehaviour))
            {
                this.uiCompleteList.Add(tempBehaviour);
            }
        }
    }
    /// <summary>
    /// 获取所有选择的game object
    /// </summary>
    /// <returns></returns>
    private List<GameObject> BuildSelectUIList()
    {
        List<GameObject> uiList = new List<GameObject>();
        if (this.uiNameList == null)
        {
            this.uiNameList = new List<string>();
        }
        this.uiNameList.Clear();

        // Debug.Log(this.uiCompleteList.Count);
        // Debug.Log(Utils.SelectedDic.Count);
        if (this.uiCompleteList.Count == Utils.SelectedDic.Count)
        {
            for (int i = 0; i < Utils.SelectedDic.Count; i++)
            {
                if (Utils.SelectedDic[i] && this.uiCompleteList[i] != null)
                {
                    uiList.Add(this.uiCompleteList[i]);
                    this.uiNameList.Add(this.uiCompleteList[i].name);
                }
            }
        }
        else
        {
            this.allCodeBuilder = null;
            Debug.LogError("data error  selectedList count and selectedDic count is different, please reopen");
        }
        return uiList;
    }
    /// <summary>
    /// 写C#的变量命名
    /// </summary>
    /// <param name="list"></param>
    private StringBuilder WriteVariableCode(List<UICompleteEnum> list)
    {
        StringBuilder variableBuilder = new StringBuilder();
        if (list != null)
        {
            if (this.variableNameList == null)
            {
                this.variableNameList = new List<string>();
            }
            this.variableNameList.Clear();
            this.variableNameList = Utils.UINameList(Utils.SelectAllUIComponent(this.BuildSelectUIList()));
            for (int i = 0; i < list.Count; i++)
            {

                string name = this.variableNameList[i] + "_" + UICodeConfig.uiVariableName[(int)list[i]];
                this.variableNameList[i] = name;
                variableBuilder.AppendFormat(UICodeConfig.variable, UICodeConfig.uiComponentName[(int)list[i]], name);
            }
        }
        return variableBuilder;
    }
    /// <summary>
    /// 写变量的查找赋值
    /// </summary>
    private StringBuilder WriteVariableValueCode()
    {
        Dictionary<Transform, string> dic = Utils.GetChildrenPaths(this.selectedObj.gameObject);

        List<UICompleteEnum> listUI = Utils.UIComponentList(this.BuildSelectUIList());

        StringBuilder pathBuilder = new StringBuilder();
        List<UIBehaviour> list = Utils.SelectAllUIComponent(this.BuildSelectUIList());
        string temp = string.Empty;
        if (this.isCSCode)
        {
            pathBuilder.Append(UICodeConfig.startName);
            temp = UICodeConfig.uiPath;
        }
        else
        {
            pathBuilder.AppendFormat(UICodeConfig.luaClassName, this.className);
            temp = UICodeConfig.luaVariable;
        }
        for (int i = 0; i < list.Count; i++)
        {
            string name = this.variableNameList[i];
            pathBuilder.AppendFormat(temp, name, dic[list[i].transform], UICodeConfig.uiComponentName[(int)listUI[i]]);

        }
        if (this.isCSCode)
        {
            pathBuilder.Append(UICodeConfig.addEventMethod);
            pathBuilder.Append(UICodeConfig.end);
        }
        else
        {
            pathBuilder.Append(UICodeConfig.luaAddEventMethod);
            pathBuilder.Append(UICodeConfig.luaEnd);
        }
        
        if (this.isCSCode)
        {
            pathBuilder.Append(UICodeConfig.updateName);
        }
        
        return pathBuilder;
    }
    /// <summary>
    /// 写事件的注册
    /// </summary>
    private StringBuilder WriteAddMethodListenerCode()
    {

        StringBuilder methodListenerdBuilder = new StringBuilder();
        List<UICompleteEnum> listUI = Utils.UIComponentList(this.BuildSelectUIList());
        string temp1 = string.Empty;
        string temp2 = string.Empty;
        if (this.isCSCode)
        {
            methodListenerdBuilder.Append(UICodeConfig.addEventName);
            temp1 = UICodeConfig.btnListenerName;
            temp2 = UICodeConfig.valueChangedName;
        }
        else
        {
            methodListenerdBuilder.AppendFormat(UICodeConfig.luaAddEvent, this.className);
            temp1 = UICodeConfig.luaEventAdd;
            temp2 = UICodeConfig.luaEventChanged;
        }
        for (int i = 0; i < listUI.Count; i++)
        {
            string name = this.variableNameList[i];
            switch (listUI[i])
            {
                case UICompleteEnum.Button:
                    methodListenerdBuilder.AppendFormat(temp1, name, name);
                    break;
                case UICompleteEnum.Toggle:
                case UICompleteEnum.Slider:
                case UICompleteEnum.Scrollbar:
                case UICompleteEnum.InputField:
                    methodListenerdBuilder.AppendFormat(temp2, name, name);
                    break;
                default:
                    break;
            }
        }
        if (this.isCSCode)
        {
            methodListenerdBuilder.Append(UICodeConfig.methodEnd);
        }
        else
        {
            methodListenerdBuilder.Append(UICodeConfig.luaEnd);
        }
        return methodListenerdBuilder;
    }

    /// <summary>
    /// 写点击的方法
    /// </summary>
    private StringBuilder WriteCSMethodCode()
    {
        List<UICompleteEnum> listUI = Utils.UIComponentList(this.BuildSelectUIList());
        StringBuilder methodBuilder = new StringBuilder();
        for (int i = 0; i < listUI.Count; i++)
        {
            string name = this.variableNameList[i];
            switch (listUI[i])
            {
                case UICompleteEnum.Button:
                    methodBuilder.AppendFormat(UICodeConfig.onBtnClickName, name);
                    break;
                case UICompleteEnum.Toggle:
                    methodBuilder.AppendFormat(UICodeConfig.onValueChangedName, name, "bool");
                    break;
                case UICompleteEnum.Slider:
                case UICompleteEnum.Scrollbar:
                    methodBuilder.AppendFormat(UICodeConfig.onValueChangedName, name, "float");
                    break;
                case UICompleteEnum.InputField:
                    methodBuilder.AppendFormat(UICodeConfig.onValueChangedName, name, "string");
                    break;
                case UICompleteEnum.ScrollRect:
                    methodBuilder.AppendFormat(UICodeConfig.onValueChangedName, name, "Vector2");
                    break;
                case UICompleteEnum.Dropdown:
                    methodBuilder.AppendFormat(UICodeConfig.onValueChangedName, name, "int");
                    break;
                default:
                    break;
            }
        }
        methodBuilder.Append(UICodeConfig.classEnd);
        return methodBuilder;
    }
    /// <summary>
    /// 写Lua的点击方法
    /// </summary>
    /// <returns></returns>
    private StringBuilder WriteLuaMethod()
    {
        List<UICompleteEnum> listUI = Utils.UIComponentList(this.BuildSelectUIList());
        StringBuilder methodBuilder = new StringBuilder();
        for (int i = 0; i < listUI.Count; i++)
        {
            string name = this.variableNameList[i];
            switch (listUI[i])
            {
                case UICompleteEnum.Button:
                    methodBuilder.AppendFormat(UICodeConfig.luaClickEvent, this.className, name);
                    break;
                case UICompleteEnum.Toggle:
                case UICompleteEnum.Slider:
                case UICompleteEnum.Scrollbar:
                case UICompleteEnum.InputField:
                case UICompleteEnum.ScrollRect:
                case UICompleteEnum.Dropdown:
                    methodBuilder.AppendFormat(UICodeConfig.luaValueChangedEvent, this.className, name);
                    break;
                default:
                    break;
            }
        }
        methodBuilder.Append(UICodeConfig.classEnd);
        return methodBuilder;
    }
    /// <summary>
    /// 写命名空间
    /// </summary>
    /// <returns></returns>
    private StringBuilder WriteCSUsing()
    {

        StringBuilder usingBuilder = new StringBuilder();
        if (this.isCSCode)
        {
            usingBuilder.Append(UICodeConfig.namespaceStr);
            usingBuilder.AppendFormat(UICodeConfig.className, this.className);
        }
        return usingBuilder;
    }
    /// <summary>
    /// 生成预览的代码
    /// </summary>
    /// <returns></returns>
    private StringBuilder GenerateCode()
    {
        if (this.allCodeBuilder == null)
        {
            this.allCodeBuilder = new StringBuilder();
        }
        this.allCodeBuilder.Remove(0, this.allCodeBuilder.Length);
        List<UICompleteEnum> list = Utils.UIComponentList(this.BuildSelectUIList());

        StringBuilder s0 = this.WriteCSUsing();
        StringBuilder s1 = this.WriteVariableCode(list);
        StringBuilder s2 = this.WriteVariableValueCode();
        StringBuilder s3 = this.WriteAddMethodListenerCode();
        StringBuilder s4 = this.WriteCSMethodCode();
        if (this.isCSCode)
        {
            this.allCodeBuilder.Append(s0);
            this.allCodeBuilder.Append(s1);
        }
        this.allCodeBuilder.Append(s2);
        this.allCodeBuilder.Append(s3);
        if (!this.isCSCode)
        {
            s4 = this.WriteLuaMethod();
        }
        this.allCodeBuilder.Append(s4); 
        //Debug.Log(temp);
        return this.allCodeBuilder;
    }
    /// <summary>
    /// 生成脚本文件
    /// </summary>
    /// <param name="type1">文件后缀</param>
    /// <param name="type2">文件类型</param>
    private void CreateCsUIScript(string type1, string type2)
    {
        if (this.allCodeBuilder == null || this.allCodeBuilder.Length <= 0)
        {
            EditorUtility.DisplayDialog("提示", "请先生成C#代码或者Lua代码", "OK");
            return;
        }
        string path = EditorPrefs.GetString("create_script_folder", "");
        path = EditorUtility.SaveFilePanel("Create Script", path, this.className + type1, type2);
        if (string.IsNullOrEmpty(path)) return;

        int index = path.LastIndexOf('/');
        className = path.Substring(index + 1, path.LastIndexOf('.') - index - 1);

        File.WriteAllText(path, this.allCodeBuilder.ToString(), new UTF8Encoding(false));
        AssetDatabase.Refresh();

        Debug.Log("脚本生成成功,生成路径为:" + path);
        EditorPrefs.SetString("create_script_folder", path);
    }
}

/*
author： Lai Zhang Yin，
description ： If you have any question or suggestion, please add QQ/Wechat : 782966734
*/
