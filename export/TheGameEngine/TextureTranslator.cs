using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using CommonObjects;

namespace TheGameEngine
{
    public class TextureTranslator
    {
        public string GetDescription(Enum en)
        {
            Type type = en.GetType();
            MemberInfo[] memInfo = type.GetMember(en.ToString());
            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(Description),
                                                                false);
                if (attrs != null && attrs.Length > 0)
                    return ((Description)attrs[0]).Text;
            }
            return en.ToString();
        }
    }
}
