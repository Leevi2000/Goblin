using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


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

        private void Update()
        {
            if (Input.GetKeyDown(Keybinds.OPEN_SELECTED_PANEL))
            {
                if (creatureSelectorCanvas.activeInHierarchy)
                {
                    creatureSelectorCanvas.SetActive(false);
                }
                else
                {
                    creatureSelectorCanvas.SetActive(true);
                    FillLeftSide(creatureList);
                }
            }

            if(selectedGoblin != null & creatureSelectorCanvas.activeSelf)
            {
                FillRightSide(selectedGoblin);
            }
        }

        /// <summary>
        /// Fills left side of the UI with selected creatures
        /// </summary>
        /// <param name="creatures"></param>
        void FillLeftSide(List<Creatures.Creature> creatures)
        {
            var containerParent = GameObject.Find("UI_Canvas").transform.Find("CreatureSelector").transform.Find("CreatureSelectorScrollView").transform.Find("Viewport").transform.Find("Content");

            GameObject.Find("UI_Canvas").transform.Find("CreaturesInList").GetComponent<TMPro.TextMeshProUGUI>().text = "Number of creatures in list: " + creatures.Count.ToString();

            // Destroy previous items/refresh
            foreach (Transform child in containerParent.transform)
            {
                Destroy(child.gameObject);
            }

            foreach (Creatures.Goblin creature in creatures)
            {
                var infoPanel = Instantiate(creatureSelectorPanelPrefab);
                infoPanel.transform.SetParent(containerParent);
                infoPanel.transform.localScale = Vector3.one;
                var x = infoPanel.GetComponentInChildren<TMPro.TMP_Text>();
                x.text = creature.Firstname + " " + creature.Lastname;

                infoPanel.GetComponent<SelectCreature>().Goblin = creature;
            }
        }

        public void FillRightSide(Creatures.Goblin goblin)
        {
            var containerParent = GameObject.Find("UI_Canvas").transform.Find("CreatureSelector").transform.Find("SelectedInfoPanel");

            // Fill General panel
            var generalPanel = containerParent.Find("GeneralPanel");
            var textField = generalPanel.Find("GeneralInfo").GetComponent<TMPro.TextMeshProUGUI>();
            var logic = goblin.gameObject.GetComponent<Logic>();
            // Fill General Panel text information
            textField.text =
                $"Name: {goblin.Firstname} {goblin.Lastname} \n" +
                $"\t {goblin.Gender}, {goblin.Age} \n \n" +
                $"Working: {goblin.working} \n \n" +
                $"Worktimer: {logic.workTimer} of {logic.workTreshold} \n" +
                $"Freetime: {logic.coolDownTimer} of {logic.workCooldown}";

            // Fill JobDropDown with correct options
            var dropDownElement = generalPanel.Find("JobDropDown").GetComponent<TMPro.TMP_Dropdown>();
            dropDownElement.ClearOptions();

            //Convert jobList array to list and add them as options
            List<string> jobList = new List<string>(Jobs.jobList);
            dropDownElement.AddOptions(jobList);

            dropDownElement.onValueChanged.RemoveAllListeners();
            dropDownElement.value = goblin.job._workId;

           
            dropDownElement.onValueChanged.AddListener(delegate { DropdownValueChanged(dropDownElement, goblin); });
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

    }
}

