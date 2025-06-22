using System.ComponentModel;
using UnityEngine;

namespace Buff.BuffList
{
    [DisplayName("标记效果")]
    public class MarkBuff : BaseBuffObj
    {
        public Color oldColor = default;
        public bool isChange = false;

        protected override void Additional()
        {
            base.Additional();
            SpriteRenderer spriteRenderer = ao.GetComponentInChildren<SpriteRenderer>();
            if (!isChange)
            {
                isChange = true;
                oldColor = spriteRenderer.color;
            }
            int v = 10 - nowCount;
            Color color = new Color(v * 0.1f, v * 0.1f, v * 0.1f);
            spriteRenderer.color = color;
        }

        protected override void RemoveBuff()
        {
            base.RemoveBuff();
            SpriteRenderer spriteRenderer = ao.GetComponentInChildren<SpriteRenderer>();
            spriteRenderer.color = oldColor;
        }
    }
}