using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DKCommon.UI
{
    public class TabGroup : MonoBehaviour
    {
		public TabButton CurrentlySelectedTab { get; protected set; }
		public Color selectedColor;
		public Color unselectedColor;
		public int firstSelectedIndex;
        protected TabButton[] tabButtons;

        protected void Start()
        {
            tabButtons = GetComponentsInChildren<TabButton>();
			if(tabButtons.Length > firstSelectedIndex)
            {
				OnTabSelected(tabButtons[firstSelectedIndex]);
            }
        }

		public virtual void OnTabSelected(TabButton tabButton)
		{
			// 탭 이미지 세팅
			// 페이지 세팅
			for (int i = 0; i < tabButtons.Length; i++)
			{
				SetPageActive(tabButtons[i], false);
				tabButtons[i].Image.color = unselectedColor;
			}
			CurrentlySelectedTab = tabButton;
			SetPageActive(tabButton, true);
			if (tabButton)
			{
				tabButton.Image.color = selectedColor;
			}
		}

		protected void SetPageActive(TabButton tabButton, bool active)
		{
			if (tabButton && tabButton.page)
			{
				tabButton.page.SetActive(active);
			}
		}

	}
}
