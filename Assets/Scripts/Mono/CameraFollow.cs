using System;
using UnityEngine;

namespace DefaultNamespace.Mono
{
    public class CameraFollow : MonoBehaviour
    {
        private Transform playerTransform;

        private void Awake()
        {
            playerTransform = PlayerObj.Instance?.transform;
        }

        private void Update()
        {
            if (playerTransform == null)
            {
                playerTransform = PlayerObj.Instance?.transform;
                return;
            }

            // 获取主摄像机的正交尺寸
            float halfHeight = Camera.main.orthographicSize;
            float halfWidth = halfHeight * Camera.main.aspect;

            // 屏幕中心60%的边界范围
            float centerXRange = halfWidth * 0.6f;
            float centerYRange = halfHeight * 0.6f;

            // 玩家相对于摄像机的位置
            Vector3 playerPos = playerTransform.position;
            Vector3 cameraPos = transform.position;

            // 判断玩家是否超出中心60%的区域
            bool shouldFollowX = playerPos.x < cameraPos.x - centerXRange || playerPos.x > cameraPos.x + centerXRange;
            bool shouldFollowY = playerPos.y < cameraPos.y - centerYRange || playerPos.y > cameraPos.y + centerYRange;

            // 如果玩家在X或Y方向超出了中心区域，则进行平滑跟随
            if (shouldFollowX || shouldFollowY)
            {
                var targetPos = playerPos;
                targetPos.z = cameraPos.z;
                cameraPos = Vector3.Lerp(cameraPos, targetPos, Time.deltaTime);
                transform.position = cameraPos;
            }
        }

    }
}