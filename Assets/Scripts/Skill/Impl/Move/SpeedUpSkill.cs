using Actor;
using Buff;
using Buff.Command;
using DefaultNamespace;
using DG.Tweening;
using QFramework;
using UnityEngine;

namespace Model.Skill.Impl.Move
{
    public class SpeedUpSkill : AbstractSkill
    {


        public override void UseSkill(ActorObj userObj)
        {
            var buffData = this.GetModel<BuffModel>().SelectBuffData(BuffAssets.技能临时加速);
            this.SendCommand<AddBuffCommand<BaseBuffObj>>(new AddBuffCommand<BaseBuffObj>(userObj, buffData));

            //创建拖影
            var sprites = userObj.GetComponentsInChildren<SpriteRenderer>();

            ActionKit.Repeat(5)
                .Callback(() => { CreateShadow(sprites); })
                .Delay(0.02f)
                .Start(userObj);



        }

        private static void CreateShadow(SpriteRenderer[] sprites)
        {
            foreach (var sr in sprites)
            {
                if (ReferenceEquals(sr, null)) continue;

                //创建残影对象
                // 创建残影对象
                GameObject afterImage = new GameObject("AfterImage");
                SpriteRenderer imageSr = afterImage.AddComponent<SpriteRenderer>();

                // 复制 Sprite、颜色、排序层等属性
                imageSr.sprite = sr.sprite;
                imageSr.sortingLayerID = sr.sortingLayerID;
                imageSr.sortingOrder = sr.sortingOrder - 1; // 稍微降低层级避免遮挡
                imageSr.color = new Color(1, 1, 1, 0.5f); // 半透明效果

                // 设置位置与旋转
                afterImage.transform.position = sr.transform.position;
                afterImage.transform.rotation = sr.transform.rotation;
                afterImage.transform.localScale = sr.transform.lossyScale;

                // 渐隐消失
                imageSr.DOFade(0f, 0.5f).OnComplete(() => Object.Destroy(afterImage, 0.5f));
            }
        }
    }
}