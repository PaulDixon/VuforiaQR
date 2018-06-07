using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ssrp_attribute_view : MonoBehaviour {

    public Text ui_name;
    public Text ui_type;
    public Text ui_value;
  

    private SSRP_attribute _model = new SSRP_attribute();

    // Use this for initialization
    void Start() {
        SSRP_Metadata[] list = new SSRP_Metadata[] {new SSRP_Metadata("base_name","base_type","base_value"),
        new SSRP_Metadata("met00_name","met00_type","met00_value"),
        new SSRP_Metadata("met01_name","met01_type","met01_value"),
        new SSRP_Metadata("met02_name","met02_type","met02_value")};
      importData(new SSRP_attribute( list));
    }

    void importData(SSRP_attribute data)
    {
        _model = data;
        refresh();
    }

    void refresh()
    {
        if (_model != null)
        {
            if (ui_name != null && ui_type != null && ui_value != null)
            {
                ui_name.text = _model.name;
                ui_value.text = _model.value;
                ui_type.text = _model.type;
            }
            else
            {
                ui_name.text = "UI_404";

            }


        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}

