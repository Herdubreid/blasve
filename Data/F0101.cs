using System.Linq;

namespace blasve.Data.F0101
{
    public class Row
    {
        public long F0101_AN8 { get; set; }
        public string F0101_DC { get; set; }
        public string F0101_ALPH { get; set; }
        public string F0101_AT1 { get; set; }
    }
    public class Response : Celin.AIS.Response
    {
        public Celin.AIS.Form<Celin.AIS.FormData<Row>> fs_DATABROWSE_F0101 { get; set; }
    }
    public class Request : Celin.AIS.DatabrowserRequest
    {
        public Request(string [] keywords)
        {
            outputType = "GRID_DATA";
            dataServiceType = "BROWSE";
            targetName = "F0101";
            targetType = "table";
            returnControlIDs = "AN8|DC|ALPH|AT1";
            maxPageSize = "20";
            query = new Celin.AIS.Query
            {
                condition = keywords.Select(w =>
                {
                    return new Celin.AIS.Condition
                    {
                        value = new Celin.AIS.Value[]
                        {
                            new Celin.AIS.Value
                            {
                                content = w.ToUpper(),
                                specialValueId = "LITERAL"
                            }
                        },
                        controlId = "F0101.DC",
                        @operator = "STR_CONTAIN"
                    };
                }).ToList(),
                matchType = "MATCH_ALL",
                autoFind = true
            };
        }
    }
}
