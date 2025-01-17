﻿using Haiku.MonoGameUI.Layouts;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using RootNomics.Primitives;
using RootNomics.Environment;
using RootNomics.SimulationRender;
using RootNomics;
using Haiku.MonoGameUI;
using RootNomicsGame.Simulation;

namespace RootNomicsGame
{
    internal class Garden : DrawableGameComponent
    {
        // dev flags
        // --
        public readonly static bool SHOW_AXIS = false;
        public readonly static bool RESTRICT_CAMERA = false;
        private readonly UserInterface userInterface;

        // --

        private Color CLEAR_COLOR = Color.CornflowerBlue; // Using CornflowerBlue, Black, White
        private const int DEFAULT_VIEWPORT_WIDTH = 1400;
        private const int DEFAULT_VIEWPORT_HEIGHT = 800;
        private CameraTransforms cameraTransforms;

        private DrawLine drawLine;
        private DrawTriangle drawTriangle;
        private Model modelUnitSquare;
        private DrawCube drawCube;

        private Model spaceshipModel;
        private Model modelCubeWedge0;
        private Model modelCubeWedge1;

        private Model modelAcaciaTree1;
        private Model modelAcaciaTree2;
        private Model modelBirchTree1;
        private Model modelBirchTree2;
        private Model modelCactus1;
        private Model modelCactus2;
        private Model modelFern1;
        private Model modelFern2;
        private Model modelFlower1;
        private Model modelFlower2;
        private Model modelFlower3;
        private Model modelFlower4;
        private Model modelMushroom1;
        private Model modelMushroom2;
        private Model modelMushroom3;
        private Model modelMushroom4;
        private Model modelMushroom5;
        private Model modelMushroom6;
        private Model modelMushroom7;
        private Model modelPineTree1;
        private Model modelPineTree2;
        private Model modelPlant1;
        private Model modelReeds1;
        private Model modelSmallPlant1;
        private Model modelTombstone;
        private Model modelTerrain1;

        private GroundTiles groundTiles;
        private PlantModels fernModels;

        //private SimulationRenderer simulationRenderer = new SimulationRenderer(new List<string>
        //{
        //    "farmer",
        //    "worker",
        //    "blacksmith",
        //    "miner",
        //    "refiner",
        //    "woodcutter"
        //});

        private SimulationRenderer simulationRenderer = new SimulationRenderer();

        public Garden(Game game, UserInterface userInterface)
            :base(game)
        {
            int screenWidth = game.Window.ClientBounds.Width;
            int screenHeight = game.Window.ClientBounds.Height;
            cameraTransforms = new CameraTransforms(screenWidth, screenHeight);
            this.userInterface = userInterface;
        }

        public SimulationRenderer Reset()
        {
            simulationRenderer.Reset();
            return simulationRenderer;
        }

