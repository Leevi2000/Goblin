using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Paths
{
    public class UI_ElementPathsLeft
    {
        public static Transform LEFT_SIDE_CONTENT_ELEMENT = GameObject.Find("UI_Canvas").transform.Find("CreatureSelector").transform.Find("CreatureSelectorScrollView").transform.Find("Viewport").transform.Find("Content");
        public static TMPro.TextMeshProUGUI LEFT_SIDE_LENGTH_TXT_ELEMENT = GameObject.Find("UI_Canvas").transform.Find("CreaturesInList").GetComponent<TMPro.TextMeshProUGUI>();

       
    }

    public class UI_ElementPathsRight
    {
        public static Transform RIGHT_SIDE_CONTENT_ELEMENT = GameObject.Find("UI_Canvas").transform.Find("CreatureSelector").transform.Find("SelectedInfoPanel");
        public static Transform RIGHT_SIDE_GENERAL_PANEL = RIGHT_SIDE_CONTENT_ELEMENT.transform.Find("GeneralPanel");
        public static TMPro.TextMeshProUGUI RIGHT_SIDE_GENERAL_TXT_ELEMENT = RIGHT_SIDE_GENERAL_PANEL.Find("GeneralInfo").GetComponent<TMPro.TextMeshProUGUI>();
        public static TMPro.TMP_Dropdown RIGHT_SIDE_GENERAL_DROPDOWN = RIGHT_SIDE_GENERAL_PANEL.Find("JobDropDown").GetComponent<TMPro.TMP_Dropdown>();

    }
}

