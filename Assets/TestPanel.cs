using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 
using UnityEngine.UI; 

public class TestPanel : MonoBehaviour
{
	private Text Text_txt; 
	private Image Image11_img; 
	private RawImage RawImage12_rImg; 
	private Image midbtn_img; 
	private Button midbtn1_btn; 
	private Text Text2_txt; 
	private Toggle Toggle_tgl; 
	private Image Background_img; 
	private Image Checkmark_img; 
	private Text Label_txt; 
	private Slider Slider_slider; 
	private Image Background3_img; 
	private Image Fill_img; 
	private Image Handle_img; 
	private Image Scrollbar_img; 
	private Scrollbar Scrollbar4_scrlbar; 
	private Image Handle5_img; 
	private Image InputField_img; 
	private InputField InputField6_iptField; 
	private Text Placeholder_txt; 
	private Text Text7_txt; 
	private ScrollRect Scroll_View_scrollRect; 
	private Image Scroll_View8_img; 
	private Mask Viewport_mask; 
	private Image Viewport9_img; 
	private Image Scrollbar_Horizontal_img; 
	private Scrollbar Scrollbar_Horizontal10_scrlbar; 
	private Image Handle11_img; 
	private Image Scrollbar_Vertical_img; 
	private Scrollbar Scrollbar_Vertical12_scrlbar; 
	private Image Handle13_img; 
	private Image Dropdown_img; 
	private Dropdown Dropdown14_dropdown; 
	private Text Label15_txt; 
	private Image Arrow_img; 
	private Image Template_img; 
	private ScrollRect Template16_scrollRect; 
	private Mask Viewport17_mask; 
	private Image Viewport18_img; 
	private Toggle Item_tgl; 
	private Image Item_Background_img; 
	private Image Item_Checkmark_img; 
	private Text Item_Label_txt; 
	private Image Scrollbar19_img; 
	private Scrollbar Scrollbar20_scrlbar; 
	private Image Handle21_img; 

