namespace Monostori_Robert_backend.Models
{
    public record CreateActorDto( int ActorId,  string ActorName);

    public record UpdateActorDto( int ActorId, string ActorName );


    public record CreateFilmTypeDto(int TypeId, string TypeName);

    public record UpdateFilmTypeDto(int TypeId, string TypeName);
}
