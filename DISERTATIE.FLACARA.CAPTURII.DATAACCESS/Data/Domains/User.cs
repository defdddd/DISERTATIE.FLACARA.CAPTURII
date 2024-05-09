using Dapper.Contrib.Extensions;
using DISERTATIE.FLACARA.CAPTURII.UTILS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace DISERTATIE.FLACARA.CAPTURII.DATAACCESS.Data.Domains;

[Table("table_Users")]
public class User
{
    [ExplicitKey]
    public int Id { get; set; }
    public required string UserName { get; set; }
    public required string Password { get; set; }
    public required string Email { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string? ProfilePicture { get; set; }
    public string? City { get; set; }
    public Gender Gender { get; set; }
    public Status Status { get; set; }
    public Role Role { get; set; }
}
