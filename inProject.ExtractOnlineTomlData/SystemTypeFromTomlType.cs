using System;
using System.Collections.Generic;
using System.Text;
using Nett;

namespace inProject.TomlObjectVisitor
{
    public class GetSystemType : ITomlObjectVisitor
    {
        public void Visit(TomlString s){throw new NotImplementedException("Prefere use the method with the out parameter.");}
        public void Visit(TomlString s, out string str)
        {
            throw new NotImplementedException();
        }

        public void Visit(TomlArray a){throw new NotImplementedException("Prefere use the method with the out parameter.");}
        public void Visit(TomlArray a, out Array array)
        {
            throw new NotImplementedException();
        }

        public void Visit(TomlInt i){throw new NotImplementedException("Prefere use the method with the out parameter.");}
        public void Visit(TomlInt i, out int nb)
        {
            throw new NotImplementedException();
        }

        public void Visit(TomlBool b){throw new NotImplementedException("Prefere use the method with the out parameter.");}
        public void Visit(TomlBool b, out bool boolean)
        {
            throw new NotImplementedException();
        }

        public void Visit(TomlFloat f){throw new NotImplementedException("Prefere use the method with the out parameter.");}
        public void Visit(TomlFloat f, out float nb)
        {
            throw new NotImplementedException();
        }

        public void Visit(TomlTable table)
        {
            throw new NotImplementedException();
        }

        public void Visit(TomlTableArray tableArray)
        {
            throw new NotImplementedException();
        }

        public void Visit(TomlDuration ts)
        {
            throw new NotImplementedException();
        }

        public void Visit(TomlOffsetDateTime dt)
        {
            throw new NotImplementedException();
        }

        public void Visit(TomlLocalDate ld)
        {
            throw new NotImplementedException();
        }

        public void Visit( TomlLocalDateTime ldt )
        {
            throw new NotImplementedException();
        }

        public void Visit(TomlLocalTime lt)
        {
            throw new NotImplementedException();
        }
    }
}
