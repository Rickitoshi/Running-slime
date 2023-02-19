using Game.Player;
using Game.UI.Panels;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "ProjectConfigInstaller", menuName = "Installers/ProjectConfigInstaller")]
public class ProjectConfigInstaller : ScriptableObjectInstaller<ProjectConfigInstaller>
{
    [SerializeField] private ProjectSettings projectSettings;
    [SerializeField] private PlayerConfig playerConfig;
    [SerializeField] private PanelsAnimationConfig panelsAnimationConfig;

    public override void InstallBindings()
    {
        Container.BindInstance(projectSettings);
        Container.BindInstance(playerConfig);
        Container.BindInstance(panelsAnimationConfig);
    }
}