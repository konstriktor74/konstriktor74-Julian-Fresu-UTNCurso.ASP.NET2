namespace UTNCurso.BLL.Services.Mappers
{
    public interface IMapper<TDal, TDto>
    {
        public TDal MapDtoToDal(TDto dto);

        public TDto MapDalToDto(TDal entity);

        public IEnumerable<TDto> MapDalToDto(IEnumerable<TDal> entities);
    }
}
