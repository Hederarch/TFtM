﻿using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace mapEditor.animations
{
    public class Attachment : Selectable
    {
        public Text text;
        public string Name { get => text.text; set => text.text = value; }
        private bool selected;
        public int index;

        private void Update()
        {
            if (Input.GetMouseButton(0) && selected)
            {
                transform.position = Input.mousePosition;
            }
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            selected = true;
            transform.SetParent(FindObjectOfType<Canvas>().transform);
            base.OnPointerDown(eventData);
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            selected = false;
            AnimationElement animationElement = FindClosestAnimationElement();
            index = animationElement.index;
            transform.SetParent(animationElement.attachmentHolderTransform, false);
            base.OnPointerUp(eventData);
        }

        private AnimationElement FindClosestAnimationElement()
        {
            float minDistance = float.MaxValue;
            AnimationElement minElement = null;
            foreach (AnimationElement animationElement in ImplementEditor.AnimationElements)
            {
                float distance = Vector3.Distance(transform.position, animationElement.attachmentHolderTransform.position);
                Debug.DrawLine(transform.position, animationElement.attachmentHolderTransform.position);
                if (minDistance > distance)
                {
                    minDistance = distance;
                    minElement = animationElement;
                }
            }
            return minElement;
        }
    }
}
