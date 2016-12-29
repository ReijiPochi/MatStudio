using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MatFramework.Graphics;
using MatFramework.Graphics._3D.Objects;
using MatFramework.Graphics._3D.Effects;
using System.Windows.Input;
using MatFramework.Graphics._3D;
using SlimDX;

namespace NITNIC.Worlds
{
    public class TestWorld : MatWorld
    {
        public TestWorld()
        {
            Objects.Add(test1);
            Objects.Add(map);
            Objects.Add(mapArea);
            sky.SetEffectPacks(new MatTextureOnlyFx(sky));
            Objects.Add(sky);

            cam1 = new MatCamera()
            {
                Eye = new Vector3(0.0f, 1.6f, 10.0f),
                Target = new Vector3(0.0f, 0.0f, 0.0f),
                Up = new Vector3(0.0f, 1.0f, 0.0f),
                FieldOfView = 80.0
            };

            ActiveCamera = cam1;
        }

        MatCamera cam1;
        MatSolid3DObject test1 = new MatSolid3DObject(@"objects\TEST.obj", @"objects\TEST.png") { Color = new Color4(0.0f, 0.03f, 0.1f), PositionX = 10.0 };
        MatSolid3DObject map = new MatSolid3DObject(@"objects\Map.obj", @"objects\Map.png") { Color = new Color4(0.0f, 0.03f, 0.1f) };
        MatSolid3DObject mapArea = new MatSolid3DObject(@"objects\MapArea.obj", @"objects\Map.png") { Visible = false };
        MatSolid3DObject sky = new MatSolid3DObject(@"objects\sky.obj", @"objects\Skydome.png");

        GameControlInputData inputData;

        double actualSpeedEW, actualSpeedSN, actualSpeedSG;
        double targetHeight = 1.6;
        PositionXYZ eye = new PositionXYZ(0.0, 0.1, 10.0);
        double fov = 70.0, actualFov = 150.0;

        public override void OnControlDataInput(object data)
        {
            inputData = data as GameControlInputData;
        }

        public override void OnRender(double viewWidth, double viewHeight)
        {
            if (inputData != null)
            {
                if (inputData.aimMode)
                    fov = 30.0;
                else
                    fov = 70.0;

                if (inputData.speedEW > actualSpeedEW) actualSpeedEW += 0.004;
                else if (inputData.speedEW < actualSpeedEW) actualSpeedEW -= 0.004;

                if (inputData.speedSN > actualSpeedSN) actualSpeedSN += 0.004;
                else if (inputData.speedSN < actualSpeedSN) actualSpeedSN -= 0.004;

                if (fov > actualFov) actualFov += 5.0;
                else if (fov < actualFov) actualFov -= 5.0;


                if (Math.Abs(actualSpeedEW) < 0.004) actualSpeedEW = 0.0;
                if (Math.Abs(actualSpeedSN) < 0.004) actualSpeedSN = 0.0;
                if (Math.Abs(actualSpeedSG) < 0.01) actualSpeedSG = 0.0;

                double trgX, trgY, trgZ;
                double preEyeX = eye.X, preEyeZ = eye.Z;

                eye.X += -Math.Sin(inputData.angleEW) * actualSpeedSN + Math.Cos(inputData.angleEW) * actualSpeedEW;
                eye.Z += Math.Cos(inputData.angleEW) * actualSpeedSN + Math.Sin(inputData.angleEW) * actualSpeedEW;

                MapJudgment.Map = mapArea;
                MapJudgmentResult hitTest = MapJudgment.Judge(eye);
                if (!hitTest.mapOK)
                {
                    actualSpeedEW = -actualSpeedEW * 0.5;
                    actualSpeedSN = -actualSpeedSN * 0.5;
                    eye.X = preEyeX;
                    eye.Z = preEyeZ;
                }

                targetHeight = hitTest.floorHeight + 1.6 + inputData.speedSG;
                if (targetHeight > 5.0) targetHeight = 5.0;
                else if (targetHeight < 0.5) targetHeight = 0.5;
                double deltaEyeY = (targetHeight - eye.Y) * 0.12;
                if (Math.Abs(deltaEyeY) < 0.001) deltaEyeY = 0.0;
                eye.Y += deltaEyeY;

                trgX = -Math.Sin(inputData.angleEW) * Math.Cos(inputData.angleSN) + eye.X;
                trgZ = Math.Cos(inputData.angleEW) * Math.Cos(inputData.angleSN) + eye.Z;
                trgY = -Math.Sin(inputData.angleSN) + eye.Y;

                cam1.Eye = PositionXYZ.ConvertToSlimDXVector3(eye);
                cam1.Target = new Vector3((float)trgX, (float)trgY, (float)trgZ);
                cam1.FieldOfView = actualFov;
            }

            base.OnRender(viewWidth, viewHeight);
        }
    }
}
