using System;

namespace SeagullDK.TaskPerformer {
    
    public struct ParamedAction {

        /// <summary>
        /// Get a paramed action with the action and the param
        /// </summary>
        public static implicit operator ParamedAction((Action<object[]> action, object[] param) value) {
            return new ParamedAction(value.action, value.param);
        }
        
        /// <summary>
        /// Get a paramed action with the action
        /// </summary>
        public static implicit operator ParamedAction(Action action) {
            return new ParamedAction(_ => action(), null);
        }
        
        public readonly Action<object[]> action;
        public readonly object[] param;

        private ParamedAction(Action<object[]> action, params object[] param) { 
            this.action = action; 
            this.param = param;
        }
    }
    
}