        protected override void LoadContent()
        {
            drawLine = new DrawLine(GraphicsDevice, cameraTransforms);
            drawTriangle = new DrawTriangle(GraphicsDevice, cameraTransforms);
            drawCube = new DrawCube(GraphicsDevice, cameraTransforms);

            modelUnitSquare = Game.Content.Load<Model>("unitsquare");
            modelCubeWedge0 = Game.Content.Load<Model>("cube-wedge0");
            modelCubeWedge1 = Game.Content.Load<Model>("cube-wedge1");
            modelAcaciaTree1 = Game.Content.Load<Model>("acaciaTree1");
            modelAcaciaTree2 = Game.Content.Load<Model>("acaciaTree2");
            modelBirchTree1 = Game.Content.Load<Model>("birchTree1");
            modelBirchTree2 = Game.Content.Load<Model>("birchTree2");
            modelCactus1 = Game.Content.Load<Model>("cactus1");
            modelCactus2 = Game.Content.Load<Model>("cactus2");
            modelFern1 = Game.Content.Load<Model>("fern1");
            modelFern2 = Game.Content.Load<Model>("fern2");
            modelFlower1 = Game.Content.Load<Model>("flower1");
            modelFlower2 = Game.Content.Load<Model>("flower2");
            modelFlower3 = Game.Content.Load<Model>("flower3");
            modelFlower4 = Game.Content.Load<Model>("flower4");
            modelMushroom1 = Game.Content.Load<Model>("mushroom1");
            modelMushroom2 = Game.Content.Load<Model>("mushroom2");
            modelMushroom3 = Game.Content.Load<Model>("mushroom3");
            modelMushroom4 = Game.Content.Load<Model>("mushroom4");
            modelMushroom5 = Game.Content.Load<Model>("mushroom5");
            modelMushroom6 = Game.Content.Load<Model>("mushroom6");
            modelMushroom7 = Game.Content.Load<Model>("mushroom7");
            modelPineTree1 = Game.Content.Load<Model>("pineTree1");
            modelPineTree2 = Game.Content.Load<Model>("pineTree2");
            modelSmallPlant1 = Game.Content.Load<Model>("smallPlant1");
            modelPlant1 = Game.Content.Load<Model>("plant1");
            modelReeds1 = Game.Content.Load<Model>("reeds1");
            modelTombstone = Game.Content.Load<Model>("tombstone");
            modelTerrain1 = Game.Content.Load<Model>("terrain1");

            groundTiles = new GroundTiles(modelCubeWedge0, modelCubeWedge1);
            simulationRenderer.SetGroundTiles(groundTiles);

            simulationRenderer.RegisterCameraTransforms(cameraTransforms);
            simulationRenderer.RegisterGameModel("acaciaTree1", modelAcaciaTree1);
            simulationRenderer.RegisterGameModel("acaciaTree2", modelAcaciaTree2);
            simulationRenderer.RegisterGameModel("birchTree1", modelBirchTree1);
            simulationRenderer.RegisterGameModel("birchTree2", modelBirchTree2);
            simulationRenderer.RegisterGameModel("cactus1", modelCactus1);
            simulationRenderer.RegisterGameModel("cactus2", modelCactus2);
            simulationRenderer.RegisterGameModel("fern1", modelFern1);
            simulationRenderer.RegisterGameModel("fern2", modelFern2);
            simulationRenderer.RegisterGameModel("flower1", modelFlower1);
            simulationRenderer.RegisterGameModel("flower2", modelFlower2);
            simulationRenderer.RegisterGameModel("flower3", modelFlower3);
            simulationRenderer.RegisterGameModel("flower4", modelFlower4);
            simulationRenderer.RegisterGameModel("mushroom1", modelMushroom1);
            simulationRenderer.RegisterGameModel("mushroom2", modelMushroom2);
            simulationRenderer.RegisterGameModel("mushroom3", modelMushroom3);
            simulationRenderer.RegisterGameModel("mushroom4", modelMushroom4);
            simulationRenderer.RegisterGameModel("mushroom5", modelMushroom5);
            simulationRenderer.RegisterGameModel("mushroom6", modelMushroom6);
            simulationRenderer.RegisterGameModel("pineTree1", modelPineTree1);
            simulationRenderer.RegisterGameModel("pineTree2", modelPineTree2);
            simulationRenderer.RegisterGameModel("smallPlant1", modelSmallPlant1);
            simulationRenderer.RegisterGameModel("plant1", modelPlant1);
            simulationRenderer.RegisterGameModel("reeds1", modelReeds1);
            simulationRenderer.RegisterGameModel("terrain1", modelTerrain1);
        }

        private int previousMouseScroll = 0;
        private int mouseDragX = 0;
        private int mouseDragY = 0;
        private bool leftMouseIsReleased = true;

