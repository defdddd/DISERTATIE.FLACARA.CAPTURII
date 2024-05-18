using DISERTATIE.FLACARA.CAPTURII.DTO.EntityDTO;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DISERTATIE.FLACARA.CAPTURII.DTO.DomainsDTO;

public record PhotoDTO
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int FolderId { get; set; }
    public string URL { get; set; }
    public string Type { get; set; }
    public string FileName { get; set; }
    public bool IsPublic { get; set; }
    public string Description { get; set; }
    public List<CommentDTO>? Comments { get; set; }
    public List<ReviewDTO>? Reviews { get; set; }
    public UserProfileDTO? User { get; set; }


}
