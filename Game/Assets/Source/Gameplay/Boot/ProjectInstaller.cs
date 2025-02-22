using VContainer;
using VContainer.Unity;
using Infrastructure.Pool.Installers;
using Infrastructure.Resource.Installers;
using Infrastructure.SceneLoading.Installers;
using Infrastructure.StateMachine.Installers;
using Infrastructure.MVVM.Installers;
using Infrastructure.Config.Installers;
using Gameplay.Core;
using Infrastructure.Time.Installers;

namespace Gameplay.Boot
{
    public sealed class ProjectInstaller : LifetimeScope
    {
        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(this);
        }

        protected override void Configure(IContainerBuilder builder)
        {
            // States
            builder.Register<BootState>(Lifetime.Singleton).AsSelf();
            builder.Register<GameplayState>(Lifetime.Singleton).AsSelf();

            // Infrastructure
            InstallBindings<StateMachineInstaller>(builder);
            InstallBindings<SceneLoaderInstaller>(builder);
            InstallBindings<ResourcesInstaller>(builder);
            InstallBindings<ObjectPoolInstaller>(builder);
            InstallBindings<MvvmInstaller>(builder);
            InstallBindings<ConfigProviderInstaller>(builder);
            InstallBindings<TimeServiceInstaller>(builder);

            // Core
            InstallBindings<CharactersInstaller>(builder);
        }

        private static void InstallBindings<TInstaller>(IContainerBuilder builder) where TInstaller : IInstaller,  new() 
        {
            TInstaller installer = new TInstaller();
            installer.Install(builder);
        }
    }
}