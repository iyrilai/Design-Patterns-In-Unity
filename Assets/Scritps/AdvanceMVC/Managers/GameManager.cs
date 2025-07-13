using CompanyName.ProjectName.Base;
using System.Threading.Tasks;
using UnityEngine;

namespace CompanyName.ProjectName.Managers
{
    public class GameManager : MonoBehaviour, IManager
    {
        [SerializeField] Camera _camera; // underscore because of 'camera' field already exists in MonoBehaviour

        ControllerManager controller;

        public static GameManager instance;
        public static GameManager Instance => instance;

        private void Awake()
        {
            CreateSingleton();
            SetGameSetting();
        }

        void CreateSingleton()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
                return;
            }

            Destroy(gameObject);
        }

        void SetGameSetting()
        {
            Application.targetFrameRate = 60;
        }

        async void Start()
        {
            await Init();
            await LoadGameAssets();
        }

        void Update()
        {
            controller.Update();
        }

        void FixedUpdate()
        {
            controller.FixedUpdate();
        }

        public async Task Init()
        {
            await InitManager(ControllerManager.Instance);
            controller = ControllerManager.Instance;

            // more manager initialize
        }

        async Task InitManager(IManager manager)
        {
            try
            {
                await manager.Init();
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Failed to initialize manager: {manager.GetType().Name}. Exception: {e.Message}");
            }
        }

        async Task LoadGameAssets()
        {
            // controller.CreateController<CameraController, CameraView>(_camera.gameObject);
            // controller.CreateController<ObstructionGenerateController>();

            var player = await CreateNewPlayer();
            // controller.CreateControllerWithRef<PlayerController, PlayerView>(player);
        }

        async Task<GameObject> CreateNewPlayer()
        {
            // Load Async gameobject or .....

            await Task.CompletedTask;
            return new GameObject();
        }
    }
}