using System;
using Microsoft.Xna.Framework;

namespace BraketsEngine
{
    public class Camera
    {
        public Color BackgroundColor = Color.Cyan;
        public float Zoom { get; private set; }

        public float ZoomMax = 10, ZoomMin = 0.1f;

        public Vector2 TargetPosition { get; private set; }
        public Matrix TranslationMatrix { get; private set; }

        internal float viewportScale = 1f;

        // Camera shake variables
        private float shakeIntensity;
        private float shakeDuration;
        private float shakeTimer;

        public Camera(Vector2 startPos)
        {
            this.TargetPosition = startPos;
            Globals.Camera = this;

            shakeIntensity = 0f;
            shakeDuration = 0f;
            shakeTimer = 0f;

            Zoom = 1f;
        }

        public void CalculateMatrix()
        {
            Zoom = viewportScale;

            var screenCenter = new Vector2(Globals.APP_Width / 2, Globals.APP_Height / 2);

            var offset = TargetPosition - screenCenter / Zoom;

            var dx = -offset.X;
            var dy = -offset.Y;

            if (shakeTimer > 0)
            {
                float shakeX = (float)(new Random().NextDouble() * 2 - 1) * shakeIntensity;
                float shakeY = (float)(new Random().NextDouble() * 2 - 1) * shakeIntensity;

                dx += shakeX;
                dy += shakeY;

                shakeTimer -= Globals.DEBUG_DT;
                if (shakeTimer <= 0)
                {
                    shakeTimer = 0;
                    shakeIntensity = 0f;
                }
            }

            TranslationMatrix = Matrix.CreateTranslation(dx, dy, 0f) * Matrix.CreateScale(Zoom, Zoom, 1f);
        }



        public void Follow(Sprite target, float smoothStep)
        {
            TargetPosition = Vector2.Lerp(Globals.Camera.TargetPosition, target.Position, smoothStep * Globals.DEBUG_DT);
        }

        public void GoTo(Vector2 target, float smoothStep)
        {
            TargetPosition = Vector2.Lerp(Globals.Camera.TargetPosition, target, smoothStep * Globals.DEBUG_DT);
        }

        public void Teleport(Vector2 target)
        {
            TargetPosition = target;
        }

        public void Center()
        {
            TargetPosition = new Vector2(Globals.APP_Width / 2, Globals.APP_Height / 2);
        }

        public void Shake(float intensity, float duration)
        {
            shakeIntensity = intensity;
            shakeDuration = duration;
            shakeTimer = duration;
        }

        public void SetZoom(float newZoom)
        {
            Zoom = MathHelper.Clamp(newZoom, ZoomMin, ZoomMax);
        }

        public void ZoomIn(float zoomAmount)
        {
            SetZoom(Zoom + zoomAmount);
        }

        public void ZoomOut(float zoomAmount)
        {
            SetZoom(Zoom - zoomAmount);
        }
    }
}
