using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AulaXNA3D004
{
    public class _Quad
    {
        Game game;
        GraphicsDevice device;
        Matrix world;
        VertexPositionTexture[] verts;
        VertexBuffer buffer;
        Effect effect;
        Texture2D texture;        

        public _Quad(GraphicsDevice device, Game game, string textureName, string effectName)
        {
            this.game = game;
            this.device = device;
            this.world = Matrix.Identity;

            this.verts = new VertexPositionTexture[]
            {
                new VertexPositionTexture(new Vector3(-1, 1,0),Vector2.Zero),  //v0
                new VertexPositionTexture(new Vector3( 1, 1,0),Vector2.UnitX), //v1
                new VertexPositionTexture(new Vector3(-1,-1,0),Vector2.UnitY), //v2

                new VertexPositionTexture(new Vector3( 1, 1,0),Vector2.UnitX), //v3
                new VertexPositionTexture(new Vector3( 1,-1,0),Vector2.One),   //v4
                new VertexPositionTexture(new Vector3(-1,-1,0),Vector2.UnitY), //v5
            };

            this.buffer = new VertexBuffer(this.device,
                                           typeof(VertexPositionTexture),
                                           this.verts.Length,
                                           BufferUsage.None);
            this.buffer.SetData<VertexPositionTexture>(this.verts);

            this.effect = this.game.Content.Load<Effect>(effectName);

            this.texture = this.game.Content.Load<Texture2D>(textureName);

        }

        public void Dispose()
        {
            this.effect.Dispose();
            this.texture.Dispose();
        }

        public void Update(GameTime gameTime)
        {
        }

        public virtual void Draw(_Camera camera, Vector2 mouse, float radius)
        {
            this.device.SetVertexBuffer(this.buffer);

            this.effect.CurrentTechnique = this.effect.Techniques["Technique1"];
            this.effect.Parameters["World"].SetValue(this.world);
            this.effect.Parameters["View"].SetValue(camera.GetView());
            this.effect.Parameters["Projection"].SetValue(camera.GetProjection());
            this.effect.Parameters["colorTexture"].SetValue(this.texture);
            this.effect.Parameters["mouse"].SetValue(mouse);
            this.effect.Parameters["radius"].SetValue(radius);

            foreach (EffectPass pass in this.effect.CurrentTechnique.Passes)
            {
                pass.Apply();

                this.device.DrawUserPrimitives<VertexPositionTexture>(PrimitiveType.TriangleList,
                                                                    this.verts, 0, 2);
            }
        }
    }
}
