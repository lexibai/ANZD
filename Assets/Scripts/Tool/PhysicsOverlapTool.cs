using UnityEngine;
using System.Collections.Generic;

namespace Tool
{
    public static class PhysicsTool
    {
        /// <summary>
        /// 在指定位置进行扇形范围内的物理检测
        /// </summary>
        /// <param name="origin">检测位置</param>
        /// <param name="direction">扇形方向</param>
        /// <param name="angle">扇形角度</param>
        /// <param name="radius">扇形半径</param>
        /// <param name="layerMask">检测层</param>
        /// <returns>返回检测到的碰撞体</returns>
        public static Collider2D[] OverlapSector(
            Vector3 origin,
            Vector3 direction,
            float angle,
            float radius,
            int layerMask = 0)
        {
            Collider2D[] results = Physics2D.OverlapCircleAll(origin, radius, layerMask);
            List<Collider2D> sectorResults = new List<Collider2D>();

            #if UNITY_EDITOR
            Vector3 leftDir = Quaternion.Euler(0, 0, -angle * 0.5f) * direction;
            Vector3 rightDir = Quaternion.Euler(0, 0, angle * 0.5f) * direction;
            
            // 绘制扇形的左右边界
            Debug.DrawRay(origin, leftDir.normalized * radius, Color.yellow, .2f);
            Debug.DrawRay(origin, rightDir.normalized * radius, Color.yellow, .2f);
            
            // 绘制扇形顶端的弧线
            int segments = Mathf.CeilToInt(angle / 10f); // 每5度一个线段
            for (int i = 0; i <= segments; i++)
            {
                float t = i / (float)segments;
                float currentAngle = Mathf.Lerp(-angle * 0.5f, angle * 0.5f, t);
                Vector3 arcDir = Quaternion.Euler(0, 0, currentAngle) * direction;
                Debug.DrawRay(origin, arcDir.normalized * radius, Color.red,.2f);
            }
            #endif
            
            foreach (var col in results)
            {
                Vector3 dirToTarget = (col.transform.position - origin).normalized;
                float currentAngle = Vector3.Angle(direction, dirToTarget);

                
                if (currentAngle < angle * 0.5f)
                {
                    sectorResults.Add(col);
                }
            }

            return sectorResults.ToArray();
        }

        /// <summary>
        /// 带泛型返回的扇形检测，可直接获取特定组件（例如 ActorObj）
        /// </summary>
        /// <typeparam name="T">组件类型</typeparam>
        /// <param name="origin">检测位置</param>
        /// <param name="direction">扇形方向</param>
        /// <param name="angle">扇形角度</param>
        /// <param name="radius">扇形半径</param>
        /// <param name="layerMask">检测层</param>
        /// <returns>返回特定组件的列表</returns>
        public static List<T> OverlapSectorComponent<T>(
            Vector3 origin,
            Vector3 direction,
            float angle,
            float radius,
            int layerMask = 0) where T : Component
        {
            Collider2D[] cols = PhysicsTool.OverlapSector(origin, direction, angle, radius, layerMask);
            List<T> components = new List<T>();
            
            foreach (var col in cols)
            {
                if (col.TryGetComponent(out T component))
                {
                    components.Add(component);
                }
            }

            return components;
        }
    }
}