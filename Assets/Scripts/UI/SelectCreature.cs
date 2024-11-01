using Creatures;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    /// <summary>
    /// Is attached to each selectable in goblin list, main purpose to load selected goblin's data to UI
    /// </summary>
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

        /// <summary>
        /// Fill UI panel with goblin information
        /// </summary>
        /// <param name="eventData"></param>
        public void OnSelect(BaseEventData eventData)
        {
            ((ISelectHandler)panel).OnSelect(eventData);
            controller.FillRightSide(goblin);
        }



    }
}

