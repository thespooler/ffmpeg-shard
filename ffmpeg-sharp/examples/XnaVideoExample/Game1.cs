using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using FFmpegSharp;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using FFmpegSharp.Audio;

namespace FFmpegTest
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class Game1 : Microsoft.Xna.Framework.Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		private Texture2D _texture;

		private VideoDecoderStream _videoStream;
		private Color[] _nextFrameColour;

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";

			this.IsFixedTimeStep = false;
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			_videoStream = new VideoDecoderStream(@"C:\Users\Public\Videos\Sample Videos\Bear.wmv");

			base.Initialize();

			_nextFrameColour = new Color[_videoStream.FrameSize / 3];

			//byte[] buffer = new byte[1000000];
			//_videoStream.Read(buffer, 0, 1000000);
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);

			_texture = new Texture2D(GraphicsDevice, _videoStream.Width, _videoStream.Height, 1, TextureUsage.None, SurfaceFormat.Color);
		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// all content.
		/// </summary>
		protected override void UnloadContent()
		{
			_videoStream.Dispose();
		}

		private bool _stillShowingVideo = true;

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			// Allows the game to exit
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
				this.Exit();

			GraphicsDevice.Textures[0] = null;

			if (_stillShowingVideo)
			{
				byte[] nextFrame;
				_stillShowingVideo = _videoStream.ReadFrame(out nextFrame);

				if (_stillShowingVideo)
				{
					int counter = 0;
					for (int i = 0, length = nextFrame.Length; i < length; i += 3)
						_nextFrameColour[counter++] = new Color(nextFrame[i], nextFrame[i + 1], nextFrame[i + 2]);
					_texture.SetData<Color>(_nextFrameColour, 0, _nextFrameColour.Length, SetDataOptions.Discard);
				}
			}

			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

			if (_stillShowingVideo)
			{
				spriteBatch.Begin();
				spriteBatch.Draw(_texture, new Rectangle(0, 0, _texture.Width, _texture.Height), Color.White);
				spriteBatch.End();
			}

			base.Draw(gameTime);
		}
	}
}
