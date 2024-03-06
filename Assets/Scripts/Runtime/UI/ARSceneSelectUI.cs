﻿using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace UnityEngine.XR.ARFoundation.Samples
{
    /// <summary>
    /// Controls the Samples Menu UI and scene loading functionality.
    /// </summary>
    /// <remarks>
    /// This class assumes that you only use it in a single Menu scene in the project, that this on the Main Menu
    /// GameObject, that all menu GameObjects are siblings of the Main Menu, and that all menu GameObjects have unique
    /// names.
    /// </remarks>
    public class ARSceneSelectUI : MonoBehaviour
    {
        const string k_DefaultTitleLabel = "AR Foundation Samples";
        static readonly Color s_DisabledButtonColor = new(228f / 255, 228f / 255, 228f / 255, .5f);

        static GameObject s_SelectedMenu;

        /// <summary>
        /// We statically cache information about the selected menu so that we can recover the menu state after the
        /// scene is unloaded and reloaded again.
        /// </summary>
        static MenuInfo s_SelectedMenuInfo;

        [SerializeField]
        Scrollbar m_HorizontalScrollBar;

        [SerializeField]
        Scrollbar m_VerticalScrollBar;

        [SerializeField]
        TextMeshProUGUI m_TitleLabel;

        [SerializeField]
        GameObject m_BackButton;

        void Awake()
        {
            if (s_SelectedMenuInfo == null)
            {
                s_SelectedMenu = gameObject;
                s_SelectedMenuInfo = new MenuInfo(name, null);
            }
            else
            {
                // If we have a selected menu on awake, this means the scene was unloaded and reloaded.
                // In this case our selected menu GameObject will be null, but we can find it again by name.
                s_SelectedMenu = transform.parent.Find(s_SelectedMenuInfo.gameObjectName).gameObject;
                if (s_SelectedMenuInfo.menuName != null)
                    SetTitleLabelMenuName(s_SelectedMenuInfo.menuName);

                // Hide the main menu in this case
                gameObject.SetActive(false);
            }

            SelectMenu(s_SelectedMenu.gameObject);
        }

        void OnDestroy()
        {
            s_SelectedMenu = null;
        }

        public void SelectMenu(GameObject menu)
        {
            if (menu == null)
            {
                Debug.LogWarning(
                    $"{nameof(ARSceneSelectUI)}.{nameof(SelectMenu)}: {nameof(menu)} was null and could not be selected.");

                return;
            }

            s_SelectedMenu.SetActive(false);
            s_SelectedMenu = menu;
            s_SelectedMenu.SetActive(true);
            m_BackButton.SetActive(s_SelectedMenu.gameObject != gameObject);

            s_SelectedMenuInfo.gameObjectName = menu.name;

            ScrollToStartPosition();
        }

        public void ResetTitleLabel()
        {
            m_TitleLabel.text = k_DefaultTitleLabel;
            s_SelectedMenuInfo.menuName = null;
        }

        public void SetTitleLabelMenuName(string menuName)
        {
            m_TitleLabel.text = $"Samples / {menuName}";
            s_SelectedMenuInfo.menuName = menuName;
        }

        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }

        public static void DisableButton(Button button)
        {
            if (button == null)
                return;

            button.interactable = false;

            var text = button.GetComponentInChildren<TextMeshProUGUI>();
            if (text == null)
                return;

            text.color = s_DisabledButtonColor;
        }

        void ScrollToStartPosition()
        {
            m_HorizontalScrollBar.value = 0;
            m_VerticalScrollBar.value = 1;
        }

        class MenuInfo
        {
            public string gameObjectName { get; set; }
            public string menuName { get; set; }

            public MenuInfo(string gameObjectName, string menuName)
            {
                this.gameObjectName = gameObjectName;
                this.menuName = menuName;
            }
        }
    }
}
