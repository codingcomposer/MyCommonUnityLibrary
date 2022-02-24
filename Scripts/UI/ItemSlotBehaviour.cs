using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DKCommon.UI
{
    public class ItemSlotBehaviour : BaseSlot
    {
        public TMPro.TextMeshProUGUI nameText;
        public TMPro.TextMeshProUGUI quantityText;
        public Image itemImage;
        public string ItemType { get; private set; }
        public int ItemQuantity { get; private set; }
        public void Set(Item item)
        {
            Set(item.itemType, item.itemQuantity);
        }

        public void Set(Reward reward)
        {
            Set(reward.Type, (int)reward.Count);
        }

        private void Set(string itemType, int itemQuantity)
        {
            ItemType = itemType;
            ItemQuantity = itemQuantity;
            if (nameText)
            {
                nameText.text = ItemType;
            }
            if (quantityText)
            {
                quantityText.text = ItemQuantity.ToString();
            }
            itemImage.sprite = FindSprite(ItemType);
        }

        private Sprite FindSprite(string itemType)
        {
            // TODO : Implement a function to find the sprite.
            return null;
        }


    }
}
