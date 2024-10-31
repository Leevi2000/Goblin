using Creatures;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class SelectCreature : MonoBehaviour, ISelectHandler
    {
        Creatures.Goblin goblin;
        Selectable panel;
        UI_Controller controller;

        public Goblin Goblin { get => goblin; set => goblin = value; }

        private void Awake()
        {
            panel = gameObject.GetComponent<Selectable>();
            controller = GameObject.Find("GameManager").GetComponent<UI_Controller>();
        }

        public void OnSelect(BaseEventData eventData)
        {
            ((ISelectHandler)panel).OnSelect(eventData);
            controller.selectedGoblin = goblin;
        }



    }
}

