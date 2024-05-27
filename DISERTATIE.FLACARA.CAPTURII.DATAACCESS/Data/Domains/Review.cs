using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DISERTATIE.FLACARA.CAPTURII.DATAACCESS.Data.Domains;

[Table("table_Reviews")]
public class Review : IIdentifiable
{
    [Key]
    public int Id { get; set; }
    public required int PhotoId { get; set; }
    public required int UserId { get; set; }
    public required int Grade { get; set; }
    public required string Text { get; set; }
}
