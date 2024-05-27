using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DISERTATIE.FLACARA.CAPTURII.DTO.DomainsDTO;

namespace DISERTATIE.FLACARA.CAPTURII.DTO.EntityDTO;

public record FolderWithPhotosDTO
{
    public int? Id { get; set; }
    public string Name { get; set; }
    public List<PhotoDTO>? Photos { get; set; }
}