namespace Projeto_Api.Domain.page
{
    public class PageParams
    {
        public const int MaxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        public int pageSize = 10;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = (value > MaxPageSize) ? MaxPageSize : value; }
        }

        public string Descricao { get; set; } = string.Empty;
        public int TipoId { get; set; }
        public int ConcessaoId { get; set; } 
    }
}
