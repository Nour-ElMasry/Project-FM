using Application.Abstract;

namespace Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _dataContext;

        public UnitOfWork(DataContext dataContext, IFixtureRepository fixtureRepository, 
            ILeagueRepository leagueRepository, IManagerRepository managerRepository, 
            IPeopleRepository peopleRepository, IPlayerRepository playerRepository, 
            ITeamRepository teamRepository, IUserRepository userRepository)
        {
            _dataContext = dataContext;

            FixtureRepository = fixtureRepository; 
            LeagueRepository = leagueRepository;
            ManagerRepository = managerRepository;
            PeopleRepository = peopleRepository;
            PlayerRepository = playerRepository;
            TeamRepository = teamRepository;
            UserRepository = userRepository;
        }

        public IFixtureRepository FixtureRepository {  get; private set; }
        public ILeagueRepository LeagueRepository { get; private set; }
        public IManagerRepository ManagerRepository {  get; private set; }
        public IPeopleRepository PeopleRepository {  get; private set; }
        public IPlayerRepository PlayerRepository {  get; private set; }
        public ITeamRepository TeamRepository {  get; private set; }
        public IUserRepository UserRepository {  get; private set; }

        public async Task Save()
        {
            await _dataContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dataContext.Dispose();
        }
    }
}
