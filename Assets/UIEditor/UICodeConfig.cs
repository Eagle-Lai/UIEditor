/*
author： Lai Zhang Yin，
description ： If you have any question or suggestion, please add QQ/Wechat : 782966734
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class UICodeConfig : MonoBehaviour
{
    /// <summary>
    /// 组件的列表
    /// </summary>
    public static string[] uiComponentName = new string[] { "Transform", "Text", "Image", "RawImage", "Button", "Toggle", "Slider", "Scrollbar", "InputField", "ScrollRect", "Dropdown", "Mask" };
    /// <summary>
    /// 组件的名字后缀
    /// </summary> 
    public static string[] uiVariableName = new string[] { "trans", "txt", "img", "rImg", "btn", "tgl", "slider", "scrlbar", "iptField", "scrollRect", "dropdown", "mask" };


    #region C#代码配置
    public const string variable = "\tprivate {0} {1}; \n";

    public const string namespaceStr = "using System.Collections; \nusing System.Collections.Generic; \nusing UnityEngine; \nusing UnityEngine.UI; \n";

    public const string className = "\npublic class {0} : MonoBehaviour\n{{\n";

    public const string startName = "\n\tprivate void Start()\n\t{ \n";

    public const string updateName = "\n\tprivate void Update()\n\t{\n\n\t}\n"; 

    public const string onBtnClickName = "\n\tprivate void On{0}Clicked()\n\t{{\n\n\t}}\n";

    public const string onValueChangedName = "\n\tprivate void On{0}ValueChanged({1} arg)\n\t{{\n\n\t}}\n";

    public const string methodEnd = "\n\t}\n";

    public const string btnListenerName = "\t\tthis.{0}.onClick.AddListener(this.On{1}Clicked);\n";

    public const string valueChangedName = "\t\tthis.{0}.onValueChanged.AddListener(this.On{1}ValueChanged);\n";

    public const string addEventName = "\tprivate void AddEventListener()\n\t{\n";

    public const string end = "\n\t}";

    public const string classEnd = "\n}";

    public const string uiPath = "\t\tthis.{0} = this.transform.Find(\"{1}\").GetComponent<{2}>();\n";

    public const string addEventMethod = "\n\t\tthis.AddEventListener();";
    #endregion
     
    #region Lua代码配置
    public const string luaModule = "module(\"{0}\", package.seeall) \n\n;";

    public const string luaClassName = "\n\tfunction {0}:InitUI()";

    public const string luaAddEvent = "\n\tfunction {0}:AddEvent() \n";

    public const string luaClickEvent = "\n\tfunction {0}:On{1}Clicked()\n\n\n\tend\n";

    public const string luaValueChangedEvent = "\n\tfunction {0}:On{1}ValueChanged(arg)\n\n\n\tend\n";

    public const string luaVariable = "\n\t\tself.{0} = self.gameObject.transform:Find(\"{1}\"):GetComponent(\"{2}\");";

    public const string luaEventAdd = "\n\t\tself.{0}.onClick:AddListener(function() self:On{1}Clicked(); end)";

    public const string luaEventChanged = "\n\t\tself.{0}.onValueChanged:AddListener(function(args) self:On{1}ValueChanged(args); end)";

    public const string luaEnd = "\n\tend\n";

    public const string luaAddEventMethod = "\n\n\t\tself:AddEvent();";
    #endregion
}

/*
author： Lai Zhang Yin，
description ： If you have any question or suggestion, please add QQ/Wechat : 782966734
*/
