using UnityEngine;
using UnityEngine.SceneManagement;

namespace TradeMarket.Characrer
{
    public class InteractableSceneComponent : InteractableItemComponentBase
    {
        [SerializeField] private string _sceneName;
        //add scene manager
        
        public override void Interract()
        {
            SceneManager.LoadScene(_sceneName);
        }
    }
}