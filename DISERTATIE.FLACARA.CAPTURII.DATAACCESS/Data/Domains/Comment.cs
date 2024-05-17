using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DISERTATIE.FLACARA.CAPTURII.DATAACCESS.Data.Domains;

[Table("table_Comments")]
public class Comment : IIdentifiable
{
    [ExplicitKey]
    public required int Id { get; set; }
    public required int PhotoId { get; set; }
    public required int UserId { get; set; }
    public required string Text { get; set; }
}