	private void Start()
	{ 
		this.Text_txt = this.transform.Find("Text").GetComponent<Text>();
		this.Image11_img = this.transform.Find("Image11").GetComponent<Image>();
		this.RawImage12_rImg = this.transform.Find("RawImage12").GetComponent<RawImage>();
		this.midbtn_img = this.transform.Find("midbtn").GetComponent<Image>();
		this.midbtn1_btn = this.transform.Find("midbtn").GetComponent<Button>();
		this.Text2_txt = this.transform.Find("midbtn/Text").GetComponent<Text>();
		this.Toggle_tgl = this.transform.Find("Toggle").GetComponent<Toggle>();
		this.Background_img = this.transform.Find("Toggle/Background").GetComponent<Image>();
		this.Checkmark_img = this.transform.Find("Toggle/Background/Checkmark").GetComponent<Image>();
		this.Label_txt = this.transform.Find("Toggle/Label").GetComponent<Text>();
		this.Slider_slider = this.transform.Find("Slider").GetComponent<Slider>();
		this.Background3_img = this.transform.Find("Slider/Background").GetComponent<Image>();
		this.Fill_img = this.transform.Find("Slider/Fill Area/Fill").GetComponent<Image>();
		this.Handle_img = this.transform.Find("Slider/Handle Slide Area/Handle").GetComponent<Image>();
		this.Scrollbar_img = this.transform.Find("Scrollbar").GetComponent<Image>();
		this.Scrollbar4_scrlbar = this.transform.Find("Scrollbar").GetComponent<Scrollbar>();
		this.Handle5_img = this.transform.Find("Scrollbar/Sliding Area/Handle").GetComponent<Image>();
		this.InputField_img = this.transform.Find("InputField").GetComponent<Image>();
		this.InputField6_iptField = this.transform.Find("InputField").GetComponent<InputField>();
		this.Placeholder_txt = this.transform.Find("InputField/Placeholder").GetComponent<Text>();
		this.Text7_txt = this.transform.Find("InputField/Text").GetComponent<Text>();
		this.Scroll_View_scrollRect = this.transform.Find("Scroll View").GetComponent<ScrollRect>();
		this.Scroll_View8_img = this.transform.Find("Scroll View").GetComponent<Image>();
		this.Viewport_mask = this.transform.Find("Scroll View/Viewport").GetComponent<Mask>();
		this.Viewport9_img = this.transform.Find("Scroll View/Viewport").GetComponent<Image>();
		this.Scrollbar_Horizontal_img = this.transform.Find("Scroll View/Scrollbar Horizontal").GetComponent<Image>();
		this.Scrollbar_Horizontal10_scrlbar = this.transform.Find("Scroll View/Scrollbar Horizontal").GetComponent<Scrollbar>();
		this.Handle11_img = this.transform.Find("Scroll View/Scrollbar Horizontal/Sliding Area/Handle").GetComponent<Image>();
		this.Scrollbar_Vertical_img = this.transform.Find("Scroll View/Scrollbar Vertical").GetComponent<Image>();
		this.Scrollbar_Vertical12_scrlbar = this.transform.Find("Scroll View/Scrollbar Vertical").GetComponent<Scrollbar>();
		this.Handle13_img = this.transform.Find("Scroll View/Scrollbar Vertical/Sliding Area/Handle").GetComponent<Image>();
		this.Dropdown_img = this.transform.Find("Dropdown").GetComponent<Image>();
		this.Dropdown14_dropdown = this.transform.Find("Dropdown").GetComponent<Dropdown>();
		this.Label15_txt = this.transform.Find("Dropdown/Label").GetComponent<Text>();
		this.Arrow_img = this.transform.Find("Dropdown/Arrow").GetComponent<Image>();
		this.Template_img = this.transform.Find("Dropdown/Template").GetComponent<Image>();
		this.Template16_scrollRect = this.transform.Find("Dropdown/Template").GetComponent<ScrollRect>();
		this.Viewport17_mask = this.transform.Find("Dropdown/Template/Viewport").GetComponent<Mask>();
		this.Viewport18_img = this.transform.Find("Dropdown/Template/Viewport").GetComponent<Image>();
		this.Item_tgl = this.transform.Find("Dropdown/Template/Viewport/Content/Item").GetComponent<Toggle>();
		this.Item_Background_img = this.transform.Find("Dropdown/Template/Viewport/Content/Item/Item Background").GetComponent<Image>();
		this.Item_Checkmark_img = this.transform.Find("Dropdown/Template/Viewport/Content/Item/Item Checkmark").GetComponent<Image>();
		this.Item_Label_txt = this.transform.Find("Dropdown/Template/Viewport/Content/Item/Item Label").GetComponent<Text>();
		this.Scrollbar19_img = this.transform.Find("Dropdown/Template/Scrollbar").GetComponent<Image>();
		this.Scrollbar20_scrlbar = this.transform.Find("Dropdown/Template/Scrollbar").GetComponent<Scrollbar>();
		this.Handle21_img = this.transform.Find("Dropdown/Template/Scrollbar/Sliding Area/Handle").GetComponent<Image>();

		this.AddEventListener();
	}
	private void Update()
	{

	}
	private void AddEventListener()
	{
		this.midbtn1_btn.onClick.AddListener(this.Onmidbtn1_btnClicked);
		this.Toggle_tgl.onValueChanged.AddListener(this.OnToggle_tglValueChanged);
		this.Slider_slider.onValueChanged.AddListener(this.OnSlider_sliderValueChanged);
		this.Scrollbar4_scrlbar.onValueChanged.AddListener(this.OnScrollbar4_scrlbarValueChanged);
		this.InputField6_iptField.onValueChanged.AddListener(this.OnInputField6_iptFieldValueChanged);
		this.Scrollbar_Horizontal10_scrlbar.onValueChanged.AddListener(this.OnScrollbar_Horizontal10_scrlbarValueChanged);
		this.Scrollbar_Vertical12_scrlbar.onValueChanged.AddListener(this.OnScrollbar_Vertical12_scrlbarValueChanged);
		this.Item_tgl.onValueChanged.AddListener(this.OnItem_tglValueChanged);
		this.Scrollbar20_scrlbar.onValueChanged.AddListener(this.OnScrollbar20_scrlbarValueChanged);

	}

	private void Onmidbtn1_btnClicked()
	{

	}

	private void OnToggle_tglValueChanged(bool arg)
	{

	}

	private void OnSlider_sliderValueChanged(float arg)
	{

	}

	private void OnScrollbar4_scrlbarValueChanged(float arg)
	{

	}

	private void OnInputField6_iptFieldValueChanged(string arg)
	{

	}

	private void OnScroll_View_scrollRectValueChanged(Vector2 arg)
	{

	}

	private void OnScrollbar_Horizontal10_scrlbarValueChanged(float arg)
	{

	}

	private void OnScrollbar_Vertical12_scrlbarValueChanged(float arg)
	{

	}

	private void OnDropdown14_dropdownValueChanged(int arg)
	{

	}

	private void OnTemplate16_scrollRectValueChanged(Vector2 arg)
	{

	}

	private void OnItem_tglValueChanged(bool arg)
	{

	}

	private void OnScrollbar20_scrlbarValueChanged(float arg)
	{

	}

}