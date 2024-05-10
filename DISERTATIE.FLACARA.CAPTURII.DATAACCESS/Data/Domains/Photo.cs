using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DISERTATIE.FLACARA.CAPTURII.DATAACCESS.Data.Domains;

[Table("table_Photos")]
public class Photo
{
    [ExplicitKey]
    public required int Id { get; set; }
    public required int UserId { get; set; }
    public int FolderId { get; set; }
    public required string URL { get; set; }
    public required string Type { get; set; }
    public bool IsPublic { get; set; }
    public string FileName { get; set; }
    public string Description { get; set; }


}
