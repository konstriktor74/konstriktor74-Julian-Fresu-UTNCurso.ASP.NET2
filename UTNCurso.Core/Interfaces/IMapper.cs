namespace UTNCurso.Core.Interfaces
{
    public interface IMapper<TDal, TDto>
    {
        public TDal MapDtoToDal(TDto dto);

        public TDto MapDalToDto(TDal entity);

        public IEnumerable<TDto> MapDalToDto(IEnumerable<TDal> entities);

        public IEnumerable<TDto> MapDalToDto(IReadOnlyCollection<TDal> entities);
    }
}