        //protected override void Update(GameTime gameTime)
        public override void Update(GameTime gameTime)
        {
            cameraTransforms.UpdateViewportDimensions(Game.Window.ClientBounds.Width, Game.Window.ClientBounds.Height);

            // TESTING ONLY, print camera position
            if (Keyboard.GetState().IsKeyDown(Keys.P))
            {
                Debug.WriteLine($"camera position {cameraTransforms.cameraPosition}");
                Debug.WriteLine($"rotation {MathHelper.ToDegrees(cameraTransforms.cameraRotation)}");
                Debug.WriteLine($"distance from origin {Vector3.Distance(cameraTransforms.cameraPosition, new Vector3(0, 0, 0))}");
            }

            if (!userInterface.DidHandleMouseMove
                && !userInterface.DidHandleMouseButtons)
            {
                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                {
                    if (leftMouseIsReleased)
                    {
                        leftMouseIsReleased = false;
                        mouseDragX = Mouse.GetState().X;
                        mouseDragY = Mouse.GetState().Y;
                    }
                    else
                    {
                        float diffX = Mouse.GetState().X - mouseDragX;
                        float diffY = Mouse.GetState().Y - mouseDragY;
                        cameraTransforms.IncrementCameraOrbitDegrees(diffX / 4);
                        cameraTransforms.OrbitUpDown(diffY / 20);
                        mouseDragX = Mouse.GetState().X;
                        mouseDragY = Mouse.GetState().Y;
                    }
                }
                if (Mouse.GetState().LeftButton == ButtonState.Released)
                {
                    leftMouseIsReleased = true;
                }

                int currentMouseScroll = Mouse.GetState().ScrollWheelValue;
                if (previousMouseScroll > currentMouseScroll)
                {
                    cameraTransforms.ZoomOut();
                    previousMouseScroll = currentMouseScroll;
                }
                if (previousMouseScroll < currentMouseScroll)
                {
                    cameraTransforms.ZoomIn();
                    previousMouseScroll = currentMouseScroll;
                }
            }


            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(CLEAR_COLOR);
            simulationRenderer.DrawGroundTiles();
            simulationRenderer.DrawAgents(cameraTransforms);

            //simulationRenderer.GetGameModel("acaciaTree1").DrawModelWithDefaultValues(cameraTransforms, ModelTransforms.Translation(-5, 6, 0));
            //simulationRenderer.GetGameModel("acaciaTree2").DrawModelWithDefaultValues(cameraTransforms, ModelTransforms.Translation(-5, 8, 0));
            //simulationRenderer.GetGameModel("birchTree1").DrawModelWithDefaultValues(cameraTransforms, ModelTransforms.Translation(-8, -8, 0));
            //simulationRenderer.GetGameModel("birchTree2").DrawModelWithDefaultValues(cameraTransforms, ModelTransforms.Translation(-8, -6, 0));
            //simulationRenderer.GetGameModel("cactus1").DrawModelWithDefaultValues(cameraTransforms, ModelTransforms.Translation(-8, -4, 0));
            //simulationRenderer.GetGameModel("cactus2").DrawModelWithDefaultValues(cameraTransforms, ModelTransforms.Translation(-8, -2, 0));
            //simulationRenderer.GetGameModel("fern1").DrawModelWithDefaultValues(cameraTransforms, ModelTransforms.Translation(-8, 0, 0));
            //simulationRenderer.GetGameModel("fern2").DrawModelWithDefaultValues(cameraTransforms, ModelTransforms.Translation(-8, 2, 0));
            //simulationRenderer.GetGameModel("flower1").DrawModelWithDefaultValues(cameraTransforms, ModelTransforms.Translation(-8, 2, 0));
            //simulationRenderer.GetGameModel("flower2").DrawModelWithDefaultValues(cameraTransforms, ModelTransforms.Translation(-8, 2, 0));
            //simulationRenderer.GetGameModel("flower3").DrawModelWithDefaultValues(cameraTransforms, ModelTransforms.Translation(-8, 2, 0));
            //simulationRenderer.GetGameModel("flower4").DrawModelWithDefaultValues(cameraTransforms, ModelTransforms.Translation(-8, 2, 0));
            //simulationRenderer.GetGameModel("mushroom1").DrawModelWithDefaultValues(cameraTransforms, ModelTransforms.Translation(-8, 2, 0));
            //simulationRenderer.GetGameModel("mushroom2").DrawModelWithDefaultValues(cameraTransforms, ModelTransforms.Translation(-8, 4, 0));
            //simulationRenderer.GetGameModel("mushroom3").DrawModelWithDefaultValues(cameraTransforms, ModelTransforms.Translation(-8, 6, 0));
            //simulationRenderer.GetGameModel("mushroom4").DrawModelWithDefaultValues(cameraTransforms, ModelTransforms.Translation(-8, 8, 0));
            //simulationRenderer.GetGameModel("mushroom5").DrawModelWithDefaultValues(cameraTransforms, ModelTransforms.Translation(-8, 10, 0));
            //simulationRenderer.GetGameModel("mushroom6").DrawModelWithDefaultValues(cameraTransforms, ModelTransforms.Translation(-4, -8, 0));
            //simulationRenderer.GetGameModel("pineTree1").DrawModelWithDefaultValues(cameraTransforms, ModelTransforms.Translation(-4, -6, 0));
            //simulationRenderer.GetGameModel("pineTree2").DrawModelWithDefaultValues(cameraTransforms, ModelTransforms.Translation(-4, -4, 0));
            //simulationRenderer.GetGameModel("smallPlant1").DrawModelWithDefaultValues(cameraTransforms, ModelTransforms.Translation(-4, -2, 0));
            //simulationRenderer.GetGameModel("plant1").DrawModelWithDefaultValues(cameraTransforms, ModelTransforms.Translation(-4, 0, 0));
            //simulationRenderer.GetGameModel("reeds1").DrawModelWithDefaultValues(cameraTransforms, ModelTransforms.Translation(-4, 2, 0));
            if (SHOW_AXIS)
            {
                drawLine.DrawAxis(GraphicsDevice);
            }
            base.Draw(gameTime);
        }

