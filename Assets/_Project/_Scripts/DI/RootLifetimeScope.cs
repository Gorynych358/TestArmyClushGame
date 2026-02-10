using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ACT.Scripts
{
    public sealed class RootLifetimeScope : LifetimeScope
    {
        [SerializeField] private AudioLibrary _audioLibrary;
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<SceneTransitionView>();
            /*builder.RegisterComponentInNewPrefab(
                transitionViewPrefab,
                Lifetime.Singleton);*/
            builder.RegisterInstance(_audioLibrary);
            builder.RegisterComponentInHierarchy<AudioRoot>();
            /*builder.RegisterComponentInNewPrefab(
                audioRootPrefab,
                Lifetime.Singleton);*/

            builder.Register<IEventBus, EventBus>(Lifetime.Singleton);
             
            builder.Register<ISoundManager, SoundManager>(Lifetime.Singleton);

            builder.Register<ISceneTransitionManager, SceneTransitionManager>(Lifetime.Singleton);

            builder.RegisterEntryPoint<EntryPoint>();
        }
    }
}
