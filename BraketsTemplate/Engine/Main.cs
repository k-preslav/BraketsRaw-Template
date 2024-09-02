using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FontStashSharp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BraketsEngine;

public class Main : Game
{
    public List<Sprite> Sprites;
    public List<UIElement> UI;

    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private GameManager _gameManager;
    private DebugUI _debugUi;
    private float sendDiagnosticCooldown = 0.5f;
    private float _sendDiagnosticsTimer = 0;

    private bool HAS_INITIALIZED = false;

    // To calculate the framerate
    private float elapsedTime;
    private int frameCount;

    public Main()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "PreloadContent";
        IsMouseVisible = true;

        this.Exiting += OnExit;
        this.Window.ClientSizeChanged += OnResize;
    }

    protected override async void Initialize()
    {
        Debug.Log("Calling Initialize()", this);

        _spriteBatch = new SpriteBatch(_graphics.GraphicsDevice);
        _gameManager = new GameManager();

        Globals.LOAD_APP_P();

        Debug.Log("Applying application properties...", this);
        Window.Title = Globals.APP_Title;
        Window.AllowUserResizing = Globals.APP_Resizable;
        _graphics.PreferredBackBufferWidth = Globals.APP_Width;
        _graphics.PreferredBackBufferHeight = Globals.APP_Height;
        if (Globals.APP_VSync)
        {
            _graphics.SynchronizeWithVerticalRetrace = true;
            IsFixedTimeStep = true;
        }
        else
        {
            _graphics.SynchronizeWithVerticalRetrace = false;
            IsFixedTimeStep = false;
        }
        _graphics.ApplyChanges();

        Globals.ENGINE_Main = this;
        Globals.ENGINE_GraphicsDevice = _graphics.GraphicsDevice;
        Globals.ENGINE_SpriteBatch = _spriteBatch;

        _debugUi = new DebugUI();
        _debugUi.Initialize(this);

        if (Debugger.IsAttached)
            Globals.DEBUG_Overlay = true;

        new Camera(Vector2.Zero);

        this.Sprites = new List<Sprite>();
        this.UI = new List<UIElement>();

        if (Globals.BRIDGE_Run)
        {
            sendDiagnosticCooldown = Globals.BRIDGE_RefreshRate;
            if (sendDiagnosticCooldown == 0)
            {
                Globals.BRIDGE_Run = false;
            }
            else
            {
                BridgeClient bridgeClient = new BridgeClient();
                await bridgeClient.ConnectAsync(Globals.BRIDGE_Hostname, Globals.BRIDGE_Port);

                Globals.BRIDGE_Client = bridgeClient;
                Globals.BRIDGE_Client.OnReceive += (string msg) =>
                {
                    if (msg == "stop") Exit();
                };
            }
        }
        HAS_INITIALIZED = true;
        _graphics.ApplyChanges();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        Debug.Log("Loading content...", this);
        _gameManager.Start();
    }

    protected override void Update(GameTime gameTime)
    {
        if (!HAS_INITIALIZED)
            return;

        Input.GetKeyboardState();
        Input.GetMouseState();

        if (Input.IsPressed(Keys.F3))
            Globals.DEBUG_Overlay = !Globals.DEBUG_Overlay;

        float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
        Globals.DEBUG_DT = dt;

        Globals.Camera.CalculateMatrix();

        ParticleManager.Update();
        foreach (var elem in UI.ToList())
        {
            elem.Update(dt);
            elem.UpdateRect();
        }

        if (Globals.BRIDGE_Run)
        {
            _sendDiagnosticsTimer -= dt;
            DebugData.GetDiagnostics();

            if (_sendDiagnosticsTimer <= 0)
            {
                DebugBridge.SendData();
                _sendDiagnosticsTimer = sendDiagnosticCooldown;
            }
        }

        _gameManager?.Update(dt);
        foreach (var sp in Sprites.ToList())
        {
            if ((!Globals.STATUS_Loading && !LoadingScreen.isLoading) || sp.drawOnLoading)
            {
                sp.Update(dt);
                sp.UpdateRect();
            }
        }

        base.Update(gameTime);
    }


    protected override void Draw(GameTime gameTime)
    {

        if (!HAS_INITIALIZED)
            return;

        elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
        frameCount++;

        if (elapsedTime >= 0.25f)
        {
            Globals.DEBUG_FPS = frameCount / elapsedTime;
            frameCount = 0;
            elapsedTime = 0f;
        }

        GraphicsDevice.Clear(Globals.Camera.BackgroundColor);

        // ------- Game Layer -------
        _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, transformMatrix: Globals.Camera.TranslationMatrix);

        var sortedSprites = Sprites.OrderBy(sp => sp.Layer).ToList();
        foreach (var sp in sortedSprites)
        {
            if ((!Globals.STATUS_Loading && !LoadingScreen.isLoading) || sp.drawOnLoading)
                sp.Draw();
        }
        _spriteBatch.End();

        // ------- UI Layer ------- 
        _spriteBatch.Begin();
        foreach (var elem in UI.ToList())
        {
            if (LoadingScreen.isLoading)
            {
                if (elem.Tag.Contains("__loading__"))
                {
                    elem.DrawUI();
                }
                _debugUi.DrawOverlay(_spriteBatch, 0.25f);

                _spriteBatch.End();
                return;
            }

            elem.DrawUI();
        }
        _debugUi.DrawWindows(gameTime);
        _debugUi.DrawOverlay(_spriteBatch, 0.25f);
        _spriteBatch.End();

        base.Draw(gameTime);
    }

    public void AddSprite(Sprite sp) => Sprites.Add(sp);
    public void RemoveSprite(Sprite sp) => Sprites.Remove(sp);

    public void AddUIElement(UIElement elem) => UI.Add(elem);
    public void RemoveUIElement(UIElement elem) => UI.Remove(elem);

    private void OnExit(object sender, EventArgs e)
    {
        Debug.Log("Calling OnExit()");
        _gameManager.Stop();
    }
    private void OnResize(object sender, EventArgs e)
    {
        Globals.APP_Width = Window.ClientBounds.Width;
        Globals.APP_Height = Window.ClientBounds.Height;

        Debug.Log($"Resized window to size: {Globals.APP_Width}x{Globals.APP_Height}");
    }
}
