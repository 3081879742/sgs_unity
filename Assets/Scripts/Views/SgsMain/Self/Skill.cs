using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class Skill : MonoBehaviour
    {
        public Button button;
        public Text text;
        public GameObject effect;

        public Model.Skill model { get; set; }
        public bool IsSelected { get; private set; }
        public OperationArea operationArea { get => GetComponentInParent<OperationArea>(); }
        public SkillArea skillArea { get => GetComponentInParent<SkillArea>(); }

        // Start is called before the first frame update
        void Start()
        {
            button.onClick.AddListener(ClickSkill);
        }

        /// <summary>
        /// 点击技能
        /// </summary>
        private void ClickSkill()
        {
            if (!IsSelected)
            {
                Select();
                operationArea.ChangeType(TimerType.CallSkill);
            }
            else
            {
                Unselect();
                operationArea.ChangeType(Model.TimerTask.Instance.timerType);
            }
        }

        /// <summary>
        /// 选中技能
        /// </summary>
        public void Select()
        {
            if (IsSelected) return;

            IsSelected = true;
            effect.SetActive(true);
            skillArea.SelectedSkill = this;
        }

        /// <summary>
        /// 取消选中
        /// </summary>
        public void Unselect()
        {
            if (!IsSelected) return;
            IsSelected = false;
            effect.SetActive(false);
            skillArea.SelectedSkill = null;
        }

        /// <summary>
        /// 重置技能
        /// </summary>
        public void ResetSkill()
        {
            button.interactable = false;
            Unselect();
        }
    }
}