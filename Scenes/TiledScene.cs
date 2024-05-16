using System;
using GXPEngine.Physics;
using GXPEngine.SceneManagement;
using System.Drawing;
using TiledMapParser;

namespace GXPEngine.Scenes
{
    internal class TiledScene : Scene
    {
        private enum SceneState {  StartAnim, Playing, EndAnim, FadeOut, FadeIn }
        private SceneState _state = SceneState.FadeIn;

        private readonly UI _ui = new UI(Game.main.width, Game.main.height);
        private readonly string _levelToLoad;
        private readonly int _levelId;

        private AnimationSprite _animationHand;
        private TextObject _currentHitCount;
        private bool _isAnimAtHalfway = false;
        private string _nextScene = null;
        private float _fadeAlpha = 255;
        private Vec2 _initialPosition;

        public int targetY = 0;
        public int hitCount = 0;

        public TiledScene(string levelToLoad, string nextScene)
        {
            _levelToLoad = levelToLoad;
            _levelId = int.Parse(levelToLoad.Substring(levelToLoad.IndexOf("Level") + 5, levelToLoad.IndexOf(".tmx") - levelToLoad.IndexOf("Level") - 5));
            _nextScene = nextScene;
        }

        public TiledScene(string levelToLoad)
        {
            _levelToLoad = levelToLoad;
            _levelId = int.Parse(levelToLoad.Substring(levelToLoad.IndexOf("Level") + 5, levelToLoad.IndexOf(".tmx") - levelToLoad.IndexOf("Level") - 5));
        }

        public TiledScene() { throw new Exception("Please provide a level to load");}

        public override void OnLoad()
        {
            GameData.ResetLevelData();
            LoadLevel();
            InitializeUI();
            InitializeAnimaiton();
            Game.main.OnBeforeStep += PhysicsManager.Step;
            Game.main.OnBeforeStep += PhysicsObjectManager.Update;
            PhysicsManager.PrintColliders();
            GameData.NextScene = _nextScene ?? $"TransitionLevel{_levelId + 1}";
        }

        public override void OnUnload() 
        {
            PhysicsManager.ClearColliders();
            foreach (var child in GetChildren()) child.Destroy();
            PhysicsObjectManager.Reset();
            Game.main.OnBeforeStep -= PhysicsManager.Step;
            Game.main.OnBeforeStep -= PhysicsObjectManager.Update;
        }

        private void InitializeAnimaiton()
        {
            AddChild(_animationHand = new AnimationSprite("start_hand.png", 2, 1));
            _animationHand.SetOrigin(_animationHand.width / 2 + 8, _animationHand.height - 8);
            _animationHand.SetXY(GameData.ActivePlayer.Collider.Position.x, GameData.ActivePlayer.Collider.Position.y);
            _animationHand.alpha = 0;
            GameData.ActivePlayer.alpha = 0;
            _initialPosition = new Vec2(_animationHand.x, _animationHand.y);
        }

        private void InitializeUI()
        {
            AddChild(_ui);
            _ui.SetTextObjectFont("DotGothic16.ttf", 20);
            var uiSprite = _ui.Image("gameplay_ui.png", 0, 0);
            uiSprite.scale = GameData.UIScale;
            _ui.SetAlignment(UI.Alignment.MIN, UI.Alignment.MIN);
            _ui.TextObject("Level", 0, 15, Color.White, 300, 250);
            _ui.TextObject(_levelId.ToString(), 35, 50, Color.White, 300, 275, Utils.LoadFont("DotGothic16.ttf", 24));
            _ui.TextObject("moves", 115, 45, Color.White, 200, 30, Utils.LoadFont("DotGothic16.ttf", 10));
            _currentHitCount = _ui.TextObject("0/0", 100, 20, Color.White, 300, 300, Utils.LoadFont("DotGothic16.ttf", 24, FontStyle.Bold));
        }

