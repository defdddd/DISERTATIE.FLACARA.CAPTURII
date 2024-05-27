using DISERTATIE.FLACARA.CAPTURII.UTILS;

namespace DISERTATIE.FLACARA.CAPTURII.DTO.EntityDTO;

public record RegisterUserDTO : LoginUserDTO
{
    public int? Id { get; set; }
    public required string Email { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
}