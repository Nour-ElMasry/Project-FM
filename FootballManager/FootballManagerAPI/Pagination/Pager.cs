namespace FootballManagerAPI.Pagination
{
    public class Pager<T>
    {
        public int TotalResults { get; private set; }
        public int PageNumOfResults { get; private set; }
        public int CurrentPage { get; private set; }
        public int TotalPages { get; private set; }
        public int StartPage { get; private set; }
        public int EndPage { get; private set; }
        public List<T> PageResults { get; set; }

        public Pager(int totalResults, int currentPage, int pageNumOfResults = 10)
        {
            TotalPages = (int)Math.Ceiling((decimal)totalResults / (decimal)pageNumOfResults);

            CurrentPage = currentPage;
            TotalResults = totalResults;
            PageNumOfResults = pageNumOfResults;

            StartPage = currentPage - 5;
            EndPage = currentPage + 4;

            if(StartPage <= 0)
            {
                EndPage -= (StartPage - 1);
                StartPage = 1;
            }

            if(EndPage > TotalPages)
            {
                EndPage = TotalPages;
                if(EndPage > 10)
                {
                    StartPage = EndPage - 9;
                }
            }
        }
    }
}