        private void Update()
        {
            _currentHitCount.Text = $"{hitCount}/{GameData.MaxHits[_levelId - 1]}";
            _ui.SceneUpdate.Invoke();
            switch (_state)
            {
                case SceneState.StartAnim:
                    StartAnim();
                    break;
                case SceneState.Playing:
                    break;
                case SceneState.EndAnim:
                    EndAnim();
                    break;
                case SceneState.FadeOut:
                    Fade(true);
                    break;
                case SceneState.FadeIn:
                    Fade(false);
                    break;
            }
        }

        private void StartAnim()
        {
            if (!_isAnimAtHalfway && _animationHand.y < _initialPosition.y + (-GameData.PlayerSpawnYOffset))
            {
                _animationHand.y += GameData.PlayerStartAnimSpeed;
                GameData.ActivePlayer.Collider.AddPosition(new Vec2(0, GameData.PlayerStartAnimSpeed));
                _animationHand.alpha = Mathf.Clamp((_animationHand.y - _initialPosition.y) / (-GameData.PlayerSpawnYOffset), 0, 1);
                GameData.ActivePlayer.alpha = _animationHand.alpha;
                if (_animationHand.y >= _initialPosition.y + (-GameData.PlayerSpawnYOffset)) _isAnimAtHalfway = true;
            }
            else if(_isAnimAtHalfway && _animationHand.y >= _initialPosition.y)
            {
                _animationHand.SetFrame(1);
                _animationHand.y -= GameData.PlayerStartAnimSpeed;
                _animationHand.alpha = (_animationHand.y - _initialPosition.y) / (-GameData.PlayerSpawnYOffset);
                if (_animationHand.y <= _initialPosition.y)
                {
                    _state = SceneState.Playing;
                    GameData.ActivePlayer.Collider.IsActive = true;
                }
            }
        }

        private void EndAnim()
        {
            
            if (!_isAnimAtHalfway && _animationHand.y < GameData.ActivePlayer.Collider.Position.y)
            {
                _animationHand.y += GameData.PlayerStartAnimSpeed;
                _animationHand.alpha = Mathf.Clamp((_animationHand.y - _initialPosition.y) / (-GameData.PlayerSpawnYOffset), 0, 1);
                if(_animationHand.y >= GameData.ActivePlayer.Collider.Position.y) _isAnimAtHalfway = true;
            }
            else if(_isAnimAtHalfway && _animationHand.y >= _initialPosition.y)
            {
                _animationHand.SetFrame(0);
                _animationHand.y -= GameData.PlayerStartAnimSpeed;
                GameData.ActivePlayer.Collider.AddPosition(new Vec2(0, -GameData.PlayerStartAnimSpeed));
                _animationHand.alpha = Mathf.Clamp((_animationHand.y - _initialPosition.y) / (-GameData.PlayerSpawnYOffset), 0, 1);
                GameData.ActivePlayer.alpha = _animationHand.alpha;
                if (_animationHand.y <= _initialPosition.y) _state = SceneState.FadeOut;
            }
        }

        private void LoadLevel()
        {
            TiledLoader loader = new TiledLoader(_levelToLoad, this, false, 0, 0);
            CustomObjectLoader.Initialize(loader);
            loader.AddManualType("WindCurrent", "Player", "Fire", "Windmill");
            loader.autoInstance = true;
            loader.LoadTileLayers();
            loader.LoadObjectGroups();
            CustomObjectLoader.Stop(loader);
            ColliderLoader.InstantiateColliders();
        }

        private void Fade(bool reverse)
        {
            _ui.Canvas.Clear(Color.FromArgb(Mathf.Clamp((int)_fadeAlpha, 0, 255), 0, 0, 0));
            _fadeAlpha += reverse ? GameData.FadeSpeed : -GameData.FadeSpeed;
            if(reverse && _fadeAlpha >= 255) SceneManager.LoadScene(GameData.NextScene);
            else if(!reverse && _fadeAlpha <= 0) _state = SceneState.StartAnim;
        }

        public void HitFlag()
        {
            _state = SceneState.EndAnim;
            _isAnimAtHalfway = false;
            _animationHand.SetXY(GameData.ActivePlayer.Collider.Position.x, GameData.ActivePlayer.Collider.Position.y + GameData.PlayerSpawnYOffset);
            _initialPosition = new Vec2(_animationHand.x, _animationHand.y);
        }
    }
}
