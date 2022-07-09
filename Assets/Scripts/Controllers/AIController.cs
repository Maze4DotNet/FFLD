using UnityEngine;

namespace FFLD
{
    [CreateAssetMenu(fileName = "AIController", menuName = "InputController/AIController")]
    public class AIController : InputController
    {
        public override bool RetrieveDashInput()
        {
            throw new System.NotImplementedException();
        }

        public override bool RetrieveJumpInput()
        {
            return true;
        }

        public override float RetrieveMoveInput()
        {
            return 1f;
        }
    }
}