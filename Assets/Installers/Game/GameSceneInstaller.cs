using Game.Player;
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
 
    }
}
