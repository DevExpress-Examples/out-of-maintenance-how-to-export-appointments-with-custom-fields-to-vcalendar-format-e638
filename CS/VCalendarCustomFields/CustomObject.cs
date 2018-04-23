using System;
using System.Collections.Generic;
using System.Text;

namespace VCalendarCustomFields {
    public class CustomObject {
        string name;
        string objType;
        string valType;
        string val;

        public CustomObject() {
        }
        public string Name { get { return name; } set { name = value; } }
        public string ObjectType { get { return objType; } set { objType = value; } }
        public string ValueType { get { return valType; } set { valType = value; } }
        public string Value { get { return val; } set { val = value; } }

        public override string ToString() {
            return String.Format("NAME={0} TYPE={1} VALUE={2}: {3}", Name, ObjectType, ValueType, Value);
        }
    }
}
