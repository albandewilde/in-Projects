using System;
using System.Collections.Generic;
using System.Text;
using Nett;

namespace inProject.ExtractOnlineTomlData
{
    class TomlObjectVisitor : ITomlObjectVisitor
    {
        public void Visit(TomlTable table)
        {
            throw new NotImplementedException();
        }

        public void Visit(TomlTableArray tableArray)
        {
            throw new NotImplementedException();
        }

        public void Visit(TomlInt i)
        {
            throw new NotImplementedException();
        }

        public void Visit(TomlFloat f)
        {
            throw new NotImplementedException();
        }

        public void Visit(TomlBool b)
        {
            throw new NotImplementedException();
        }

        public void Visit(TomlString s)
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

        public void Visit(TomlArray a)
        {
            throw new NotImplementedException();
        }
    }
}
