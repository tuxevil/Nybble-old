using NHibernate.Dialect;
using NHibernate.Dialect.Function;

namespace ProjectBase.Data
{
    public class FullTextDialect : MsSql2000Dialect
    {
        public FullTextDialect()
        {
            RegisterFunction("freetext", new SQLFunctionTemplate(null, "freetext($1,$2)"));
            RegisterFunction("contains", new SQLFunctionTemplate(null, "freetext($1,$2)"));
            //RegisterFunction("freetext", new StandardSQLFunction("freetext", null));
            //RegisterFunction("contains", new StandardSQLFunction("contains", null));
        }
    }
}