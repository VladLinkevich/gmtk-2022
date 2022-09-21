﻿using Code.Game;
using Code.Game.CardLogic;
using UnityEngine;
using Zenject;

namespace Code.Installer
{
  [CreateAssetMenu(fileName = "ProjectSettings", menuName = "Data/ProjectSettings")]
  public class ProjectSettings : ScriptableObjectInstaller<ProjectSettings>
  {
    public CardFactory.Settings CardFactory;
    public CardPositioner.Settings CardPositioner;
    public DiceMover.Settings DiceMover;
    public ObjectFactory.Settings ObjectFactory;
    public EnemyTargetSelecter.Settings PickTarget;
    public PlayerDeck.Settings PlayerDeck;
    public EnemyDeck.Settings EnemyDeck;

    public override void InstallBindings()
    {
      Container.BindInstance(CardFactory).IfNotBound();
      Container.BindInstance(CardPositioner).IfNotBound();
      Container.BindInstance(DiceMover).IfNotBound();
      Container.BindInstance(ObjectFactory).IfNotBound();
      Container.BindInstance(PickTarget).IfNotBound();
      Container.BindInstance(PlayerDeck).IfNotBound();
      Container.BindInstance(EnemyDeck).IfNotBound();
    }
  }
}