using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
using X2DPE;
using X2DPE.Helpers;
using WindowsGame4;

namespace Particles
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class Game1 : Microsoft.Xna.Framework.Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		Texture2D backGround;
		Texture2D customMousePointer;
		SpriteFont VideoFont;
		ParticleComponent particleComponent;

		MouseState mouseState;
		ButtonState lastButtonState;

		Random random;

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";

			//graphics.IsFullScreen = true;

			graphics.PreferredBackBufferWidth = 1280;
			graphics.PreferredBackBufferHeight = 720;

			graphics.ApplyChanges();

			// IsMouseVisible = true;

			FrameRateCounter FrameRateCounter = new FrameRateCounter(this, "Fonts\\Default");
			particleComponent = new ParticleComponent(this);

			this.Components.Add(FrameRateCounter);
			this.Components.Add(particleComponent);
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			random = new Random();

			base.Initialize();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);

			backGround = Content.Load<Texture2D>("Sprites\\spooky_forest");
			customMousePointer = Content.Load<Texture2D>("Sprites\\pointer");
			VideoFont = Content.Load<SpriteFont>("Fonts\\Default");

			particleComponent.particleEmitterList.Add(
					new Emitter()
					{
						Active = false,
						TextureList = new List<Texture2D>() {
			      Content.Load<Texture2D>("Sprites\\flower_orange"),
			      Content.Load<Texture2D>("Sprites\\flower_green"),
			      Content.Load<Texture2D>("Sprites\\flower_yellow"),
			      Content.Load<Texture2D>("Sprites\\flower_purple")
			    },
						RandomEmissionInterval = new RandomMinMax(8.0d),
						ParticleLifeTime = 2000,
						ParticleDirection = new RandomMinMax(0, 359),
						ParticleSpeed = new RandomMinMax(0.1f, 1.0f),
						ParticleRotation = new RandomMinMax(0, 100),
						RotationSpeed = new RandomMinMax(0.015f),
						ParticleFader = new ParticleFader(false, true, 1350),
						ParticleScaler = new ParticleScaler(false, 0.3f)
					}
			);

			Emitter testEmitter2 = new Emitter();
			testEmitter2.Active = true;
			testEmitter2.TextureList.Add(Content.Load<Texture2D>("Sprites\\raindrop"));
			testEmitter2.RandomEmissionInterval = new RandomMinMax(16.0d);
			testEmitter2.ParticleLifeTime = 1000;
			testEmitter2.ParticleDirection = new RandomMinMax(170);
			testEmitter2.ParticleSpeed = new RandomMinMax(10.0f);
			testEmitter2.ParticleRotation = new RandomMinMax(0);
			testEmitter2.RotationSpeed = new RandomMinMax(0f);
			testEmitter2.ParticleFader = new ParticleFader(false, true, 800);
			testEmitter2.ParticleScaler = new ParticleScaler(false, 1.0f);
			testEmitter2.Opacity = 255;

			particleComponent.particleEmitterList.Add(testEmitter2);

			Emitter fireEmitter = new Emitter();
			fireEmitter.Active = true;
			fireEmitter.TextureList.Add(Content.Load<Texture2D>("Sprites\\fire"));
			fireEmitter.RandomEmissionInterval = new RandomMinMax(300);
			fireEmitter.ParticleLifeTime = 1500;
			fireEmitter.ParticleDirection = new RandomMinMax(0);
			fireEmitter.ParticleSpeed = new RandomMinMax(0.3f);
			fireEmitter.ParticleRotation = new RandomMinMax(0);
			fireEmitter.RotationSpeed = new RandomMinMax(0);
			fireEmitter.ParticleFader = new ParticleFader(true, true, 0);
			fireEmitter.ParticleScaler = new ParticleScaler(0.1f, 0.2f, 0, 1000);
			fireEmitter.Position = new Vector2(140, 580);

			Emitter smokeEmitter = new Emitter();
			smokeEmitter.Active = true;
			smokeEmitter.TextureList.Add(Content.Load<Texture2D>("Sprites\\smoke"));
			smokeEmitter.RandomEmissionInterval = new RandomMinMax(200);
			smokeEmitter.ParticleLifeTime = 9000;
			smokeEmitter.ParticleDirection = new RandomMinMax(-5, 5);
			smokeEmitter.ParticleSpeed = new RandomMinMax(.6f);
			smokeEmitter.ParticleRotation = new RandomMinMax(0);
			smokeEmitter.RotationSpeed = new RandomMinMax(-0.008f, 0.008f);
			smokeEmitter.ParticleFader = new ParticleFader(true, true);
			smokeEmitter.ParticleScaler = new ParticleScaler(0.15f, 0.7f, 400, smokeEmitter.ParticleLifeTime);
			smokeEmitter.Position = new Vector2(240, 160);

			particleComponent.particleEmitterList.Add(fireEmitter);
			particleComponent.particleEmitterList.Add(smokeEmitter);
		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// all content.
		/// </summary>
		protected override void UnloadContent()
		{
			// TODO: Unload any non ContentManager content here
		}

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

			mouseState = Mouse.GetState();

			// Particle modification
			particleComponent.particleEmitterList[0].Position = new Vector2((float)mouseState.X, (float)mouseState.Y);

			if (mouseState.LeftButton == ButtonState.Pressed && lastButtonState != ButtonState.Pressed)
			{
				particleComponent.particleEmitterList[0].Active = !particleComponent.particleEmitterList[0].Active;
			}
			lastButtonState = mouseState.LeftButton;


			Emitter t2 = particleComponent.particleEmitterList[1];
			t2.Position = new Vector2((float)random.NextDouble() * (graphics.GraphicsDevice.Viewport.Width), 0);
			if (t2.EmittedNewParticle)
			{
				float f = MathHelper.ToRadians(t2.LastEmittedParticle.Direction + 180);
				t2.LastEmittedParticle.Rotation = f;
			}

			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			// TODO: Move to variable in Emitter class
			int activeParticles = 0;
			foreach (Emitter activeEmitters in particleComponent.particleEmitterList)
			{
				activeParticles += activeEmitters.ParticleList.Count();
			}

			GraphicsDevice.Clear(Color.Black);

			spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

			spriteBatch.Draw(backGround, Vector2.Zero, Color.White);
			spriteBatch.Draw(customMousePointer, new Vector2((float)mouseState.X, (float)mouseState.Y), null, Color.White, 0, new Vector2(customMousePointer.Width / 2, customMousePointer.Width / 2), 1.0f, SpriteEffects.None, 0);
			spriteBatch.DrawString(VideoFont,
															activeParticles.ToString(),
															new Vector2(10, 30),	//Game.GraphicsDevice.Viewport.Width - 25, Game.GraphicsDevice.Viewport.Height - VideoFont.LineSpacing),
															Color.White,
															0f,
															Vector2.Zero,
															1.0f,
															SpriteEffects.None,
															0);
			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
