namespace Application.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        Task Save();
        public IFixtureRepository FixtureRepository { get; }
        public ILeagueRepository LeagueRepository { get; }
        public IManagerRepository ManagerRepository { get; }
        public IPeopleRepository PeopleRepository { get; }
        public IPlayerRepository PlayerRepository { get; }
        public ITeamRepository TeamRepository { get; }
        public IUserRepository UserRepository { get; }
    }
}
