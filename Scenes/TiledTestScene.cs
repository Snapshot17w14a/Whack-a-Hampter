using System;
using GXPEngine.Physics;
using GXPEngine.SceneManager;
using TiledMapParser;

namespace GXPEngine.Scenes
{
    internal class TiledTestScene : Scene
    {
        enum SceneState {  StartAnim, Playing, EndAnim }
        private SceneState _state = SceneState.StartAnim;

        private AnimationSprite _animationHand;
        private Vec2 _initialPosition;
        private bool _isAnimAtHalfway = false;

        public int targetY = 0;

        public TiledTestScene() {  }

        public override void OnLoad()
        {
            LoadLevel();
            ColliderLoader.InstantiateColliders();
            _animationHand = new AnimationSprite("start_hand.png", 2, 1);
            _animationHand.SetOrigin(_animationHand.width / 2 + 8, _animationHand.height - 8);
            _animationHand.SetXY(GameData.ActivePlayer.Collider.Position.x, GameData.ActivePlayer.Collider.Position.y);
            _initialPosition = new Vec2(_animationHand.x, _animationHand.y);
            AddChild(_animationHand);
            Game.main.OnBeforeStep += PhysicsManager.Step;
            Game.main.OnBeforeStep += PhysicsObjectManager.Update;
        }

        public override void OnUnload() 
        { 
            Game.main.OnBeforeStep -= PhysicsManager.Step;
            Game.main.OnBeforeStep -= PhysicsObjectManager.Update;
        }

        private void Update()
        {
            switch(_state)
            {
                case SceneState.StartAnim:
                    StartAnim();
                    break;
                case SceneState.Playing:
                    
                    break;
                case SceneState.EndAnim:
                    //End animation
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
            else if(_isAnimAtHalfway && _animationHand.y >= _initialPosition.y )
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

        private void LoadLevel()
        {
            TiledLoader loader = new TiledLoader(GameData.TiledSceneMap, defaultOriginX: 0, defaultOriginY: 0);
            CustomObjectLoader.Initialize(loader);
            loader.AddManualType("WindCurrent", "Player", "Fire");
            loader.autoInstance = true;
            loader.LoadTileLayers();
            loader.LoadObjectGroups();
            CustomObjectLoader.Stop(loader);
            foreach(var asd in GameData.TileSlowdownValues.Keys)
            {
                Console.WriteLine(asd + " | " + GameData.TileSlowdownValues[asd]);
            }
        }
    }
}
