using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DISERTATIE.FLACARA.CAPTURII.DTO;
public record FolderDTO
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int PhotoId { get; set; }
    public string Name { get; set; }
}