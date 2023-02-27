using Game.Managers;
using Game.Player;
using Signals;
using Zenject;

public class GameSceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<SaveSystem>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<Wallet>().AsSingle().NonLazy();

        BindSignals();
    }
    
    private void BindSignals()
    {
        Container.DeclareSignal<PlayerJumpSignal>();
        Container.DeclareSignal<ChangeGameStateSignal>();
        Container.DeclareSignal<ChangeUIStateSignal>();
        Container.DeclareSignal<CoinsCountChangedSignal>();
        Container.DeclareSignal<CoinsAddSignal>();
        Container.DeclareSignal<CoinsRemoveSignal>();
        Container.DeclareSignal<ScoreChangedSignal>();
        Container.DeclareSignal<OnPlayerDieSignal>();
        Container.DeclareSignal<GraphicsSettingsChangedSignal>();
        Container.DeclareSignal<TargetFPSChangedSignal>();
        Container.DeclareSignal<SoundSettingsChangedSignal>();
    }
}
