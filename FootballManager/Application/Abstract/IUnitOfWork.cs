namespace Application.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        Task Save();
        public IFixtureRepository FixtureRepository { get; set; }
        public ILeagueRepository LeagueRepository { get; set; }
        public IManagerRepository ManagerRepository { get; set; }
        public IPeopleRepository PeopleRepository { get; set; }
        public IPlayerRepository PlayerRepository { get; set; }
        public ITeamRepository TeamRepository { get; set; }
        public IUserRepository UserRepository { get; set; }
    }
}
