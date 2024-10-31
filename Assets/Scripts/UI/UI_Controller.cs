using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

            textField.text =
                $"Name: {goblin.Firstname} {goblin.Lastname} \n" +
                $"\t {goblin.Gender}, {goblin.Age} \n \n" +
                $"Working: {goblin.working} \n \n";

        }

    }
}

