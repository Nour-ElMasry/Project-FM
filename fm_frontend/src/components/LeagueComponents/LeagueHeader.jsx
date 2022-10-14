const LeagueHeader = (props) => {
    return <div className="globalHeaderInfo">
        <img 
            className="globalImg" 
            src="https://brandlogos.net/wp-content/uploads/2013/06/world-cup-vector-logo-400x400.png" 
            alt="League"
        />
        <div className="globalName">
            <h2>{props.league?.name}</h2>
            <em>{props.league?.currentSeason?.year}</em>
        </div>
    </div>
}

export default LeagueHeader;