using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UI.Paths;
using System;

namespace UI
{
    public class UI_Controller : MonoBehaviour
    {
        [SerializeField] GameObject creatureSelectorCanvas;
        List<Creatures.Creature> creatureList;
        [SerializeField] GameObject creatureSelectorPanelPrefab;

        public Creatures.Goblin selectedGoblin;

        private void Start()
        {
            creatureList = GameObject.Find("UserInput").GetComponentInChildren<MouseController>().creatureList;
        }

        // Main logic for filling UI panels
        private void Update()
        {

            // If pressed to open panel:
            if (Input.GetKeyDown(Keybinds.OPEN_SELECTED_PANEL))
            {
                // If disabling panel, deselect goblin and empty the right side from the information.
                if (creatureSelectorCanvas.activeInHierarchy)
                {
                    ClearRightSide();
                    creatureSelectorCanvas.SetActive(false);
                    selectedGoblin = null;
                }
                else
                {
                    creatureSelectorCanvas.SetActive(true);
                    if (creatureList.Count > 0)
                        FillLeftSide(creatureList);
                }
            }

            if(selectedGoblin != null & creatureSelectorCanvas.activeSelf)
            {
                UpdateRightSide();
            }
        }

        /// <summary>
        /// Fills left side of the UI with selected creatures
        /// </summary>
        /// <param name="creatures"></param>
        void FillLeftSide(List<Creatures.Creature> creatures)
        {
            var containerParent = UI_ElementPathsLeft.LEFT_SIDE_CONTENT_ELEMENT;

            UI_ElementPathsLeft.LEFT_SIDE_LENGTH_TXT_ELEMENT.text = "Number of creatures in list: " + creatures.Count.ToString();

            // Destroy previous items/refresh
            foreach (Transform child in containerParent.transform)
            {
                Destroy(child.gameObject);
            }

            // Create selectable element for each goblin on list
            foreach (Creatures.Goblin creature in creatures)
            {
                CreateSelectableElement(creature, containerParent);
            }

            // Automatically display information of the first goblin on a list
            if(creatures.Count > 0)
            {
                if(creatures[0] is Creatures.Goblin)
                {
                    selectedGoblin = creatures[0] as Creatures.Goblin;
                    FillRightSide(selectedGoblin);
                }
                
            }
        }

        void ClearRightSide()
        {
            // Get reference to text GameObject
            TMPro.TextMeshProUGUI textField = UI_ElementPathsRight.RIGHT_SIDE_GENERAL_TXT_ELEMENT;

            var text = "No Goblin Selected";
            FillText(textField, text);

            var dropDownElement = UI_ElementPathsRight.RIGHT_SIDE_GENERAL_DROPDOWN;
            //dropDownElement.ClearOptions();
            dropDownElement.onValueChanged.RemoveAllListeners();
        }

        /// <summary>
        /// Adds a selectable goblin to left side of the UI panel and attaches it to the parent gameobject
        /// </summary>
        void CreateSelectableElement(Creatures.Goblin creature, Transform parent)
        {
            var infoPanel = Instantiate(creatureSelectorPanelPrefab);
            infoPanel.transform.SetParent(parent);
            infoPanel.transform.localScale = Vector3.one;
            var x = infoPanel.GetComponentInChildren<TMPro.TMP_Text>();
            x.text = creature.Firstname + " " + creature.Lastname;

            infoPanel.GetComponent<SelectCreature>().Goblin = creature;
        }


        /// <summary>
        /// Fills right side panels with chosen goblin's information
        /// </summary>
        /// <param name="goblin"></param>
        public void FillRightSide(Creatures.Goblin goblin)
        {
            selectedGoblin = goblin;

            // Get reference to text GameObject
            var textField = UI_ElementPathsRight.RIGHT_SIDE_GENERAL_TXT_ELEMENT;

            var text = GenerateGeneralText(goblin);
            FillText(textField, text);

            // Fill JobDropDown with correct options
            var dropDownElement = UI_ElementPathsRight.RIGHT_SIDE_GENERAL_DROPDOWN;
            ConfigureJobDropdown(dropDownElement, goblin);

        }

        /// <summary>
        /// Configures dropdown options with goblin work name and other work possibilities.
        /// </summary>
        /// <param name="dropDownElement"></param>
        /// <param name="goblin"></param>
        void ConfigureJobDropdown(TMPro.TMP_Dropdown dropDownElement, Creatures.Goblin goblin)
        {
            dropDownElement.ClearOptions();

            //Convert jobList array to list and add them as options
            List<string> jobList = new List<string>(Jobs.jobList);
            dropDownElement.AddOptions(jobList);


            dropDownElement.onValueChanged.RemoveAllListeners();
            dropDownElement.value = goblin.job._workId;
            dropDownElement.onValueChanged.AddListener(delegate { DropdownValueChanged(dropDownElement, goblin); });
        }

        /// <summary>
        /// Updates the information about the currently selected goblin.
        /// </summary>
        public void UpdateRightSide()
        {
            if(selectedGoblin == null)
            {
                return;
            }

            // Get reference to text GameObject
            var textField = UI_ElementPathsRight.RIGHT_SIDE_GENERAL_TXT_ELEMENT;

            var text = GenerateGeneralText(selectedGoblin);
            FillText(textField, text);
        }

        string GenerateGeneralText(Creatures.Goblin goblin)
        {
            // Get the chosen goblin's logic script
            var logic = goblin.gameObject.GetComponent<Logic>();

            // Fill General Panel's text information
            string text =
                $"Name: {goblin.Firstname} {goblin.Lastname} \n" +
                $"\t {goblin.Gender}, {goblin.Age} \n \n" +
                $"Working: {goblin.working} \n \n" +
                $"Worktimer: {logic.workTimer} of {logic.workTreshold} \n" +
                $"Freetime: {logic.coolDownTimer} of {logic.workCooldown}";

            return text;
        }

        void DropdownValueChanged(TMPro.TMP_Dropdown change, Creatures.Goblin goblin)
        {
            // Offsetting by 3
            goblin.job._workId = change.value - 3;
        }
        
        /// <summary>
        /// Centers the camera to goblin and selects it in game 
        /// </summary>
        public void CenterToSelected()
        {
            if(selectedGoblin == null)
            {
                return;
            }

            var cam = GameObject.Find("Main Camera");
            var mouseController = GameObject.Find("UserInput").transform.Find("Cursor").GetComponent<MouseController>();
            mouseController.UnselectAll();
            mouseController.creatureList.Add(selectedGoblin);
            mouseController.SetOutline(selectedGoblin);
            cam.transform.transform.position = new Vector3(selectedGoblin.transform.position.x - 2, selectedGoblin.transform.position.y, cam.transform.position.z);
        }

        public void FillText(TMPro.TextMeshProUGUI element, string txt)
        {
            element.text = txt;
        }

    }
}

