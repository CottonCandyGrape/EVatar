using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EyeHelpers
{
    public class EVatarInputField : InputField
    {
        public void AppendString(string input)
        {
            this.Append(input);
        }

        protected override void Append(string input)
        {
            base.Append(input);
            Debug.Log("Append Called");
        }
    }
}