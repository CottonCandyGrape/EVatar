using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EyeHelpers
{
    public class EVatarInputField : InputField
    {
        public void AppendChar(char input)
        {
            Append(input);
        }

        protected override void Append(char input)
        {
            base.Append(input);
            Validate(text, text.Length, input);
            textComponent.text = this.text;
        }

        public void AppendString(string input)
        {
            this.Append(input);
        }

        protected override void Append(string input)
        {
            base.Append(input);
            
            textComponent.text = this.text;
        }
    }
}