
	function TestPanel:InitUI()
		self.Text_txt = self.gameObject.transform:Find("Text"):GetComponent("Text");
		self.Image11_img = self.gameObject.transform:Find("Image11"):GetComponent("Image");
		self.RawImage12_rImg = self.gameObject.transform:Find("RawImage12"):GetComponent("RawImage");
		self.midbtn_img = self.gameObject.transform:Find("midbtn"):GetComponent("Image");
		self.midbtn1_btn = self.gameObject.transform:Find("midbtn"):GetComponent("Button");
		self.Text2_txt = self.gameObject.transform:Find("midbtn/Text"):GetComponent("Text");
		self.Toggle_tgl = self.gameObject.transform:Find("Toggle"):GetComponent("Toggle");
		self.Background_img = self.gameObject.transform:Find("Toggle/Background"):GetComponent("Image");
		self.Checkmark_img = self.gameObject.transform:Find("Toggle/Background/Checkmark"):GetComponent("Image");
		self.Label_txt = self.gameObject.transform:Find("Toggle/Label"):GetComponent("Text");
		self.Slider_slider = self.gameObject.transform:Find("Slider"):GetComponent("Slider");
		self.Background3_img = self.gameObject.transform:Find("Slider/Background"):GetComponent("Image");
		self.Fill_img = self.gameObject.transform:Find("Slider/Fill Area/Fill"):GetComponent("Image");
		self.Handle_img = self.gameObject.transform:Find("Slider/Handle Slide Area/Handle"):GetComponent("Image");
		self.Scrollbar_img = self.gameObject.transform:Find("Scrollbar"):GetComponent("Image");
		self.Scrollbar4_scrlbar = self.gameObject.transform:Find("Scrollbar"):GetComponent("Scrollbar");
		self.Handle5_img = self.gameObject.transform:Find("Scrollbar/Sliding Area/Handle"):GetComponent("Image");
		self.InputField_img = self.gameObject.transform:Find("InputField"):GetComponent("Image");
		self.InputField6_iptField = self.gameObject.transform:Find("InputField"):GetComponent("InputField");
		self.Placeholder_txt = self.gameObject.transform:Find("InputField/Placeholder"):GetComponent("Text");
		self.Text7_txt = self.gameObject.transform:Find("InputField/Text"):GetComponent("Text");
		self.Scroll_View_scrollRect = self.gameObject.transform:Find("Scroll View"):GetComponent("ScrollRect");
		self.Scroll_View8_img = self.gameObject.transform:Find("Scroll View"):GetComponent("Image");
		self.Viewport_mask = self.gameObject.transform:Find("Scroll View/Viewport"):GetComponent("Mask");
		self.Viewport9_img = self.gameObject.transform:Find("Scroll View/Viewport"):GetComponent("Image");
		self.Scrollbar_Horizontal_img = self.gameObject.transform:Find("Scroll View/Scrollbar Horizontal"):GetComponent("Image");
		self.Scrollbar_Horizontal10_scrlbar = self.gameObject.transform:Find("Scroll View/Scrollbar Horizontal"):GetComponent("Scrollbar");
		self.Handle11_img = self.gameObject.transform:Find("Scroll View/Scrollbar Horizontal/Sliding Area/Handle"):GetComponent("Image");
		self.Scrollbar_Vertical_img = self.gameObject.transform:Find("Scroll View/Scrollbar Vertical"):GetComponent("Image");
		self.Scrollbar_Vertical12_scrlbar = self.gameObject.transform:Find("Scroll View/Scrollbar Vertical"):GetComponent("Scrollbar");
		self.Handle13_img = self.gameObject.transform:Find("Scroll View/Scrollbar Vertical/Sliding Area/Handle"):GetComponent("Image");
		self.Dropdown_img = self.gameObject.transform:Find("Dropdown"):GetComponent("Image");
		self.Dropdown14_dropdown = self.gameObject.transform:Find("Dropdown"):GetComponent("Dropdown");
		self.Label15_txt = self.gameObject.transform:Find("Dropdown/Label"):GetComponent("Text");
		self.Arrow_img = self.gameObject.transform:Find("Dropdown/Arrow"):GetComponent("Image");
		self.Template_img = self.gameObject.transform:Find("Dropdown/Template"):GetComponent("Image");
		self.Template16_scrollRect = self.gameObject.transform:Find("Dropdown/Template"):GetComponent("ScrollRect");
		self.Viewport17_mask = self.gameObject.transform:Find("Dropdown/Template/Viewport"):GetComponent("Mask");
		self.Viewport18_img = self.gameObject.transform:Find("Dropdown/Template/Viewport"):GetComponent("Image");
		self.Item_tgl = self.gameObject.transform:Find("Dropdown/Template/Viewport/Content/Item"):GetComponent("Toggle");
		self.Item_Background_img = self.gameObject.transform:Find("Dropdown/Template/Viewport/Content/Item/Item Background"):GetComponent("Image");
		self.Item_Checkmark_img = self.gameObject.transform:Find("Dropdown/Template/Viewport/Content/Item/Item Checkmark"):GetComponent("Image");
		self.Item_Label_txt = self.gameObject.transform:Find("Dropdown/Template/Viewport/Content/Item/Item Label"):GetComponent("Text");
		self.Scrollbar19_img = self.gameObject.transform:Find("Dropdown/Template/Scrollbar"):GetComponent("Image");
		self.Scrollbar20_scrlbar = self.gameObject.transform:Find("Dropdown/Template/Scrollbar"):GetComponent("Scrollbar");
		self.Handle21_img = self.gameObject.transform:Find("Dropdown/Template/Scrollbar/Sliding Area/Handle"):GetComponent("Image");

		self:AddEvent();
	end

	function TestPanel:AddEvent() 

		self.midbtn1_btn.onClick:AddListener(function() self:Onmidbtn1_btnClicked(); end)
		self.Toggle_tgl.onValueChanged:AddListener(function(args) self:OnToggle_tglValueChanged(args); end)
		self.Slider_slider.onValueChanged:AddListener(function(args) self:OnSlider_sliderValueChanged(args); end)
		self.Scrollbar4_scrlbar.onValueChanged:AddListener(function(args) self:OnScrollbar4_scrlbarValueChanged(args); end)
		self.InputField6_iptField.onValueChanged:AddListener(function(args) self:OnInputField6_iptFieldValueChanged(args); end)
		self.Scrollbar_Horizontal10_scrlbar.onValueChanged:AddListener(function(args) self:OnScrollbar_Horizontal10_scrlbarValueChanged(args); end)
		self.Scrollbar_Vertical12_scrlbar.onValueChanged:AddListener(function(args) self:OnScrollbar_Vertical12_scrlbarValueChanged(args); end)
		self.Item_tgl.onValueChanged:AddListener(function(args) self:OnItem_tglValueChanged(args); end)
		self.Scrollbar20_scrlbar.onValueChanged:AddListener(function(args) self:OnScrollbar20_scrlbarValueChanged(args); end)
	end

	function TestPanel:Onmidbtn1_btnClicked()


	end

	function TestPanel:OnToggle_tglValueChanged(arg)


	end

	function TestPanel:OnSlider_sliderValueChanged(arg)


	end

	function TestPanel:OnScrollbar4_scrlbarValueChanged(arg)


	end

	function TestPanel:OnInputField6_iptFieldValueChanged(arg)


	end

	function TestPanel:OnScroll_View_scrollRectValueChanged(arg)


	end

	function TestPanel:OnScrollbar_Horizontal10_scrlbarValueChanged(arg)


	end

	function TestPanel:OnScrollbar_Vertical12_scrlbarValueChanged(arg)


	end

	function TestPanel:OnDropdown14_dropdownValueChanged(arg)


	end

	function TestPanel:OnTemplate16_scrollRectValueChanged(arg)


	end

	function TestPanel:OnItem_tglValueChanged(arg)


	end

	function TestPanel:OnScrollbar20_scrlbarValueChanged(arg)


	end

}