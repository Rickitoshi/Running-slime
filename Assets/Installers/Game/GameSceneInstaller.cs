using Game.Player;
using Signals;
using Zenject;

public class GameSceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<InputHandler>().AsSingle().NonLazy();
        
        BindSignals();
    }
    
    private void BindSignals()
    {
        Container.DeclareSignal<PlayerJumpSignal>();
        Container.DeclareSignal<ChangePanelUISignal>();
        Container.DeclareSignal<CoinsCountChangedSignal>();
        Container.DeclareSignal<ScoreChangedSignal>();
    }
}
