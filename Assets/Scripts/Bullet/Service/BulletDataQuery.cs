using QFramework;

namespace Bullet
{
    /// <summary>
    /// 查询子弹数据
    /// </summary>
    public class BulletDataQuery : AbstractQuery<BulletData>
    {
        public string bulletName;

        /// <summary>
        /// 查询子弹数据构造方法
        /// </summary>
        /// <param name="name">根据子弹名称搜索，子弹名称应该从常量中获取</param>
        public BulletDataQuery(string name)
        {
            bulletName = name;
        }

        protected override BulletData OnDo()
        {
            return this.GetModel<BulletModel>().data.Find(o => o.name == bulletName).Clone().As<BulletData>();
        }
    }
}