using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace AulaXNA3D004
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        _Screen screen;
        _Camera camera;
        _Quad quad;

        MouseState mouse;
        float radius = 0.2f;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
        }

        protected override void Initialize()
        {
            this.screen = _Screen.GetInstance();
            this.screen.SetWidth(graphics.PreferredBackBufferWidth);
            this.screen.SetHeight(graphics.PreferredBackBufferHeight);

            this.camera = new _Camera();

            this.quad = new _Quad(GraphicsDevice, this, @"Textures\logoJD", @"Effects\Effect1");

            this.mouse = new MouseState();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent()
        {
            this.quad.Dispose();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            this.quad.Update(gameTime);

            this.mouse = Mouse.GetState();

            float aux = 0.2f + Math.Abs(this.mouse.ScrollWheelValue * gameTime.ElapsedGameTime.Milliseconds * 0.00001f);
            this.radius =  aux < 0.2f ? 0.2f : aux;

            Window.Title = "AulaXNA3D004 - Radius: " + this.radius.ToString();
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            this.quad.Draw(this.camera, new Vector2(this.mouse.X / (float)this.screen.GetWidth(),
                                                    this.mouse.Y / (float)this.screen.GetHeight()),
                                                    this.radius);

            base.Draw(gameTime);
        }
    }
}