        private void DrawModel(Model model, Matrix world, Matrix view, Matrix projection)
        {
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (Effect effect in mesh.Effects)
                {
                    BasicEffect basicEffect = (BasicEffect)effect;
                    basicEffect.World = world;
                    basicEffect.View = view;
                    basicEffect.Projection = projection;
                    Matrix.CreateTranslation(0, 0, 0, out Matrix translation);
                    basicEffect.World = Matrix.Multiply(translation, basicEffect.World);

                }
                mesh.Draw();
            }
        }


        private void DrawModelTranslationAndColor(Model model, Matrix world, Matrix view, Matrix projection,
            float tX, float tY, float tZ, Vector3 color, float scaleX = 1f, float scaleY = 1f, float scaleZ = 1f, float rot = 0f)
        {
            // int count = 0;
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (Effect effect in mesh.Effects)
                {
                    BasicEffect basicEffect = (BasicEffect)effect;
                    basicEffect.EnableDefaultLighting();
                    CommonBasicEffects.SetEffects(basicEffect);

                    //basicEffect.AmbientLightColor = new Vector3(0.7f, 0.5f, 0.4f);
                    //basicEffect.DirectionalLight0.Enabled = true;
                    //basicEffect.DirectionalLight0.Direction = new Vector3(1, 1, -1);


                    Matrix S = Matrix.CreateScale(scaleX, scaleY, scaleZ);
                    Matrix R = Matrix.CreateRotationZ(MathHelper.ToRadians(rot));
                    Matrix T = Matrix.CreateTranslation(tX, tY, tZ);
                    Matrix transform = Matrix.Multiply(R, T);
                    transform = Matrix.Multiply(S, transform);

                    basicEffect.World = world;
                    basicEffect.View = view;
                    basicEffect.Projection = projection;



                    basicEffect.World = Matrix.Multiply(transform, basicEffect.World);
                    // basicEffect.Projection = Matrix.Multiply(T, basicEffect.World);
                    // basicEffect.DiffuseColor = color;

                    // basicEffect.World = Matrix.Multiply(T, basicEffect.World);

                }
                mesh.Draw();
            }
        }



        float rotateModel = 0f;

        private void DrawModel2(Model model, Matrix world, Matrix view, Matrix projection)
        {
            rotateModel += 0.01f;

            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (Effect effect in mesh.Effects)
                {
                    BasicEffect basicEffect = (BasicEffect)effect;
                    // CommonBasicEffects.SetEffects(basicEffect);
                    basicEffect.EnableDefaultLighting();
                    basicEffect.AmbientLightColor = new Vector3(0.8f, 0.8f, 0.8f);
                    basicEffect.DirectionalLight0.Enabled = true;
                    basicEffect.DirectionalLight0.Direction = new Vector3(0.4f, 0.3f, 0.3f);
                    basicEffect.DirectionalLight0.DiffuseColor = new Vector3(0.8f, 0.8f, 0.8f);


                    // Matrix rotation = Matrix.CreateRotationZ(rotateModel);
                    // Matrix rotation = Matrix.CreateRotationX(rotateModel);

                    // basicEffect.TextureEnabled = true;
                    basicEffect.World = world;
                    basicEffect.View = view;
                    basicEffect.Projection = projection;
                    // basicEffect.DiffuseColor = Color.Red.ToVector3();
                    // basicEffect.World = Matrix.Multiply(rotation, basicEffect.World);
                }
                GraphicsDevice.RasterizerState = RasterizerState.CullNone;
                // GraphicsDevice.RasterizerState = RasterizerState.CullClockwise;
                mesh.Draw();
                // GraphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;
            }
        }
    }

}
